// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Markdown;
using Genova.Common.Utilities;
using Genova.OneHundredAgentsCom.Endpoints;
using Genova.Theme.Styles;

namespace Genova.OneHundredAgentsCom.Utilities;

/// <summary>
/// Provides methods for rendering Markdown pages as HTML, including support for essential and non-essential resources
/// based on query string parameters. This class is used to generate HTML output from Markdown content and apply
/// appropriate templates and resource loading logic.
/// </summary>
internal static class PageRenderer
{
    /// <summary>
    /// Indicates whether theme-specific images are used for hero images.
    /// </summary>
    internal const bool ThemeSpecificImages = false;

    /// <summary>
    /// A cache buster value to append to static resource URLs, ensuring that clients fetch the latest versions
    /// when this value is changed.
    /// </summary>
    internal const string CacheBuster = "3";

    private const bool NakedPage = false;

    /// <summary>
    /// Renders a Markdown page as HTML using the specified path and query string.
    /// </summary>
    /// <param name="requestContext">The context of the current HTTP request.</param>
    /// <param name="pathAndQuery">
    /// The path and query string of the requested page, combined as a single string (e.g., <c>/about?essential</c>).
    /// </param>
    /// <returns>
    /// The rendered HTML as a string if the Markdown content is found; otherwise, <c>null</c>.
    /// </returns>
    internal static string? RenderMarkdown(IRequestContext requestContext, string pathAndQuery)
    {
        string[] splits = pathAndQuery.Split('?');
        string path = splits[0];
        string query = splits.Length > 1 ? splits[1] : "";
        return RenderMarkdown(requestContext, path, query);
    }

    /// <summary>
    /// Renders a Markdown page as HTML using the specified path and query string components.
    /// </summary>
    /// <param name="requestContext">The context of the current HTTP request.</param>
    /// <param name="path">The path of the requested page (e.g., <c>/about</c>).</param>
    /// <param name="query">The query string of the request, without the leading <c>?</c> (e.g., <c>essential</c>).
    /// </param>
    /// <returns>
    /// The rendered HTML as a string if the Markdown content is found; otherwise, <c>null</c>.
    /// </returns>
    internal static string? RenderMarkdown(IRequestContext requestContext, string path, string query)
    {
        string? markdown = GetMarkdown(path);
        if (string.IsNullOrEmpty(markdown))
        {
            return null;
        }

        MarkdownDocument? markdownDocument = MarkdownDocument.Parse(markdown);
        if (markdownDocument == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(markdownDocument.Content))
        {
            return null;
        }

        string? heroImage = GetHeroImage(path, markdownDocument);

        string template = GetHtmlTemplate(requestContext, query, bodyClass: null, heroImageName: heroImage);
        string[] splits = template.Split("{article}");
        string opening = splits[0];
        string closing = splits[1];

        string html = new HtmlBuilder()
            .Append(opening)
            .AppendMarkdown(markdownDocument.Content)
            .Append(closing).ToString();

        return html;
    }

    /// <summary>
    /// Renders a Markdown page as HTML using the specified path and query string components.
    /// </summary>
    /// <param name="requestContext">The context of the current HTTP request.</param>
    /// <param name="html">The HTML to render.</param>
    /// <param name="query">The query string of the request, without the leading <c>?</c> (e.g., <c>essential</c>).
    /// </param>
    /// <param name="bodyClass">The CSS class to apply to the document body.</param>
    /// <param name="heroImageName">The name of the hero image to use.</param>
    /// <returns>
    /// The rendered HTML as a string if the Markdown content is found; otherwise, <c>null</c>.
    /// </returns>
    internal static string? RenderHtml(
        IRequestContext requestContext,
        string html,
        string query,
        string? bodyClass = null,
        string? heroImageName = null)
    {
        string template = GetHtmlTemplate(requestContext, query, bodyClass, heroImageName);
        string[] splits = template.Split("{article}");

        return new HtmlBuilder()
            .Append(splits[0])
            .Append(html)
            .Append(splits[1]).ToString();
    }

    /// <summary>
    /// Determines whether the specified query string contains the <c>essential</c> parameter.
    /// </summary>
    /// <param name="query">The query string to evaluate (without the leading <c>?</c>).</param>
    /// <returns>
    /// <c>true</c> if the query string contains the <c>essential</c> parameter; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method returns <c>true</c> if the query string includes the <c>essential</c> key, otherwise <c>false</c>.
    /// </remarks>
    internal static bool HasEssentialQuery(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return false;
        }

        // Remove leading '?', if present
        if (query.StartsWith('?'))
        {
            query = query.Substring(1);
        }

        if (string.IsNullOrEmpty(query))
        {
            return false;
        }

        string[] parts = query.Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            // Accept "essential" or "essential=..." but not "essentials"
            int equalIndex = part.IndexOf('=');
            string key = equalIndex >= 0 ? part.Substring(0, equalIndex) : part;
            if (key.Equals("essential", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Retrieves the Markdown content for the specified path.
    /// </summary>
    /// <param name="path">The URL path.</param>
    /// <returns>The Markdown for the web page at the specified URL path.</returns>
    internal static string? GetMarkdown(string path)
    {
        return GetMarkdown(path, false) ?? GetMarkdown(path, true);
    }

    private static string GetHtmlTemplate(
        IRequestContext requestContext, string query, string? bodyClass = null, string? heroImageName = null)
    {
        StyleOptions styleOptions = new() { Commentary = false, Condense = true };
        StyleBuilder styleBuilder = new(styleOptions);
        string essentialCss = styleBuilder.BuildEssential();

        bool hasEssentialQuery = NakedPage || HasEssentialQuery(query);
        string nonEssentialCss = hasEssentialQuery ? "" :
            """
              <link rel="stylesheet" nonce="{nonce}" href="/-/styles/bundled.css">            
            """;
        string nonEssentialJs = hasEssentialQuery ? "" :
            """
              <script nonce="{nonce}" src="/-/scripts/bundled.js" defer></script>            
            """;

        string bodyClassAttribute = string.IsNullOrEmpty(bodyClass) ? "" : $" class=\"{bodyClass}\"";

        if (string.IsNullOrEmpty(heroImageName))
        {
            string requestedPath = requestContext.PathAndQuery.Split('?')[0];
            string[] pathSlugs = requestedPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string lastSlug = pathSlugs.Length > 0 ? pathSlugs[^1] : "home";
            heroImageName = lastSlug.ToLower();
        }

        string buster = string.IsNullOrEmpty(CacheBuster) ? "" : $"?v={CacheBuster}";

        StringBuilder html = new();

        html.AppendLine("""
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width,initial-scale=1">
  <title></title>
  <style nonce="{nonce}">
{essentialCss}
  </style>
  {nonEssentialCss}
""");

        html.AppendLine("""
</head>
<body{bodyClassAttribute}>
  <!-- Empty anchor so "Back to the top" never loses its target. -->
  <div id="top"></div>

  <div id="skip-links" class="skip-links">
    <a href="#main" class="skip-link">Skip to main content</a>
  </div>

  <header id="header">
    <div class="masthead">
      <div class="layout-container">
        <div class="site-title">
          <a href="/" class="naked" title="100 Agents in 100 Days">100 Agents in 100 Days</a>
        </div>
      </div>
    </div>
  </header>

  <hr class="visually-hidden"/>

  <div class="hero-image">
      <div class="layout-container">
        <picture class="hero">
          <img src="/-/images/heroes/{heroImageName}.jpg{buster}" alt="" role="presentation" width="988" height="329">
        </picture>
      </div>
  </div>

  <main id="main">
    <article>
      <div class="layout-container">
        {article}
      </div>
    </article>
  </main>
  <div class="scroll-links">
    <p class="layout-container">
      <a href="#top" class="back-to-top"> <span class="icon" aria-hidden="true"></span>
      <span class="label">Back to the top</span> </a>
    </p>
  </div>

  <hr class="visually-hidden"/>

  <footer id="footer">
    <div class="layout-container">
      <div class="footline">
        <span>&copy;&nbsp;{year}</span>
   
        <nav aria-label="Footer links">
          <ul class="inline-links">
            <li><a href="/notices">Notices</a></li>
            <li><a href="/sitemap">Sitemap</a></li>
          </ul>
        </nav>
      </div>

      <section aria-labelledby="notices-heading">
        <h2 id="notices-heading" class="visually-hidden">Footer notices</h2>
        <div class="smallprint reading-area">
          <p>The words and images on this site were largely generated with OpenAI’s ChatGPT and Codex, and the design was inspired by a layout generated by Anthropic’s Claude.</p>
          <p>Generative AI was used deliberately: this site is part of an experiment in learning what these tools can actually do.</p>
        </div>
        <div class="cc-license">
          <a rel="license" href="https://creativecommons.org/licenses/by-nc/4.0/" title="Licensed under a Creative Commons Attribution-NonCommercial 4.0 International License">
            <span class="cc-icon cc-icon--cc" aria-hidden="true"></span>
            <span class="cc-icon cc-icon--by" aria-hidden="true"></span>
            <span class="cc-icon cc-icon--nc" aria-hidden="true"></span>
            <span class="visually-hidden">This work is licensed under a Creative Commons Attribution-NonCommercial 4.0 International License</span>
          </a>
        </div>
      </section>

      <div id="theme-toggle"></div>
    </div>
  </footer>
{nonEssentialJs}
</body>
</html> 
""");

        // Replace markers
        string result = html.ToString()
            .Replace("{essentialCss}", essentialCss)
            .Replace("{nonEssentialCss}", nonEssentialCss)
            .Replace("{nonEssentialJs}", nonEssentialJs)
            .Replace("{bodyClassAttribute}", bodyClassAttribute)
            .Replace("{nonce}", requestContext.Nonce)
            .Replace("{year}", DateTime.Today.Year.ToString())
            .Replace("{heroImageName}", heroImageName)
            .Replace("{buster}", buster);

        return result.Trim();
    }

    private static string? GetHeroImage(string path, MarkdownDocument? markdownDocument)
    {
        // Normalize the incoming path for dictionary lookup and potential parent traversal.
        string normalizedPath = NormalizePathForLookup(path);

        if (markdownDocument is null)
        {
            return null;
        }

        // 2) First check the current page's metadata.
        string? heroImage = GetHeroImage(markdownDocument);
        if (!string.IsNullOrEmpty(heroImage))
        {
            return heroImage;
        }

        // Walk up the URL path, inspecting each parent (including root) for a hero image.
        string current = normalizedPath;
        while (true)
        {
            // Determine parent path
            int lastSlash = current.LastIndexOf('/');
            string parent;
            if (lastSlash <= 0)
            {
                parent = "/";
            }
            else
            {
                parent = current.Substring(0, lastSlash);
                if (string.IsNullOrEmpty(parent))
                {
                    parent = "/";
                }
            }

            // If we've reached the same segment as before, stop.
            if (parent == current)
            {
                break;
            }

            current = parent;

            // Attempt to load the parent's markdown (GetMarkdown already tries index.md fallback).
            string? parentMarkdown = GetMarkdown(current);
            if (string.IsNullOrEmpty(parentMarkdown))
            {
                // No markdown for this parent; continue up.
                if (current == "/")
                {
                    break;
                }

                continue;
            }

            MarkdownDocument? parentDoc = MarkdownDocument.Parse(parentMarkdown);
            if (parentDoc is null)
            {
                if (current == "/")
                {
                    break;
                }

                continue;
            }

            string? parentHero = GetHeroImage(parentDoc);
            if (!string.IsNullOrEmpty(parentHero))
            {
                return parentHero;
            }

            // If we've reached root, stop.
            if (current == "/")
            {
                break;
            }
        }

        // No parent specified image found.
        return null;
    }

    private static string? GetHeroImage(MarkdownDocument? markdownDocument)
    {
        if (markdownDocument is null)
        {
            return null;
        }

        // Return the value of the "image" metadata entry if present, otherwise null.
        return markdownDocument.Metadata.TryGetValue("image", out string? value) ? value : null;
    }

    private static string? GetMarkdown(string path, bool useIndex)
    {
        string separator = path.Length > 1 ? "/" : "";
        string filename = useIndex ? $"wwwroot{path}.md" : $"wwwroot{path}{separator}index.md";
        return GetEmbeddedText(filename);
    }

    private static string? GetEmbeddedText(string filename)
    {
        string normalizedPath = WwwFiles.NormalizeFilePath(filename);

        string? content = null;
        Stream? stream = FileHelper.GetEmbeddedResourceStream(typeof(Website), normalizedPath);
        if (stream != null)
        {
            using (StreamReader reader = new(stream))
            {
                content = reader.ReadToEnd();
            }
        }

        return content;
    }

    private static string NormalizePathForLookup(string? path)
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
}
