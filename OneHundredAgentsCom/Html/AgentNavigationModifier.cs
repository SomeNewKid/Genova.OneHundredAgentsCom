// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Injects previous and next hyperlinks after the &lt;main&gt; element on agent pages.
/// </summary>
internal sealed class AgentNavigationModifier : IHtmlModifier
{
    private string? _path;

    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
        string pathAndQuery = executionContext.RequestContext.PathAndQuery;
        _path = pathAndQuery.Split('?')[0];
    }

    /// <inheritdoc/>
    public void Modify(IDocument document)
    {
        string currentPath = NormalizePath(_path);
        if (currentPath == "/")
        {
            return;
        }

        AgentLinkPair pair = FindPreviousAndNextAgent(currentPath);
        if (pair.Previous is null && pair.Next is null)
        {
            return;
        }

        BuildAgentNavigation(document, pair.Previous, pair.Next);
        AddBodyClass(document);
    }

    private static void AddBodyClass(IDocument document)
    {
        document.Body!.ClassList.Add("has-agent-navigation");
    }

    private static AgentLinkPair FindPreviousAndNextAgent(string currentAgentPath)
    {
        List<AgentEntry> agents = GetAgents();
        int agentIndex = agents.FindIndex(agent =>
            string.Equals($"/{agent.Slug}", currentAgentPath, StringComparison.OrdinalIgnoreCase));

        if (agentIndex < 0)
        {
            return new AgentLinkPair(null, null);
        }

        AgentEntry? previous = agentIndex > 0 ? agents[agentIndex - 1] : null;
        AgentEntry? next = agentIndex < agents.Count - 1 ? agents[agentIndex + 1] : null;

        return new AgentLinkPair(previous, next);
    }

    private static List<AgentEntry> GetAgents()
    {
        AgentCatalogue catalogue = new();
        List<AgentEntry> agents = [];

        foreach (AgentGroup group in catalogue.Groups)
        {
            agents.AddRange(group.Agents);
        }

        return agents;
    }

    private static void BuildAgentNavigation(IDocument document, AgentEntry? previous, AgentEntry? next)
    {
        IElement wrapper = document.CreateElement("div");
        wrapper.ClassList.Add("agent-navigation-links");

        IElement nav = document.CreateElement("nav");
        nav.SetAttribute("aria-label", "Agent navigation");
        nav.ClassList.Add("layout-container");

        IElement agentNav = document.CreateElement("div");
        agentNav.ClassList.Add("agent-nav");

        IElement previousCard = BuildAgentCard(
            document,
            "previous-agent",
            "agent-card",
            previous,
            directionLabel: "Previous",
            linkClass: "prev-agent");

        IElement nextCard = BuildAgentCard(
            document,
            "next-agent",
            "agent-card",
            next,
            directionLabel: "Next",
            linkClass: "next-agent");

        agentNav.AppendChild(previousCard);
        agentNav.AppendChild(nextCard);

        nav.AppendChild(agentNav);
        wrapper.AppendChild(nav);

        IElement? main = document.QuerySelector("main");
        if (main?.Parent is null)
        {
            return;
        }

        INode? nextSibling = main.NextSibling;
        if (nextSibling is null)
        {
            main.Parent.AppendChild(wrapper);
        }
        else
        {
            main.Parent.InsertBefore(wrapper, nextSibling);
        }
    }

    private static IElement BuildAgentCard(
        IDocument document,
        string positionClass,
        string cardClass,
        AgentEntry? agent,
        string directionLabel,
        string linkClass)
    {
        IElement card = document.CreateElement("div");
        card.ClassList.Add(positionClass);
        card.ClassList.Add(cardClass);

        if (agent is null)
        {
            card.InnerHtml = "&nbsp;";
            return card;
        }

        IElement link = BuildAgentLink(document, agent, directionLabel, linkClass);

        card.AppendChild(link);
        return card;
    }

    private static IElement BuildAgentLink(
        IDocument document,
        AgentEntry agent,
        string directionLabel,
        string linkClass)
    {
        IElement a = document.CreateElement("a");
        a.SetAttribute("href", $"/{agent.Slug}");
        a.ClassName = linkClass;

        IElement img = document.CreateElement("img");
        img.SetAttribute("src", $"/-/images/thumbnails/{agent.Slug}.jpg");
        img.SetAttribute("alt", "");
        img.SetAttribute("role", "presentation");
        a.AppendChild(img);

        IElement label = document.CreateElement("span");
        label.TextContent = $"{directionLabel}:";
        a.AppendChild(label);

        a.AppendChild(document.CreateTextNode($" {agent.Name}"));

        return a;
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

    private readonly struct AgentLinkPair
    {
        public AgentLinkPair(AgentEntry? previous, AgentEntry? next)
        {
            Previous = previous;
            Next = next;
        }

        public AgentEntry? Previous { get; }

        public AgentEntry? Next { get; }
    }
}
