// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;
using Genova.OneHundredAgentsCom.Utilities;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Displays a sandbox tester report as an HTML table.
/// </summary>
internal sealed class SandboxReportModifier : IHtmlModifier
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Conflicting naming rules.")]
    private static readonly JsonSerializerOptions _serializer_options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Determines the CSS class for a capability row based on the Tool, Shell and Alternates outcomes.
    /// Returns one of: "capability-allowed", "capability-denied", "capability-na".
    /// </summary>
    /// <param name="capability">The capability result to evaluate.</param>
    /// <returns>The CSS class name for the capability row.</returns>
    public static string GetCapabilityRowClass(CapabilityResult capability)
    {
        static string NormalizeOutcome(string? s) => string.IsNullOrWhiteSpace(s) ? "N/A" : s.Trim();

        string toolOutcome = NormalizeOutcome(capability.Tool?.Outcome);
        string shellOutcome = NormalizeOutcome(capability.Shell?.Outcome);
        string altOutcome = NormalizeOutcome(capability.Alternates?.Outcome);

        string[] outcomes = [toolOutcome, shellOutcome, altOutcome];

        // (3) Allowed: any outcome is "Allowed"
        bool anyAllowed = Array.Exists(outcomes, o => o.Equals("Allowed", StringComparison.OrdinalIgnoreCase));
        if (anyAllowed)
        {
            return "capability-allowed";
        }

        // (1) Denied: all outcomes are in {Not applicable, Error, Denied} AND at least one is Denied
        bool anyDenied = Array.Exists(outcomes, o => o.Equals("Denied", StringComparison.OrdinalIgnoreCase));
        bool allInDeniedSet = Array.TrueForAll(outcomes, o =>
            o.Equals("N/A", StringComparison.OrdinalIgnoreCase) ||
            o.Equals("Error", StringComparison.OrdinalIgnoreCase) ||
            o.Equals("Denied", StringComparison.OrdinalIgnoreCase));

        if (allInDeniedSet && anyDenied)
        {
            return "capability-denied";
        }

        // (2) NA: all outcomes are in {Not applicable, Error}
        bool allNaOrError = Array.TrueForAll(outcomes, o =>
            o.Equals("N/A", StringComparison.OrdinalIgnoreCase) ||
            o.Equals("Error", StringComparison.OrdinalIgnoreCase));

        if (allNaOrError)
        {
            return "capability-na";
        }

        // Default fallback: treat unknown/mixed permutations as NA.
        return "capability-na";
    }

    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
        // No initialization required for this implementation.
    }

    /// <inheritdoc/>
    public void Modify(IDocument document)
    {
        List<IElement> paragraphs = document.QuerySelectorAll("p").ToList();

        foreach (IElement p in paragraphs)
        {
            string text = p.TextContent.Trim();

            if (!text.StartsWith(":::", StringComparison.Ordinal) ||
                !text.Contains(" SANDBOX-REPORT ", StringComparison.OrdinalIgnoreCase) ||
                !text.EndsWith(":::", StringComparison.Ordinal))
            {
                continue;
            }

            // Normalize curly quotes to straight quotes so ParseAttributes can read them.
            string normalized = text.Replace('“', '"').Replace('”', '"');

            Dictionary<string, string> attributes = InstructionHelper.ParseAttributes(normalized);

            // If visible="false" (case-insensitive) remove the placeholder and do not insert a table.
            if (attributes.TryGetValue("visible", out string? visible) &&
                string.Equals(visible?.Trim(), "false", StringComparison.OrdinalIgnoreCase))
            {
                p.ParentElement?.RemoveChild(p);

                // Consistent with existing behavior: stop after handling the first matching marker.
                break;
            }

            if (!attributes.TryGetValue("name", out string? name) || string.IsNullOrWhiteSpace(name))
            {
                // Nothing to replace with; skip.
                continue;
            }

            // Build resource path like "Data/stock-local-machine.json"
            string resourcePath = $"Data/{name}.json";

            // Load embedded JSON (must exist); if not present treat as error and remove marker.
            Stream? stream = FileHelper.GetEmbeddedResourceStream(typeof(Website), resourcePath);
            if (stream is null)
            {
                // Remove the marker paragraph and stop processing.
                p.ParentElement?.RemoveChild(p);
                break;
            }

            string json;
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            // Attempt to deserialize into a list of CapabilityGroupResult.
            List<CapabilityGroupResult>? groups = null;
            try
            {
                groups = JsonSerializer.Deserialize<List<CapabilityGroupResult>>(json, _serializer_options);
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

            if (groups is null)
            {
                // Null result counts as failure; remove marker and stop.
                p.ParentElement?.RemoveChild(p);
                break;
            }

            // Present the report using a dedicated helper.
            PresentReport(p, groups);

            // Only handle the first matching marker for this simple implementation.
            break;
        }
    }

    private static IElement CreateOutcomeElement(IDocument document, string outcome)
    {
        string text = outcome ?? string.Empty;
        string normalized = text.Trim();

        // Map outcome text to a CSS class. Default to "na" (not applicable).
        string outcomeClass = normalized.Equals("Allowed", StringComparison.OrdinalIgnoreCase) ? "outcome-allowed" :
                              normalized.Equals("Denied", StringComparison.OrdinalIgnoreCase) ? "outcome-denied" :
                              normalized.Equals("Error", StringComparison.OrdinalIgnoreCase) ? "outcome-error" :
                              "outcome-na";

        // Determine tooltip/title text: Allowed/Denied/Error otherwise "Not applicable".
        string titleText = normalized.Equals("Allowed", StringComparison.OrdinalIgnoreCase) ? "Allowed" :
                           normalized.Equals("Denied", StringComparison.OrdinalIgnoreCase) ? "Denied" :
                           normalized.Equals("Error", StringComparison.OrdinalIgnoreCase) ? "Error" :
                           "Not applicable";

        IElement wrapper = document.CreateElement("span");
        wrapper.ClassName = $"outcome {outcomeClass}";

        // Add title so browsers show a tooltip on hover.
        wrapper.SetAttribute("title", titleText);

        IElement icon = document.CreateElement("span");
        icon.ClassName = "icon";
        icon.SetAttribute("aria-hidden", "true");
        wrapper.AppendChild(icon);

        IElement hidden = document.CreateElement("span");
        hidden.ClassName = "visually-hidden";
        hidden.TextContent = normalized;
        wrapper.AppendChild(hidden);

        return wrapper;
    }

    private static void PresentReport(IElement markerParagraph, List<CapabilityGroupResult> groups)
    {
        // Obtain the document to create elements from. If unavailable, remove the marker.
        IDocument? document = markerParagraph.Owner ?? markerParagraph.ParentElement?.Owner;
        if (document is null)
        {
            markerParagraph.ParentElement?.RemoveChild(markerParagraph);
            return;
        }

        // Re-parse attributes from the marker paragraph to pick up an optional title.
        string markerText = markerParagraph.TextContent.Trim();
        string normalized = markerText.Replace('“', '"').Replace('”', '"');
        Dictionary<string, string> attributes = InstructionHelper.ParseAttributes(normalized);
        attributes.TryGetValue("title", out string? tableTitle);

        // Create table with accessible caption and headers.
        IElement table = document.CreateElement("table");
        table.ClassName = "sandbox-report";

        if (!string.IsNullOrWhiteSpace(tableTitle))
        {
            IElement caption = document.CreateElement("caption");
            caption.TextContent = tableTitle;
            table.AppendChild(caption);
        }

        // Create thead with column headers.
        IElement thead = document.CreateElement("thead");
        IElement headerRow = document.CreateElement("tr");

        IElement thTest = document.CreateElement("th");
        thTest.ClassName = "test-head";
        thTest.TextContent = "Test";
        headerRow.AppendChild(thTest);

        IElement thDescription = document.CreateElement("th");
        thDescription.ClassName = "description-head";
        thDescription.TextContent = "Description";
        headerRow.AppendChild(thDescription);

        IElement thTool = document.CreateElement("th");
        thTool.ClassName = "outcome-head";
        thTool.TextContent = "Tool";
        headerRow.AppendChild(thTool);

        IElement thShell = document.CreateElement("th");
        thShell.ClassName = "outcome-head";
        thShell.TextContent = "Shell";
        headerRow.AppendChild(thShell);

        IElement thAlternates = document.CreateElement("th");
        thAlternates.ClassName = "outcome-head";
        thAlternates.TextContent = "Alt";
        headerRow.AppendChild(thAlternates);

        thead.AppendChild(headerRow);
        table.AppendChild(thead);

        IElement tbody = document.CreateElement("tbody");

        // For each group, emit a group row (two columns) then rows for each capability.
        foreach (CapabilityGroupResult group in groups)
        {
            // Group row: two columns, second spans four columns.
            IElement groupRow = document.CreateElement("tr");
            groupRow.ClassName = "sandbox-group";

            IElement groupIdCell = document.CreateElement("th");
            groupIdCell.SetAttribute("scope", "row");
            groupIdCell.TextContent = group.Id;
            groupRow.AppendChild(groupIdCell);

            IElement groupDescCell = document.CreateElement("td");
            groupDescCell.ClassName = "group-description";
            groupDescCell.SetAttribute("colspan", "4");

            // Place title inside a child <div> for styling/consistency.
            IElement groupDescDiv = document.CreateElement("div");
            groupDescDiv.TextContent = group.Title;
            groupDescCell.AppendChild(groupDescDiv);

            groupRow.AppendChild(groupDescCell);

            tbody.AppendChild(groupRow);

            // Capability rows.
            foreach (CapabilityResult capability in group.Capabilities)
            {
                IElement capRow = document.CreateElement("tr");

                // Determine row class by delegating to a static helper that inspects the capability.
                string resultClass = GetCapabilityRowClass(capability);
                capRow.ClassName = $"capability-row {resultClass}";

                IElement compositeIdCell = document.CreateElement("th");
                compositeIdCell.SetAttribute("scope", "row");

                // Wrap group id and trailing space in a <span class="group-id">, leave capability id as text node.
                IElement groupSpan = document.CreateElement("span");
                groupSpan.ClassName = "group-id";
                groupSpan.TextContent = group.Id + " ";
                compositeIdCell.AppendChild(groupSpan);

                IText capIdText = document.CreateTextNode(capability.Id);
                compositeIdCell.AppendChild(capIdText);

                capRow.AppendChild(compositeIdCell);

                IElement descCell = document.CreateElement("td");
                descCell.ClassName = "test-description";

                // Place description inside a child <div> for styling/consistency.
                IElement descDiv = document.CreateElement("div");
                descDiv.TextContent = capability.Title;
                descCell.AppendChild(descDiv);

                capRow.AppendChild(descCell);

                // Tool outcome
                IElement toolCell = document.CreateElement("td");
                toolCell.ClassName = "outcome-cell";
                IElement toolOutcomeElement = CreateOutcomeElement(document, capability.Tool?.Outcome ?? string.Empty);
                toolCell.AppendChild(toolOutcomeElement);
                capRow.AppendChild(toolCell);

                // Shell outcome
                IElement shellCell = document.CreateElement("td");
                shellCell.ClassName = "outcome-cell";
                IElement shellOutcomeElement = CreateOutcomeElement(document, capability.Shell?.Outcome ?? string.Empty);
                shellCell.AppendChild(shellOutcomeElement);
                capRow.AppendChild(shellCell);

                // Alternates outcome
                IElement altCell = document.CreateElement("td");
                altCell.ClassName = "outcome-cell";
                IElement altOutcomeElement = CreateOutcomeElement(document, capability.Alternates?.Outcome ?? string.Empty);
                altCell.AppendChild(altOutcomeElement);
                capRow.AppendChild(altCell);

                tbody.AppendChild(capRow);
            }
        }

        table.AppendChild(tbody);

        IElement? parent = markerParagraph.ParentElement;
        if (parent is not null)
        {
            parent.InsertBefore(table, markerParagraph);
            markerParagraph.Remove();
        }
    }
}
