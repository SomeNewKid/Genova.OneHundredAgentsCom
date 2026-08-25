// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Provides shared helpers for article HTML modifiers.
/// </summary>
internal abstract class BaseArticleModifier : IHtmlModifier
{
    private string? _path;

    /// <summary>
    /// Gets the normalized path for the current request.
    /// </summary>
    protected string CurrentPath => NormalizePath(_path);

    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
        string pathAndQuery = executionContext.RequestContext.PathAndQuery;
        _path = pathAndQuery.Split('?')[0];
    }

    /// <inheritdoc/>
    public abstract void Modify(IDocument document);

    /// <summary>
    /// Gets the primary article elements from the rendered document.
    /// </summary>
    /// <param name="document">The HTML document to inspect.</param>
    /// <returns>The article elements, or <see langword="null"/> if the expected structure was not found.</returns>
    protected static ArticleElements? GetArticleElements(IDocument document)
    {
        IElement? article = document.QuerySelector("main#main > article");
        if (article is null)
        {
            return null;
        }

        IElement? layoutContainer = GetDirectChildByClassName(article, "layout-container");
        IElement? heading = GetDirectChildByTagName(layoutContainer, "h1");
        if (layoutContainer is null || heading is null)
        {
            return null;
        }

        return new ArticleElements(layoutContainer, heading);
    }

    /// <summary>
    /// Gets the agent entry associated with the current request path.
    /// </summary>
    /// <param name="currentPath">The normalized path for the current request.</param>
    /// <returns>The matching agent entry, or <see langword="null"/> if the path is not an agent page.</returns>
    protected static AgentEntry? GetCurrentAgent(string currentPath)
    {
        AgentCatalogue catalogue = new();

        foreach (AgentGroup group in catalogue.Groups)
        {
            AgentEntry? agent = group.Agents.Find(agent =>
                string.Equals($"/{agent.Slug}", currentPath, StringComparison.OrdinalIgnoreCase));

            if (agent is not null)
            {
                return agent;
            }
        }

        return null;
    }

    /// <summary>
    /// Gets the first direct child of an element with the specified class name.
    /// </summary>
    /// <param name="element">The element to inspect.</param>
    /// <param name="className">The class name to match.</param>
    /// <returns>The matching child element, or <see langword="null"/> if no child matches.</returns>
    protected static IElement? GetDirectChildByClassName(IElement? element, string className)
    {
        if (element is null)
        {
            return null;
        }

        foreach (IElement child in element.Children)
        {
            if (child.ClassList.Contains(className))
            {
                return child;
            }
        }

        return null;
    }

    /// <summary>
    /// Determines whether any node has non-whitespace content.
    /// </summary>
    /// <param name="nodes">The nodes to inspect.</param>
    /// <returns><see langword="true"/> if any node contains non-whitespace content; otherwise, <see langword="false"/>.</returns>
    protected static bool HasNonWhitespaceContent(IEnumerable<INode> nodes)
    {
        foreach (INode node in nodes)
        {
            if (node is IText text)
            {
                if (!string.IsNullOrWhiteSpace(text.Data))
                {
                    return true;
                }

                continue;
            }

            return true;
        }

        return false;
    }

    private static IElement? GetDirectChildByTagName(IElement? element, string tagName)
    {
        if (element is null)
        {
            return null;
        }

        foreach (IElement child in element.Children)
        {
            if (string.Equals(child.LocalName, tagName, StringComparison.OrdinalIgnoreCase))
            {
                return child;
            }
        }

        return null;
    }

    private static string NormalizePath(string? path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return "/";
        }

        string p = path.Split('?')[0];
        p = p.StartsWith('/') ? p : "/" + p;

        if (p.Length > 1 && p.EndsWith('/'))
        {
            p = p.TrimEnd('/');
        }

        return p;
    }

    /// <summary>
    /// Represents the common elements needed to reshape an article page.
    /// </summary>
    protected readonly struct ArticleElements
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleElements"/> struct.
        /// </summary>
        /// <param name="layoutContainer">The article layout container element.</param>
        /// <param name="heading">The article heading element.</param>
        public ArticleElements(IElement layoutContainer, IElement heading)
        {
            LayoutContainer = layoutContainer;
            Heading = heading;
        }

        /// <summary>
        /// Gets the article layout container element.
        /// </summary>
        public IElement LayoutContainer { get; }

        /// <summary>
        /// Gets the article heading element.
        /// </summary>
        public IElement Heading { get; }
    }
}
