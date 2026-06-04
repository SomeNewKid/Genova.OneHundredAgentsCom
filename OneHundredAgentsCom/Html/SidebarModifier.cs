// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Websites;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Converts SIDEBAR instruction blocks into sidebar definition-list markup.
/// </summary>
internal sealed class SidebarModifier : IHtmlModifier
{
    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
        // No initialization required.
    }

    /// <inheritdoc/>
    public void Modify(IDocument document)
    {
        List<IElement> paragraphs = document.QuerySelectorAll("p").ToList();

        IElement? start = null;
        IElement? end = null;

        for (int i = 0; i < paragraphs.Count; i++)
        {
            string text = paragraphs[i].TextContent.Trim();
            if (start is null && IsSidebarStart(text))
            {
                start = paragraphs[i];
                continue;
            }

            if (start is not null && IsSidebarEnd(text))
            {
                end = paragraphs[i];
                break;
            }
        }

        if (start is null || end is null || start.ParentElement != end.ParentElement)
        {
            return;
        }

        List<INode> nodes = GetNodesBetween(start, end);
        IElement sidebar = BuildSidebar(document, nodes);

        IElement parent = start.ParentElement!;
        parent.InsertBefore(sidebar, start);

        foreach (INode node in nodes)
        {
            node.Parent?.RemoveChild(node);
        }

        start.Remove();
        end.Remove();
    }

    private static IElement BuildSidebar(IDocument document, List<INode> nodes)
    {
        IElement sidebar = document.CreateElement("div");
        sidebar.SetAttribute("class", "sidebar");

        IElement? definitionList = null;

        foreach (INode node in nodes)
        {
            if (node is not IElement element)
            {
                continue;
            }

            if (element.TagName.Equals("HR", StringComparison.OrdinalIgnoreCase))
            {
                definitionList = null;
                continue;
            }

            if (!element.TagName.Equals("P", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            DefinitionEntry? entry = GetDefinitionEntry(element);
            if (entry is null)
            {
                continue;
            }

            definitionList ??= CreateDefinitionList(document, sidebar);
            AppendDefinitionEntry(document, definitionList, entry.Value);
        }

        return sidebar;
    }

    private static IElement CreateDefinitionList(IDocument document, IElement sidebar)
    {
        IElement definitionList = document.CreateElement("dl");
        sidebar.AppendChild(definitionList);
        return definitionList;
    }

    private static void AppendDefinitionEntry(IDocument document, IElement definitionList, DefinitionEntry entry)
    {
        IElement term = document.CreateElement("dt");
        term.TextContent = entry.Term;
        definitionList.AppendChild(term);

        IElement description = document.CreateElement("dd");
        AppendDescription(document, description, entry.DescriptionHtml);
        definitionList.AppendChild(description);
    }

    private static void AppendDescription(IDocument document, IElement description, string descriptionHtml)
    {
        if (!descriptionHtml.Contains('|', StringComparison.Ordinal))
        {
            description.InnerHtml = descriptionHtml;
            return;
        }

        IElement list = document.CreateElement("ul");
        string[] items = descriptionHtml.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (string item in items)
        {
            IElement listItem = document.CreateElement("li");
            listItem.InnerHtml = item;
            list.AppendChild(listItem);
        }

        description.AppendChild(list);
    }

    private static DefinitionEntry? GetDefinitionEntry(IElement paragraph)
    {
        string html = paragraph.InnerHtml;
        int colonIndex = html.IndexOf(':', StringComparison.Ordinal);
        if (colonIndex < 0)
        {
            return null;
        }

        string term = html.Substring(0, colonIndex).Trim();
        string description = html.Substring(colonIndex + 1).Trim();

        if (string.IsNullOrEmpty(term))
        {
            return null;
        }

        return new DefinitionEntry(term, description);
    }

    private static List<INode> GetNodesBetween(IElement start, IElement end)
    {
        List<INode> nodes = [];

        INode? current = start.NextSibling;
        while (current is not null && current != end)
        {
            nodes.Add(current);
            current = current.NextSibling;
        }

        return nodes;
    }

    private static bool IsSidebarStart(string text)
    {
        return text.StartsWith(":::", StringComparison.Ordinal) &&
               text.Contains(" SIDEBAR ", StringComparison.OrdinalIgnoreCase) &&
               text.EndsWith(":::", StringComparison.Ordinal);
    }

    private static bool IsSidebarEnd(string text)
    {
        return text.StartsWith(":::", StringComparison.Ordinal) &&
               text.Contains(" /SIDEBAR ", StringComparison.OrdinalIgnoreCase) &&
               text.EndsWith(":::", StringComparison.Ordinal);
    }

    private readonly struct DefinitionEntry
    {
        public DefinitionEntry(string term, string descriptionHtml)
        {
            Term = term;
            DescriptionHtml = descriptionHtml;
        }

        public string Term { get; }

        public string DescriptionHtml { get; }
    }
}
