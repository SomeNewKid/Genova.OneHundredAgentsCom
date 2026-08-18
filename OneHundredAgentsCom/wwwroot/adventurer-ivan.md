# Adventurer Ivan

**Adventurer Ivan** is a small agent built to test what tracing looks like in IBM’s BeeAI framework. The business sketch is simple: a player names an adventure game she enjoyed, and Ivan recommends another game in the same broad style. That makes the agent easy to reason about, which is useful when the real subject is not game taste but observability. If the trace is confusing in a tiny recommendation flow, it will not become magically delightful in a larger one.

The useful finding is that BeeAI makes OpenTelemetry tracing fairly easy to switch on. The Jaeger dashboard can show the agent run, model calls, tool calls, and final answer as a connected trace. That is exactly the kind of visibility a repeated-use agent needs. You can see where time goes, what decisions were made, and which parts of the workflow deserve attention.

The less tidy finding is custom tracing. BeeAI exposes strong built-in traces, but adding your own spans into the exact place you want appears to be impossible. Ivan shows both sides: OpenTelemetry support is real and valuable, but extending it cleanly may take more digging than the cheerful demo path suggests.

::: SIDEBAR :::

Language: Python
Framework: [IBM BeeAI Framework](https://framework.beeai.dev)
Pattern: Single agent
Model: [Claude Opus 5](https://www.anthropic.com/claude/opus)
---

Repository: [GitHub](https://github.com/SomeNewKid/AdventurerIvan)

::: /SIDEBAR :::
