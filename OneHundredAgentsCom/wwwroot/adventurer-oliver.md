# Adventurer Oliver

**Adventurer Oliver** is a small proof of concept about seeing what an AI agent actually did after it answered. A player names an adventure game she enjoyed, and Oliver recommends another game in the same broad style. The recommendation itself is intentionally modest. The point is the trail it leaves behind: model calls, tool choices, reasoning summaries, token use, and estimated cost all become easier to inspect.

The project uses the OpenAI Agents SDK because its tracing is not an afterthought bolted on with hope and string. Each run can be opened in the OpenAI Traces dashboard, where the path from user request to tool call to final answer is visible. Custom spans add business-relevant notes, such as total usage and estimated run cost, so the trace starts to look like an operational record rather than a mystery receipt.

The useful lesson is bigger than game suggestions. Any agent used repeatedly needs this kind of observability to explain failures, compare runs, and find places to improve.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Model: [GPT-5](https://developers.openai.com/api/docs/models/gpt-5)
---

Repository: [GitHub](https://github.com/SomeNewKid/AdventurerOliver)

::: /SIDEBAR :::
