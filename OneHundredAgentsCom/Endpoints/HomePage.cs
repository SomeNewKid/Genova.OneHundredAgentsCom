// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Common.Execution;
using Genova.Common.Utilities;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;
using Genova.OneHundredAgentsCom.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Genova.OneHundredAgentsCom.Endpoints;

/// <summary>
/// Provides methods for mapping the home page endpoint.
/// </summary>
internal static class HomePage
{
    private const string HeroImage = "home";

    /// <summary>
    /// Maps the home page endpoint to the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to which to add the home page endpoint.</param>
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", (HttpContext httpContext, IExecutionContextAccessor executionContextAccessor) =>
        {
            IExecutionContext executionContext = executionContextAccessor.Current;

            string query = StringHelper.GetNonNullValue(httpContext.Request.QueryString.Value);

            StringBuilder builder = new();
            builder.AppendLine("<h1>100 Agents in 100 Days</h1>");
            builder.AppendLine(
                "<p>This project is what happens when over-planning is asked politely to leave the room. Instead of designing the perfect AI agent architecture, the goal is to build 100 small agents in 100 days. Most are deliberately simple and focused on one agentic technology.</p>");
            builder.AppendLine(
                "<p>Here they are: the useful agents, the odd agents, and the agents who made a strong case for better supervision.</p>");

            AgentCatalogue catalogue = new();
            AppendAgentGroupsHtml(builder, catalogue);

            string fragment = builder.ToString();

            string? html = PageRenderer.RenderHtml(executionContext.RequestContext, fragment, query, null, HeroImage);

            return Results.Text(html, "text/html");
        })
        .WithMetadata(new WebsiteRouteInfo(Website.Identifier));
    }

    private static void AppendAgentGroupsHtml(StringBuilder builder, AgentCatalogue catalogue)
    {
        int agentCount = catalogue.Groups.Sum(g => g.Agents.Count);
        builder.AppendLine("<p><strong><em>There are current " + agentCount + " agents in the catalogue.</em></strong></p>");
        foreach (AgentGroup group in catalogue.Groups)
        {
            if (group.Agents.Count == 0)
            {
                continue;
            }

            builder.AppendLine("<div class=\"agent-group\">");
            builder.AppendLine($"<h2>{group.Title}</h2>");
            builder.AppendLine($"<p>{group.Description}</p>");
            builder.AppendLine("<ul>");

            foreach (AgentEntry agent in group.Agents)
            {
                builder.Append("<li>");
                builder.Append($"<a href=\"/{agent.Slug}\">");
                builder.Append($"<img src=\"/-/images/thumbnails/{agent.Slug}.jpg\" alt=\"\" role=\"presentation\">");
                builder.Append($"<span>{agent.Name}</span>");
                builder.AppendLine("</a></li>");
            }

            builder.AppendLine("</ul>");
            builder.AppendLine("</div>");
        }
    }
}
