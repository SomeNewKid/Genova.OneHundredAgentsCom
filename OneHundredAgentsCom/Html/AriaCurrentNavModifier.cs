// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Websites;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// A modifier which adds an ARIA `current` attribute to the main navigation of the HTML document.
/// </summary>
internal sealed class AriaCurrentNavModifier : IHtmlModifier
{
    private string? _path;

    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
        // Extract the path (without query string) from PathAndQuery.
        string pathAndQuery = executionContext.RequestContext.PathAndQuery;
        _path = pathAndQuery.Split('?')[0];
    }

    /// <inheritdoc/>
    public void Modify(IDocument document)
    {
        string[] navSelectors = ["#header nav"];
        foreach (string navSelector in navSelectors)
        {
            IElement? navElement = document.QuerySelector(navSelector);
            if (navElement is null)
            {
                continue;
            }

            ModifyNav(navElement);
        }
    }

    /// <summary>
    /// Determines whether a navigation link should receive <c>aria-current="location"</c>
    /// based on the current request path and the link's <c>href</c> attribute.
    /// </summary>
    /// <param name="path">
    /// The current request path, excluding any query string (e.g., <c>/section/page</c>).
    /// </param>
    /// <param name="href">
    /// The <c>href</c> attribute of the navigation link to evaluate.
    /// </param>
    /// <returns>
    /// <c>true</c> if the <paramref name="href"/> is a strict parent section of the <paramref name="path"/>,
    /// meaning <paramref name="path"/> starts with <paramref name="href"/> followed by a <c>/</c>,
    /// and <paramref name="href"/> is not the homepage (<c>/</c>); otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsAriaCurrentLocation(string path, string href)
    {
        if (string.IsNullOrEmpty(href) || href == "/")
        {
            return false;
        }

        if (path.Length > href.Length &&
            path.StartsWith(href, StringComparison.OrdinalIgnoreCase) &&
            path[href.Length] == '/')
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Retrieves the value of the specified attribute from the given HTML element.
    /// </summary>
    /// <param name="element">
    /// The <see cref="IElement"/> from which to retrieve the attribute value.
    /// </param>
    /// <param name="attributeName">
    /// The name of the attribute whose value should be retrieved.
    /// </param>
    /// <returns>
    /// The value of the specified attribute if it exists; otherwise, an empty string.
    /// </returns>
    // <notes>
    // This method is added to make it easier to achieve full code coverage.
    // </notes>
    internal static string GetAttributeValue(IElement element, string attributeName)
    {
        return element.GetAttribute(attributeName) ?? "";
    }

    private void ModifyNav(IElement navElement)
    {
        // If any link already has aria-current="page" or aria-current="location", do nothing.
        if (navElement.QuerySelector("a[aria-current=\"page\"],a[aria-current=\"location\"]") is not null)
        {
            return;
        }

        if (string.IsNullOrEmpty(_path))
        {
            return;
        }

        // Find all <a> elements in the nav
        IHtmlCollection<IElement> links = navElement.QuerySelectorAll("a[href]");

        // Try to find an exact match for the current page
        foreach (IElement link in links)
        {
            string? href = link.GetAttribute("href");
            if (string.Equals(href, _path, StringComparison.OrdinalIgnoreCase))
            {
                link.SetAttribute("aria-current", "page");
                return;
            }
        }

        // Try to find a parent section match (strict prefix match, not "/")
        foreach (IElement link in links)
        {
            string href = GetAttributeValue(link, "href");
            if (IsAriaCurrentLocation(_path, href))
            {
                link.SetAttribute("aria-current", "location");
                return;
            }
        }
    }
}
