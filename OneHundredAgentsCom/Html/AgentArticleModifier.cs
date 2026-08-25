// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Applies post-processing to rendered agent article pages.
/// </summary>
internal sealed class AgentArticleModifier : BaseArticleModifier
{
    /// <inheritdoc/>
    public override void Modify(IDocument document)
    {
        AgentEntry? agent = GetCurrentAgent(CurrentPath);
        if (agent is null)
        {
            return;
        }

        ModifyArticle(document, agent);
    }

    private static void ModifyArticle(IDocument document, AgentEntry agent)
    {
        ArticleElements? elements = GetArticleElements(document);
        if (elements is null)
        {
            return;
        }

        IElement layoutContainer = elements.Value.LayoutContainer;
        IElement heading = elements.Value.Heading;
        IElement? sidebar = GetDirectChildByClassName(layoutContainer, "sidebar");
        if (sidebar is null)
        {
            return;
        }

        List<INode> articleBodyNodes = [];
        List<INode> articleArtifactNodes = [];
        bool afterHeading = false;
        bool afterSidebar = false;

        foreach (INode node in layoutContainer.ChildNodes.ToArray())
        {
            if (node == heading)
            {
                afterHeading = true;
                continue;
            }

            if (node == sidebar)
            {
                afterSidebar = true;
                continue;
            }

            if (afterSidebar)
            {
                articleArtifactNodes.Add(node);
            }
            else if (afterHeading)
            {
                articleBodyNodes.Add(node);
            }
        }

        IElement articleHeader = document.CreateElement("div");
        articleHeader.ClassList.Add("article-header");

        IElement agentNumber = document.CreateElement("div");
        agentNumber.ClassList.Add("agent-number");
        agentNumber.TextContent = $"Agent {agent.Number}";
        articleHeader.AppendChild(agentNumber);

        articleHeader.AppendChild(heading);

        IElement agentDescription = document.CreateElement("p");
        agentDescription.ClassList.Add("agent-description");
        agentDescription.TextContent = agent.Description;
        articleHeader.AppendChild(agentDescription);

        IElement articleContent = document.CreateElement("div");
        articleContent.ClassList.Add("layout-cols-3-1 article-content");

        IElement articleBody = document.CreateElement("div");
        articleBody.ClassList.Add("article-body");

        IElement readingArea = document.CreateElement("div");
        readingArea.ClassList.Add("reading-area");
        foreach (INode node in articleBodyNodes)
        {
            readingArea.AppendChild(node);
        }

        articleBody.AppendChild(readingArea);
        articleContent.AppendChild(articleBody);
        articleContent.AppendChild(sidebar);

        layoutContainer.TextContent = string.Empty;
        layoutContainer.AppendChild(articleHeader);
        layoutContainer.AppendChild(articleContent);

        if (HasNonWhitespaceContent(articleArtifactNodes))
        {
            IElement articleArtifact = document.CreateElement("div");
            articleArtifact.ClassList.Add("article-artifact");
            foreach (INode node in articleArtifactNodes)
            {
                articleArtifact.AppendChild(node);
            }

            layoutContainer.AppendChild(articleArtifact);
        }
    }
}
