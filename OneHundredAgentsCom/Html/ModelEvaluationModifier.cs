// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using AngleSharp.Dom;
using Genova.Common.Html;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;
using Genova.OneHundredAgentsCom.Utilities;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Replaces a markdown-style evaluation report placeholder with a simple DOM fragment
/// while we implement full deserialization later.
/// </summary>
internal sealed class ModelEvaluationModifier : IHtmlModifier
{
    private const string FrontierModel = "gpt-5";

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
                !text.Contains(" EVALUATION-REPORT ", StringComparison.OrdinalIgnoreCase) ||
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

            // Build resource path like "Data/local-model-evaluation.json"
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

            // Deserialize into List<LocalModelEvalution>.
            List<LocalModelEvaluation>? models = null;
            try
            {
                models = JsonSerializer.Deserialize<List<LocalModelEvaluation>>(json, _serializer_options);
            }
            catch (JsonException)
            {
                // Treat deserialization errors as a failure: remove marker and stop.
                p.ParentElement?.RemoveChild(p);
                break;
            }
            catch (Exception)
            {
                // Any other error: remove marker and stop.
                p.ParentElement?.RemoveChild(p);
                break;
            }

            if (models is null || models.Count == 0)
            {
                // No usable data: remove marker and stop.
                p.ParentElement?.RemoveChild(p);
                break;
            }

            // Sort by Score descending.
            models = models.OrderByDescending(m => m.Score).ToList();

            // Ensure item with Name "gpt-5" appears first (case-insensitive) if present.
            int gptIndex = models.FindIndex(m => string.Equals(m.Name, FrontierModel, StringComparison.OrdinalIgnoreCase));
            if (gptIndex > 0)
            {
                LocalModelEvaluation gptItem = models[gptIndex];
                models.RemoveAt(gptIndex);
                models.Insert(0, gptItem);
            }

            // Create table to present the list.
            IDocument? doc = p.Owner ?? p.ParentElement?.Owner;
            if (doc is null)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }

            IElement table = doc.CreateElement("table");
            table.ClassName = "model-evaluation";

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

            IElement thName = doc.CreateElement("th");
            thName.TextContent = "Name";
            headerRow.AppendChild(thName);

            IElement thSize = doc.CreateElement("th");
            thSize.TextContent = "Size";
            headerRow.AppendChild(thSize);

            IElement thScore = doc.CreateElement("th");
            thScore.ClassName = "score-column";
            thScore.TextContent = "Score";
            headerRow.AppendChild(thScore);

            // Columns 4..9 get hidden-if-narrow class on headers
            string hiddenClass = "hidden-if-narrow";

            IElement thExtraction = doc.CreateElement("th");
            thExtraction.ClassName = hiddenClass;
            thExtraction.TextContent = "Extraction";
            headerRow.AppendChild(thExtraction);

            IElement thSchema = doc.CreateElement("th");
            thSchema.ClassName = hiddenClass;
            thSchema.TextContent = "Schema";
            headerRow.AppendChild(thSchema);

            IElement thReportAccuracy = doc.CreateElement("th");
            thReportAccuracy.ClassName = hiddenClass;
            thReportAccuracy.TextContent = "Report Accuracy";
            headerRow.AppendChild(thReportAccuracy);

            IElement thReportStyle = doc.CreateElement("th");
            thReportStyle.ClassName = hiddenClass;
            thReportStyle.TextContent = "Report Style";
            headerRow.AppendChild(thReportStyle);

            IElement thIDTool = doc.CreateElement("th");
            thIDTool.ClassName = hiddenClass;
            thIDTool.TextContent = "ID Tool";
            headerRow.AppendChild(thIDTool);

            IElement thCreditTool = doc.CreateElement("th");
            thCreditTool.ClassName = hiddenClass;
            thCreditTool.TextContent = "Credit Tool";
            headerRow.AppendChild(thCreditTool);

            thead.AppendChild(headerRow);
            table.AppendChild(thead);

            // Create tbody and rows
            IElement tbody = doc.CreateElement("tbody");

            foreach (LocalModelEvaluation model in models)
            {
                if (model is null ||
                    model.Name!.Equals(FrontierModel, StringComparison.OrdinalIgnoreCase) ||
                    model.Score <= 0)
                {
                    continue;
                }

                IElement row = doc.CreateElement("tr");

                // Name cell (no scale)
                IElement nameCell = doc.CreateElement("td");
                nameCell.ClassName = "model-name";
                nameCell.TextContent = model.Name ?? string.Empty;
                row.AppendChild(nameCell);

                // Size cell (GB scale)
                IElement sizeCell = doc.CreateElement("td");
                sizeCell.ClassName = "size-column";
                if (Math.Abs(model.Size) < double.Epsilon)
                {
                    // If Size is 0 -> inner HTML is a non-breaking space only.
                    sizeCell.InnerHtml = "&nbsp;";
                }
                else
                {
                    // numeric text formatted with at most two decimal places
                    IText sizeText = doc.CreateTextNode(model.Size.ToString("0.##", CultureInfo.InvariantCulture));
                    sizeCell.AppendChild(sizeText);

                    // scale span (no leading space inside span)
                    IElement sizeScale = doc.CreateElement("span");
                    sizeScale.ClassName = "scale";
                    sizeScale.TextContent = "GB";
                    sizeCell.AppendChild(sizeScale);
                }

                row.AppendChild(sizeCell);

                // Score cell (% scale)
                IElement scoreCell = doc.CreateElement("td");
                scoreCell.ClassName = "score-column";
                IText scoreText = doc.CreateTextNode(model.Score.ToString(CultureInfo.InvariantCulture));
                scoreCell.AppendChild(scoreText);
                IElement scoreScale = doc.CreateElement("span");
                scoreScale.ClassName = "scale";
                scoreScale.TextContent = "%";
                scoreCell.AppendChild(scoreScale);
                row.AppendChild(scoreCell);

                // Extraction (hidden-if-narrow) (/5)
                IElement extractionCell = doc.CreateElement("td");
                extractionCell.ClassName = hiddenClass;
                IText extractionText = doc.CreateTextNode(model.Extraction.ToString(CultureInfo.InvariantCulture));
                extractionCell.AppendChild(extractionText);
                IElement extractionScale = doc.CreateElement("span");
                extractionScale.ClassName = "scale";
                extractionScale.TextContent = "/5";
                extractionCell.AppendChild(extractionScale);
                row.AppendChild(extractionCell);

                // Schema (hidden-if-narrow) (/5)
                IElement schemaCell = doc.CreateElement("td");
                schemaCell.ClassName = hiddenClass;
                IText schemaText = doc.CreateTextNode(model.Schema.ToString(CultureInfo.InvariantCulture));
                schemaCell.AppendChild(schemaText);
                IElement schemaScale = doc.CreateElement("span");
                schemaScale.ClassName = "scale";
                schemaScale.TextContent = "/5";
                schemaCell.AppendChild(schemaScale);
                row.AppendChild(schemaCell);

                // ReportAccuracy (hidden-if-narrow) (/5)
                IElement reportAccuracyCell = doc.CreateElement("td");
                reportAccuracyCell.ClassName = hiddenClass;
                IText reportAccuracyText = doc.CreateTextNode(model.ReportAccuracy.ToString(CultureInfo.InvariantCulture));
                reportAccuracyCell.AppendChild(reportAccuracyText);
                IElement reportAccuracyScale = doc.CreateElement("span");
                reportAccuracyScale.ClassName = "scale";
                reportAccuracyScale.TextContent = "/5";
                reportAccuracyCell.AppendChild(reportAccuracyScale);
                row.AppendChild(reportAccuracyCell);

                // ReportStyle (hidden-if-narrow) (/5)
                IElement reportStyleCell = doc.CreateElement("td");
                reportStyleCell.ClassName = hiddenClass;
                IText reportStyleText = doc.CreateTextNode(model.ReportStyle.ToString(CultureInfo.InvariantCulture));
                reportStyleCell.AppendChild(reportStyleText);
                IElement reportStyleScale = doc.CreateElement("span");
                reportStyleScale.ClassName = "scale";
                reportStyleScale.TextContent = "/5";
                reportStyleCell.AppendChild(reportStyleScale);
                row.AppendChild(reportStyleCell);

                // IDTool (hidden-if-narrow) (/5)
                IElement idToolCell = doc.CreateElement("td");
                idToolCell.ClassName = hiddenClass;
                IText idToolText = doc.CreateTextNode(model.IDTool.ToString(CultureInfo.InvariantCulture));
                idToolCell.AppendChild(idToolText);
                IElement idToolScale = doc.CreateElement("span");
                idToolScale.ClassName = "scale";
                idToolScale.TextContent = "/5";
                idToolCell.AppendChild(idToolScale);
                row.AppendChild(idToolCell);

                // CreditTool (hidden-if-narrow) (/5)
                IElement creditToolCell = doc.CreateElement("td");
                creditToolCell.ClassName = hiddenClass;
                IText creditToolText = doc.CreateTextNode(model.CreditTool.ToString(CultureInfo.InvariantCulture));
                creditToolCell.AppendChild(creditToolText);
                IElement creditToolScale = doc.CreateElement("span");
                creditToolScale.ClassName = "scale";
                creditToolScale.TextContent = "/5";
                creditToolCell.AppendChild(creditToolScale);
                row.AppendChild(creditToolCell);

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
}
