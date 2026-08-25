// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Applies post-processing to rendered non-agent article pages.
/// </summary>
internal sealed class GeneralArticleModifier : BaseArticleModifier
{
    /// <inheritdoc/>
    public override void Modify(IDocument document)
    {
        string currentPath = CurrentPath;
        if (currentPath == "/")
        {
            return;
        }

        AgentEntry? agent = GetCurrentAgent(currentPath);
        if (agent is not null)
        {
            return;
        }

        ModifyArticle(document);
    }

    private static void ModifyArticle(IDocument document)
    {
        ArticleElements? elements = GetArticleElements(document);
        if (elements is null)
        {
            return;
        }

        IElement layoutContainer = elements.Value.LayoutContainer;
        IElement heading = elements.Value.Heading;
        List<INode> articleBodyNodes = [];
        bool afterHeading = false;

        foreach (INode node in layoutContainer.ChildNodes.ToArray())
        {
            if (node == heading)
            {
                afterHeading = true;
                continue;
            }

            if (afterHeading)
            {
                articleBodyNodes.Add(node);
            }
        }

        IElement articleHeader = document.CreateElement("div");
        articleHeader.ClassList.Add("article-header");
        articleHeader.AppendChild(heading);

        IElement articleContent = document.CreateElement("div");
        articleContent.ClassList.Add("article-content");

        IElement readingArea = document.CreateElement("div");
        readingArea.ClassList.Add("reading-area");
        foreach (INode node in articleBodyNodes)
        {
            readingArea.AppendChild(node);
        }

        articleContent.AppendChild(readingArea);

        layoutContainer.TextContent = string.Empty;
        layoutContainer.AppendChild(articleHeader);
        layoutContainer.AppendChild(articleContent);
    }
}
