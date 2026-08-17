# Adventurer Adrian

**Adventurer Adrian** is a small proof of concept built to test tracing with the Claude Agent SDK. The business scenario is deliberately simple: a player names an adventure game she enjoyed, and Adrian recommends another game in the same broad genre. That gives the agent just enough work to do without letting the demo become “yet another game recommender,” which nobody was urgently waiting for.

The real point is observability. If an agent will be used more than a handful of times, someone will eventually ask why it chose a tool, why a run was slow, why costs changed, or why the answer looked odd. Adrian sends traces through OpenTelemetry and makes it possible to inspect the run in Jaeger, including model usage, tool activity, and estimated cost.

What the experiment shows is mixed. The Claude Agent SDK does provide built-in tracing, but it is fairly minimal, and extending it with custom tracing is awkward. The agent’s own spans can participate, but not always in the neat parent-child shape you might want. That is useful to learn early, before the agent is doing anything more expensive than recommending imaginary weekends indoors.

::: SIDEBAR :::

Language: Python
Framework: [Claude Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Observability: [OpenTelemetry](https://opentelemetry.io/)
Pattern: Single agent
Model: [Claude Opus 5](https://www.anthropic.com/claude/opus)
---

Repository: [GitHub](https://github.com/SomeNewKid/AdventurerAdrian)

::: /SIDEBAR :::
