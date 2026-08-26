// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using Genova.Common.Html;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;
using Genova.OneHundredAgentsCom.Utilities;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Replaces a markdown-style OCR report placeholder with a simple DOM fragment
/// backed by the embedded local-model-ocr.json data.
/// </summary>
internal sealed partial class OcrEvaluationModifier : IHtmlModifier
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Conflicting naming rules.")]
    private static readonly JsonSerializerOptions _serializer_options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <inheritdoc/>
    public void Initialize(Common.Execution.IExecutionContext executionContext, IWebsite website)
    {
        // No initialization required for this phase.
    }

    /// <inheritdoc/>
    public void Modify(IDocument document)
    {
        List<IElement> paragraphs = document.QuerySelectorAll("p").ToList();

        foreach (IElement p in paragraphs)
        {
            string text = p.TextContent.Trim();

            if (!text.StartsWith(":::", StringComparison.Ordinal) ||
                !text.Contains(" OCR-REPORT ", StringComparison.OrdinalIgnoreCase) ||
                !text.EndsWith(":::", StringComparison.Ordinal))
            {
                continue;
            }

            // Normalize curly quotes to straight quotes so ParseAttributes can read them.
            string normalized = text.Replace('“', '"').Replace('”', '"');

            Dictionary<string, string> attributes = InstructionHelper.ParseAttributes(normalized);

            if (!attributes.TryGetValue("name", out string? name) || string.IsNullOrWhiteSpace(name))
            {
                // Nothing to load; skip this marker.
                continue;
            }

            // Build resource path like "Data/local-model-ocr.json"
            string resourcePath = $"Data/{name}.json";

            // Load embedded JSON (must exist); if not present remove the marker.
            Stream? stream = FileHelper.GetEmbeddedResourceStream(typeof(Website), resourcePath);
            if (stream is null)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }

            string json;
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            // Deserialize into List<LocalOcrEvaluation>.
            List<LocalOcrEvaluation>? models = null;
            try
            {
                models = JsonSerializer.Deserialize<List<LocalOcrEvaluation>>(json, _serializer_options);
            }
            catch (JsonException)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }
            catch (Exception)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }

            if (models is null || models.Count == 0)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }

            // Sort by FinalScore descending.
            models = models.OrderByDescending(m => m.FinalScore).ToList();

            // Create table to present the list.
            IDocument? doc = p.Owner ?? p.ParentElement?.Owner;
            if (doc is null)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }

            IElement table = doc.CreateElement("table");
            table.ClassName = "ocr-evaluation";

            // Optional caption from attributes.title
            if (attributes.TryGetValue("title", out string? title) && !string.IsNullOrWhiteSpace(title))
            {
                IElement caption = doc.CreateElement("caption");
                caption.TextContent = title;
                table.AppendChild(caption);
            }

            // Create thead
            IElement thead = doc.CreateElement("thead");
            IElement headerRow = doc.CreateElement("tr");

            // Column 1: Model
            IElement thModel = doc.CreateElement("th");
            thModel.ClassName = "model-name";
            thModel.TextContent = "Model";
            headerRow.AppendChild(thModel);

            // Column 2: Size
            IElement thSize = doc.CreateElement("th");
            thSize.ClassName = "model-size size-column";
            thSize.TextContent = "Size";
            headerRow.AppendChild(thSize);

            // Column 3: Speed
            IElement thSpeed = doc.CreateElement("th");
            thSpeed.ClassName = "model-speed speed-column";
            thSpeed.TextContent = "Speed";
            headerRow.AppendChild(thSpeed);

            // Column 4: Score
            IElement thScore = doc.CreateElement("th");
            thScore.ClassName = "model-score score-column";
            thScore.TextContent = "Score";
            headerRow.AppendChild(thScore);

            // Columns 5..7 get hidden-if-narrow class
            string hiddenClass = "hidden-if-narrow";

            IElement thArticle = doc.CreateElement("th");
            thArticle.ClassName = hiddenClass;
            thArticle.TextContent = "Article";
            headerRow.AppendChild(thArticle);

            IElement thHandwriting = doc.CreateElement("th");
            thHandwriting.ClassName = hiddenClass;
            thHandwriting.TextContent = "Handwriting";
            headerRow.AppendChild(thHandwriting);

            IElement thPoster = doc.CreateElement("th");
            thPoster.ClassName = hiddenClass;
            thPoster.TextContent = "Poster";
            headerRow.AppendChild(thPoster);

            thead.AppendChild(headerRow);
            table.AppendChild(thead);

            // Create tbody and rows
            IElement tbody = doc.CreateElement("tbody");

            foreach (LocalOcrEvaluation model in models)
            {
                if (model is null)
                {
                    continue;
                }

                if (model.Size < 0.0f)
                {
                    // Online entries.
                    continue;
                }

                if (model.AverageScore <= 0.0)
                {
                    // Skip invalid entries.
                    continue;
                }

                IElement row = doc.CreateElement("tr");

                // Model cell
                IElement nameCell = doc.CreateElement("td");
                nameCell.ClassName = "model-name";
                nameCell.TextContent = model.Name ?? string.Empty;
                row.AppendChild(nameCell);

                // Size cell (GB scale) - model-size class
                IElement sizeCell = doc.CreateElement("td");
                sizeCell.ClassName = "model-size size-column";

                // Special-case gpt-5 with size 0.00 -> render non-breaking space
                if (string.Equals(model.Name, "gpt-5", StringComparison.OrdinalIgnoreCase) &&
                    Math.Abs(model.Size) < float.Epsilon)
                {
                    sizeCell.InnerHtml = "&nbsp;";
                }
                else
                {
                    IText sizeText = doc.CreateTextNode(model.Size.ToString("0.##", CultureInfo.InvariantCulture));
                    sizeCell.AppendChild(sizeText);
                    IElement sizeScale = doc.CreateElement("span");
                    sizeScale.ClassName = "scale";
                    sizeScale.TextContent = "GB";
                    sizeCell.AppendChild(sizeScale);
                }

                row.AppendChild(sizeCell);

                // Speed cell (format TotalDuration as hh:mm:ss or mm:ss)
                IElement speedCell = doc.CreateElement("td");
                speedCell.ClassName = "model-speed speed-column";
                string formattedDuration = FormatDuration(model.TotalDuration);
                IText speedText = doc.CreateTextNode(formattedDuration);
                speedCell.AppendChild(speedText);
                row.AppendChild(speedCell);

                // Score cell (AverageScore formatted with a single decimal place)
                IElement scoreCell = doc.CreateElement("td");
                scoreCell.ClassName = "model-score score-column";
                IText scoreText = doc.CreateTextNode(model.AverageScore.ToString("0.0", CultureInfo.InvariantCulture));
                scoreCell.AppendChild(scoreText);
                IElement scoreScale = doc.CreateElement("span");
                scoreScale.ClassName = "scale";
                scoreScale.TextContent = "%";
                scoreCell.AppendChild(scoreScale);
                row.AppendChild(scoreCell);

                // Article (hidden-if-narrow)
                IElement articleCell = doc.CreateElement("td");
                articleCell.ClassName = hiddenClass;
                IText articleText = doc.CreateTextNode(model.Article.Score.ToString(CultureInfo.InvariantCulture));
                articleCell.AppendChild(articleText);
                IElement articleScale = doc.CreateElement("span");
                articleScale.ClassName = "scale";
                articleScale.TextContent = "%";
                articleCell.AppendChild(articleScale);
                row.AppendChild(articleCell);

                // Handwriting (hidden-if-narrow) -- property name in model is 'Hardwriting' per LocalOcrEvaluation
                IElement handwritingCell = doc.CreateElement("td");
                handwritingCell.ClassName = hiddenClass;
                IText handwritingText = doc.CreateTextNode(model.Hardwriting.Score.ToString(CultureInfo.InvariantCulture));
                handwritingCell.AppendChild(handwritingText);
                IElement handwritingScale = doc.CreateElement("span");
                handwritingScale.ClassName = "scale";
                handwritingScale.TextContent = "%";
                handwritingCell.AppendChild(handwritingScale);
                row.AppendChild(handwritingCell);

                // Poster (hidden-if-narrow)
                IElement posterCell = doc.CreateElement("td");
                posterCell.ClassName = hiddenClass;
                IText posterText = doc.CreateTextNode(model.Poster.Score.ToString(CultureInfo.InvariantCulture));
                posterCell.AppendChild(posterText);
                IElement posterScale = doc.CreateElement("span");
                posterScale.ClassName = "scale";
                posterScale.TextContent = "%";
                posterCell.AppendChild(posterScale);
                row.AppendChild(posterCell);

                tbody.AppendChild(row);
            }

            table.AppendChild(tbody);

            IElement? parent = p.ParentElement;
            if (parent is not null)
            {
                parent.InsertBefore(table, p);
                p.Remove();
            }

            // Only handle the first matching marker for now.
            break;
        }
    }

    /// <summary>
    /// Parse duration strings like "01h 41m 20s" or "01m 02s" into a formatted
    /// "hh:mm:ss" (for &gt;= 1 hour) or "mm:ss" (for &lt; 1 hour) string. Returns
    /// an empty string when input is null/empty or cannot be parsed.
    /// </summary>
    private static string FormatDuration(string? duration)
    {
        if (string.IsNullOrWhiteSpace(duration))
        {
            return string.Empty;
        }

        // Accept patterns with optional hours/minutes/seconds, allowing spaces:
        // e.g. "01h 41m 20s", "01m 02s", "00m 13s"
        Match match = TimeFormatRegex().Match(duration);
        if (!match.Success)
        {
            return duration.Trim();
        }

        int hours = 0;
        int minutes = 0;
        int seconds = 0;

        if (match.Groups[1].Success)
        {
            int.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out hours);
        }

        if (match.Groups[2].Success)
        {
            int.TryParse(match.Groups[2].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out minutes);
        }

        if (match.Groups[3].Success)
        {
            int.TryParse(match.Groups[3].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out seconds);
        }

        var ts = new TimeSpan(hours, minutes, seconds);

        if (ts.TotalHours >= 1.0)
        {
            int totalHours = (int)ts.TotalHours;
            return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}", totalHours, ts.Minutes, ts.Seconds);
        }
        else
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
    }

    [GeneratedRegex(@"^\s*(?:(\d+)\s*h)?\s*(?:(\d+)\s*m)?\s*(?:(\d+)\s*s)?\s*$", RegexOptions.IgnoreCase, "en-AU")]
    private static partial Regex TimeFormatRegex();
}
