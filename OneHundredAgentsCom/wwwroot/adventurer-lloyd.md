# Adventurer Lloyd

**Adventurer Lloyd** is a small proof of concept built to test tracing with an agent based on the LlamaIndex framework. The business sketch is simple: a player tells Lloyd about an adventure game she enjoyed, and Lloyd recommends another game in the same broad genre.

The useful part is what happens around the recommendation. The project shows how quickly LlamaIndex can emit traces for an agent run, tool use, and model calls, then send them to a Phoenix dashboard through OpenTelemetry. That makes the agent’s behaviour visible instead of mystical. For any agent used repeatedly, that visibility matters. Traces help explain odd answers, slow steps, tool mistakes, and places where the workflow could be improved.

The exercise also shows a wrinkle. LlamaIndex makes basic tracing fairly easy, but adding custom tracing from ordinary application code is less polished than it could be. It is possible, because OpenTelemetry is there, but the path feels more like plumbing than a friendly paved road.

::: SIDEBAR :::

Language: Python
Framework: [LlamaIndex](https://llamaindex.ai/llamaindex)
Platform: [Phoenix](https://arize.com/docs/phoenix)
Pattern: Single agent
Model: [Claude Opus 5](https://www.anthropic.com/claude/opus)
---

Repository: [GitHub](https://github.com/SomeNewKid/AdventurerLloyd)

::: /SIDEBAR :::
