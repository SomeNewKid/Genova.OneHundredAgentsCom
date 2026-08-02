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
/// Replaces a markdown-style coding evaluation report placeholder with a simple DOM fragment
/// while we implement full deserialization later.
/// </summary>
internal sealed class CodingEvaluationModifier : IHtmlModifier
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
                !text.Contains(" CODING-REPORT ", StringComparison.OrdinalIgnoreCase) ||
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

            // Build resource path like "Data/local-coding-evaluation.json"
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

            // Deserialize into List<LocalCodeEvaluation>.
            List<LocalCodeEvaluation>? models = null;
            try
            {
                models = JsonSerializer.Deserialize<List<LocalCodeEvaluation>>(json, _serializer_options);
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

            // Create table to present the list.
            IDocument? doc = p.Owner ?? p.ParentElement?.Owner;
            if (doc is null)
            {
                p.ParentElement?.RemoveChild(p);
                break;
            }

            IElement table = doc.CreateElement("table");
            table.ClassName = "coding-evaluation";

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
            thScore.TextContent = "Score";
            headerRow.AppendChild(thScore);

            // Sub-metrics get hidden-if-narrow class on headers
            string hiddenClass = "hidden-if-narrow";

            IElement thCodeGen = doc.CreateElement("th");
            thCodeGen.ClassName = hiddenClass;
            thCodeGen.TextContent = "Code Generation";
            headerRow.AppendChild(thCodeGen);

            IElement thBugRepair = doc.CreateElement("th");
            thBugRepair.ClassName = hiddenClass;
            thBugRepair.TextContent = "Bug Repair";
            headerRow.AppendChild(thBugRepair);

            IElement thCodeCompletion = doc.CreateElement("th");
            thCodeCompletion.ClassName = hiddenClass;
            thCodeCompletion.TextContent = "Code Completion";
            headerRow.AppendChild(thCodeCompletion);

            IElement thRefactoring = doc.CreateElement("th");
            thRefactoring.ClassName = hiddenClass;
            thRefactoring.TextContent = "Refactoring";
            headerRow.AppendChild(thRefactoring);

            IElement thTestGen = doc.CreateElement("th");
            thTestGen.ClassName = hiddenClass;
            thTestGen.TextContent = "Test Generation";
            headerRow.AppendChild(thTestGen);

            IElement thCodeExplanation = doc.CreateElement("th");
            thCodeExplanation.ClassName = hiddenClass;
            thCodeExplanation.TextContent = "Code Explanation";
            headerRow.AppendChild(thCodeExplanation);

            thead.AppendChild(headerRow);
            table.AppendChild(thead);

            // Create tbody and rows
            IElement tbody = doc.CreateElement("tbody");

            foreach (LocalCodeEvaluation model in models)
            {
                if (model is null || model.Score <= 0)
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
                IText scoreText = doc.CreateTextNode(model.Score.ToString(CultureInfo.InvariantCulture));
                scoreCell.AppendChild(scoreText);
                IElement scoreScale = doc.CreateElement("span");
                scoreScale.ClassName = "scale";
                scoreScale.TextContent = "%";
                scoreCell.AppendChild(scoreScale);
                row.AppendChild(scoreCell);

                // Code Generation (hidden-if-narrow)
                IElement codeGenCell = doc.CreateElement("td");
                codeGenCell.ClassName = hiddenClass;
                IText codeGenText = doc.CreateTextNode(model.CodeGeneration.ToString(CultureInfo.InvariantCulture));
                codeGenCell.AppendChild(codeGenText);
                row.AppendChild(codeGenCell);

                // Bug Repair (hidden-if-narrow)
                IElement bugRepairCell = doc.CreateElement("td");
                bugRepairCell.ClassName = hiddenClass;
                IText bugRepairText = doc.CreateTextNode(model.BugRepair.ToString(CultureInfo.InvariantCulture));
                bugRepairCell.AppendChild(bugRepairText);
                row.AppendChild(bugRepairCell);

                // Code Completion (hidden-if-narrow)
                IElement codeCompletionCell = doc.CreateElement("td");
                codeCompletionCell.ClassName = hiddenClass;
                IText codeCompletionText = doc.CreateTextNode(model.CodeCompletion.ToString(CultureInfo.InvariantCulture));
                codeCompletionCell.AppendChild(codeCompletionText);
                row.AppendChild(codeCompletionCell);

                // Refactoring (hidden-if-narrow)
                IElement refactoringCell = doc.CreateElement("td");
                refactoringCell.ClassName = hiddenClass;
                IText refactoringText = doc.CreateTextNode(model.Refactoring.ToString(CultureInfo.InvariantCulture));
                refactoringCell.AppendChild(refactoringText);
                row.AppendChild(refactoringCell);

                // Test Generation (hidden-if-narrow)
                IElement testGenCell = doc.CreateElement("td");
                testGenCell.ClassName = hiddenClass;
                IText testGenText = doc.CreateTextNode(model.TestGeneration.ToString(CultureInfo.InvariantCulture));
                testGenCell.AppendChild(testGenText);
                row.AppendChild(testGenCell);

                // Code Explanation (hidden-if-narrow)
                IElement codeExplanationCell = doc.CreateElement("td");
                codeExplanationCell.ClassName = hiddenClass;
                IText codeExplanationText = doc.CreateTextNode(model.CodeExplanation.ToString(CultureInfo.InvariantCulture));
                codeExplanationCell.AppendChild(codeExplanationText);
                row.AppendChild(codeExplanationCell);

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
