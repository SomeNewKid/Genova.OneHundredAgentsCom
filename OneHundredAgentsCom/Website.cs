// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Common.Attributes;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Metadata;
using Genova.Common.Models;
using Genova.Common.Models.Csp;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Endpoints;
using Genova.OneHundredAgentsCom.Html;
using Genova.OneHundredAgentsCom.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genova.OneHundredAgentsCom;

/// <summary>
/// Represents the 100 Agents in 100 Days website, which integrates with the Engine and provides
/// route and cluster configurations.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended to be instantiated by a Host.")]
public sealed class Website : WebsiteBase
{
    /// <summary>
    /// The name which prefixes policy names and view names.
    /// </summary>
    internal const string NamePrefix = "100AGENTSCOM";

    /// <summary>
    /// The name of the cache policy used for output caching.
    /// </summary>
    internal const string CachePolicyName = $"{NamePrefix}_CachePolicy";

    /// <summary>
    /// The tag which may be used for items in the output cache, by which items may be purged from the cache.
    /// </summary>
    internal const string CachePolicyTag = $"{NamePrefix}_CacheTag";

    /// <summary>
    /// The GUID identifier for the OneHundredAgentsCom website.
    /// </summary>
    internal const string Identifier = "e5b3c7a1-2f4d-4a6b-8c9d-1e2f3a4b5c6d";

    /// <summary>
    /// Initializes a new instance of the <see cref="Website"/> class.
    /// </summary>
    /// <param name="configuration">The configuration for the website.</param>
    public Website(IConfiguration configuration)
    {
        WebsiteConfig websiteConfig = new(configuration, WebsiteId.ToString(), "en");
        Name = websiteConfig.Name;
        TenantId = websiteConfig.TenantId;
        Hosts = websiteConfig.Hosts;
        SupportedCultures = websiteConfig.SupportedCultures;
        DefaultCulture = websiteConfig.DefaultCulture;
    }

    /// <inheritdoc/>
    public override string Name { get; }

    /// <inheritdoc/>
    public override Guid WebsiteId => Guid.Parse(Identifier);

    /// <inheritdoc/>
    public override Guid TenantId { get; }

    /// <inheritdoc/>
    public override string[] Hosts { get; }

    /// <inheritdoc/>
    public override string[] SupportedCultures { get; }

    /// <inheritdoc/>
    public override string DefaultCulture { get; }

    /// <inheritdoc/>
    public override IEnumerable<IUrlRedirect> UrlRedirects
    {
        get
        {
            List<IUrlRedirect> redirects = [.. base.UrlRedirects];
            return redirects;
        }
    }

    /// <inheritdoc/>
    public override IEnumerable<KeyValuePair<string, Action<OutputCachePolicyBuilder>>> OutputCachePolicies
    {
        get
        {
            return new List<KeyValuePair<string, Action<OutputCachePolicyBuilder>>>
            {
                new(CachePolicyName, policy =>
                {
                    policy.Expire(TimeSpan.FromMinutes(15));
                }),
            };
        }
    }

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
    }

    /// <inheritdoc/>
    public override void ConfigureMiddleware(WebApplication app)
    {
        base.ConfigureMiddleware(app);
    }

    /// <inheritdoc/>
    public override void ConfigureRoutes(IEndpointRouteBuilder endpoints)
    {
        // Files
        WwwFiles.MapEndpoints(endpoints);
        RobotsText.MapEndpoints(endpoints);

        // General Pages
        HomePage.MapEndpoints(endpoints);
        HelloPages.MapEndpoints(endpoints);
        SitemapPage.MapEndpoints(endpoints);
        HelloScannerPage.MapEndpoints(endpoints);
        HelloUnfriendlyErrorPages.MapEndpoints(endpoints);

        // Catchall
        MarkdownPages.MapEndpoints(endpoints);
    }

    /// <inheritdoc/>
    public override IContentSecurityPolicy GetContentSecurityPolicy(IExecutionContext executionContext, string path)
    {
        ContentSecurityPolicy csp =
            new ContentSecurityPolicy(Disposition.Enforce, executionContext.RequestContext.Nonce)
                       .Add().DefaultSrc().None() // Disallow all resources by default
                       .Add().BaseUri().Self() // Allow the use of the `<base>` element
                       .Add().ScriptSrc().Self().WithNonce() // Only own scripts
                       .Add().StyleSrc().Self().WithNonce() // Only own styles
                       .Add().ImgSrc().Self().Scheme("data") // Only own images
                       .Add().FontSrc().None() // Disallow all fonts
                       .Add().ConnectSrc().Self() // All connections to own endpoints (e.g., AJAX, WebSocket)
                       .Add().MediaSrc().Self() // All media (e.g., audio, video) on own domain
                       .Add().ObjectSrc().None() // Disallow all plugins and objects
                       .Add().FrameSrc().None() // Disallow all iframes
                       .Add().FormAction().None() // Disallow all form submissions
                       .Add().FrameAncestors().None() // Disallow embedding in iframes
                       .Add().UpgradeInsecureRequests() // Upgrade all HTTP requests to HTTPS
                       .Add().BlockAllMixedContent() // Block all mixed content (HTTP content on HTTPS pages)
                       .End();

        return csp;
    }

    /// <inheritdoc/>
    public override IEnumerable<IHtmlModifier> GetHtmlModifiers(string pathAndQuery)
    {
        IEnumerable<IHtmlModifier> baseModifiers = base.GetHtmlModifiers(pathAndQuery);

        IEnumerable<IHtmlModifier> websiteModifiers = new IHtmlModifier[]
        {
            new AriaCurrentNavModifier(),
            new AgentNavigationModifier(),
            new SidebarModifier(),
            new WrapperModifier(),
            new SandboxReportModifier(),
            new ModelEvaluationModifier(),
            new CodingEvaluationModifier(),
            new OcrEvaluationModifier(),
            new AgentArticleModifier(),
            new GeneralArticleModifier(),
        };

        return baseModifiers.Concat(websiteModifiers);
    }

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage(Justification = "Tested by integration tests, and final line difficult to test.")]
    public override string? GetHtmlResponse(HttpContext httpContext, IExecutionContext executionContext, object model)
    {
        if (model is ErrorStatusModel errorStatusModel)
        {
            IRequestContext requestContext = executionContext.RequestContext;

            switch (errorStatusModel.StatusCode)
            {
                case 400:
                    return PageRenderer.RenderMarkdown(requestContext, "/error/400");
                case 401:
                    return PageRenderer.RenderMarkdown(requestContext, "/error/401");
                case 403:
                    return PageRenderer.RenderMarkdown(requestContext, "/error/403");
                case 404:
                    return PageRenderer.RenderMarkdown(requestContext, "/error/404?query=for-testing");
                case 500:
                    return PageRenderer.RenderMarkdown(requestContext, "/error/500");
                case 502:
                    return PageRenderer.RenderMarkdown(requestContext, "/error/502");
                default:
                    break;
            }

            return PageRenderer.RenderMarkdown(requestContext, "/error");
        }

        return base.GetHtmlResponse(httpContext, executionContext, model);
    }

    /// <inheritdoc/>
    protected override ContentMetadata GetMetadataContent(IExecutionContext executionContext)
    {
        return new ContentMetadata
        {
            Description = "A 100-day project documenting 100 small AI agents, each built to explore one practical idea.",
            Keywords = "python, artificial intelligence, ai, agents, agentic",
            Image = "/-/images/logo-opengraph.png",
            Website = "100 Agents in 100 Days",
        };
    }

    /// <inheritdoc/>
    protected override IconSet GetMetadataIconSet(IExecutionContext executionContext)
    {
        return
        [
            new IconResource
            {
                Rel = "icon",
                Src = "/favicon.ico",
                Type = "image/x-icon",
            },
            new IconResource
            {
                Rel = "apple-touch-icon",
                Src = "/apple-touch-icon.png",
                Type = "image/png",
                Sizes = "180x180",
            },
        ];
    }
}
