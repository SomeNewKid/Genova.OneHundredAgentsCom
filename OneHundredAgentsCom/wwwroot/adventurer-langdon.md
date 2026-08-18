# Adventurer Langdon

**Adventurer Langdon** is a deliberately small agent built to test something larger than game recommendations: how well LangGraph helps you see what an agent is doing. The user names an adventure game she enjoyed, and Langdon recommends another game in the same broad genre. That business pattern could fit any recommendation workflow where the stakes are modest but repeated use still matters. The game choice is the bait; observability is the hook.

The useful part is the trace. LangGraph gives the run a clear path through the model call, tool choice, tool result, and final answer. LangSmith then makes that path visible in a dashboard, so the agent is not just a mysterious sentence dispenser wearing a little cape. The project also tests custom tracing, adding spans for local tool work and estimated model cost.

That matters because most agent problems are not obvious from the final response. A recommendation may look fine while the agent used the wrong tool, spent too much, or wandered through unnecessary steps. This proof of concept shows that tracing should be part of the build from the start, not sprinkled on later like parsley.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Platform: [LangSmith](https://www.langchain.com/langsmith)
Pattern: Single agent
Model: [Claude Opus 5](https://www.anthropic.com/claude/opus)
---

Repository: [GitHub](https://github.com/SomeNewKid/AdventurerLangdon)

::: /SIDEBAR :::
