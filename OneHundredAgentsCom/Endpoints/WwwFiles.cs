// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.Theme.Scripts;
using Genova.Theme.Styles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping file-related endpoints.
/// </summary>
internal static class WwwFiles
{
    /// <summary>
    /// Maps the file-related endpoints to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the file-related endpoints.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Map endpoints for supported file extensions
        MapImagesEndpoints(endpoints, "ico");
        MapImagesEndpoints(endpoints, "png");
        MapImagesEndpoints(endpoints, "jpg");
        MapImagesEndpoints(endpoints, "svg");
        MapStylesEndpoints(endpoints, "css");
        MapScriptsEndpoints(endpoints, "js");
    }

    /// <summary>
    /// Normalizes a filename in a URL to the name of an embedded resource.
    /// </summary>
    /// <param name="filename">The filename in a URL.</param>
    /// <returns>The name of the embedded resource.</returns>
    internal static string? NormalizeFilename(string? filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return filename;
        }

        return filename.Replace('.', '~');
    }

    /// <summary>
    /// Normalizes a path in a URL to the path of an embedded resource.
    /// </summary>
    /// <param name="path">The path in a URL.</param>
    /// <returns>The path of the embedded resource.</returns>
    internal static string NormalizeFilePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        // Split into segments
        string[] segments = path.Split('/');

        // If the path starts with '/', the first segment will be empty
        int last = segments.Length - 1;
        for (int i = 1; i < last; i++)
        {
            if (segments[i] == "-")
            {
                segments[i] = "__";
            }
            else
            {
                segments[i] = segments[i].Replace("-", "_");
            }
        }

        // Recombine segments with '/'
        return string.Join("/", segments);
    }

    /// <summary>
    /// Maps image-related endpoints for a specific file extension and path template.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the endpoint.</param>
    /// <param name="extension">The file extension to map (e.g., "ico", "png").</param>
    private static void MapImagesEndpoints(IEndpointRouteBuilder endpoints, string extension)
    {
        endpoints.MapGet($"/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = GetFilenameRouteParameter(httpContext);
            string pathTemplate = "wwwroot/{0}.{1}";
            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/images/heroes/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = GetFilenameRouteParameter(httpContext);
            string pathTemplate = "wwwroot/-/images/heroes/{0}.{1}";
            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/images/thumbnails/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = GetFilenameRouteParameter(httpContext);
            string pathTemplate = "wwwroot/-/images/thumbnails/{0}.{1}";
            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/images/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = GetFilenameRouteParameter(httpContext);
            string pathTemplate = "wwwroot/-/images/{0}.{1}";
            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Maps stylesheet-related endpoints for a specific file extension and path template.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the endpoint.</param>
    /// <param name="extension">The file extension to map (e.g., "css").</param>
    private static void MapStylesEndpoints(IEndpointRouteBuilder endpoints, string extension)
    {
        string pathTemplate = "wwwroot/-/styles/{0}.{1}";

        endpoints.MapGet($"/-/styles/global.{extension}", (HttpContext httpContext) =>
        {
            string styleSheet = GetGlobalStylesheet();
            string contentType = FileHelper.GetContentType("global." + extension);

            return Results.Content(styleSheet, contentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/styles/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = GetFilenameRouteParameter(httpContext);

            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/styles/bundled.{extension}", (HttpContext httpContext) =>
        {
            string global = GetGlobalStylesheet();
            string website = GetFileFromTemplate(pathTemplate, "onehundredagents", extension) ?? string.Empty;
            string contentType = FileHelper.GetContentType("bundled." + extension);

            return Results.Content($"{global}\n{website}", contentType);
        })
        .CacheOutput(Website.CachePolicyName).WithTags(Website.CachePolicyTag)
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Maps JavaScript-related endpoints for a specific file extension and path template.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the endpoint.</param>
    /// <param name="extension">The file extension to map (e.g., "css").</param>
    private static void MapScriptsEndpoints(IEndpointRouteBuilder endpoints, string extension)
    {
        string pathTemplate = "wwwroot/-/scripts/{0}.{1}";

        endpoints.MapGet($"/-/scripts/plain.{extension}", (HttpContext httpContext) =>
        {
            string scriptSheet = GetGlobalScript();
            string contentType = FileHelper.GetContentType("plain." + extension);

            return Results.Content(scriptSheet, contentType);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/scripts/{{filename}}.{extension}", (HttpContext httpContext) =>
        {
            string? filename = GetFilenameRouteParameter(httpContext);

            return ServeFileFromTemplate(httpContext, pathTemplate, filename, extension);
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));

        endpoints.MapGet($"/-/scripts/bundled.{extension}", (HttpContext httpContext) =>
        {
            string global = GetGlobalScript();
            string website = GetFileFromTemplate(pathTemplate, "onehundredagents", extension) ?? string.Empty;
            string contentType = FileHelper.GetContentType("bundled." + extension);

            return Results.Content($"{global}\n{website}", contentType);
        })
        .CacheOutput(Website.CachePolicyName).WithTags(Website.CachePolicyTag)
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    /// <summary>
    /// Serves an embedded file based on the provided path template and extension.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="pathTemplate">The template for constructing the embedded file path.</param>
    private static IResult ServeFileFromTemplate(HttpContext context, string pathTemplate)
    {
        string normalizedPath = NormalizeFilePath(pathTemplate);

        if (!string.IsNullOrWhiteSpace(normalizedPath))
        {
            // Construct the embedded file path using the template
            string embeddedFilePath = string.Format(normalizedPath, normalizedPath);

            // Serve the embedded file
            FileHelper.ServeEmbeddedFileAsync(context, typeof(Website), embeddedFilePath).GetAwaiter().GetResult();
            return Results.Empty;
        }
        else
        {
            // Return 404 if the filename is missing
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Results.Empty;
        }
    }

    /// <summary>
    /// Serves an embedded file based on the provided path template and extension.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="pathTemplate">The template for constructing the embedded file path.</param>
    /// <param name="filename">The file name of the requested file.</param>
    /// <param name="extension">The file extension of the requested file.</param>
    private static IResult ServeFileFromTemplate(
        HttpContext context, string pathTemplate, string? filename, string extension)
    {
        string normalizedPath = NormalizeFilePath(pathTemplate);
        string? normalizedFilename = NormalizeFilename(filename);

        if (!string.IsNullOrWhiteSpace(normalizedFilename))
        {
            // Construct the embedded file path using the template
            string embeddedFilePath = string.Format(normalizedPath, normalizedFilename, extension);

            // Serve the embedded file
            FileHelper.ServeEmbeddedFileAsync(context, typeof(Website), embeddedFilePath).GetAwaiter().GetResult();
            return Results.Empty;
        }
        else
        {
            // Return 404 if the filename is missing
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Results.Empty;
        }
    }

    /// <summary>
    /// Returns an embedded file based on the provided path template and extension.
    /// </summary>
    /// <param name="pathTemplate">The template for constructing the embedded file path.</param>
    /// <param name="filename">The file name of the requested file.</param>
    /// <param name="extension">The file extension of the requested file.</param>
    private static string? GetFileFromTemplate(string pathTemplate, string? filename, string extension)
    {
        string normalizedPath = NormalizeFilePath(pathTemplate);
        string? normalizedFilename = NormalizeFilename(filename);

        if (!string.IsNullOrWhiteSpace(normalizedFilename))
        {
            // Construct the embedded file path using the template
            string embeddedFilePath = string.Format(normalizedPath, normalizedFilename, extension);

            string? content = null;
            Stream? stream = FileHelper.GetEmbeddedResourceStream(typeof(Website), embeddedFilePath);
            if (stream != null)
            {
                using (StreamReader reader = new(stream))
                {
                    content = reader.ReadToEnd();
                }
            }

            return content;
        }

        return null;
    }

    private static string? GetExtensionRouteParameter(HttpContext httpContext)
    {
        return httpContext.Request.RouteValues["extension"] as string;
    }

    private static string? GetFilenameRouteParameter(HttpContext httpContext)
    {
        return httpContext.Request.RouteValues["filename"] as string;
    }

    private static string GetGlobalStylesheet()
    {
        StyleOptions styleOptions = new()
        {
            Commentary = false,
            UseExternalLinkIcon = true,
            UseStickyFooter = true,
            UseSmoothScrolling = true,

            BodyFontStack = FontStack.SystemUI,
            HeadingFontStack = FontStack.SystemUI,
            HeadingFontWeight = FontWeight.Bold,
            ArticleFontStack = FontStack.Transitional,
            ArticleFontSelectors = new[] { "main article", "main .content" },

            UseLayoutColumns = false,
            LargeBreakpoint = 1024,

            LightTheme = new StyleTheme
            {
                Foreground = "#282e31",
                Background = "#e2ede7",
                LinkColor = "#9c4a26",
            },

            DarkTheme = new StyleTheme
            {
                Foreground = "#d6e6dd",
                Background = "#282e31",
                LinkColor = "#e8a077",
            },
        };

        StyleBuilder styleBuilder = new(styleOptions);
        string styleSheet = styleBuilder.BuildPlain();
        return styleSheet;
    }

    private static string GetGlobalScript()
    {
        ScriptOptions scriptOptions = new()
        {
            Commentary = true,
            IncludeHeaderMenuButton = false,
            HamburgerHeaderMenuButton = false,
            IncludeThemeToggler = true,
        };
        ScriptBuilder scriptBuilder = new(scriptOptions);
        string scriptSheet = scriptBuilder.BuildPlain();
        return scriptSheet;
    }
}
