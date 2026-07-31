# Webpage image repairer

**Webpage image repairer** is a small proof of concept built to test how tool use feels in the OpenAI Agents SDK. The business task is deliberately plain: check an HTML page, find an image, and make sure it has the accessibility attribute it needs. Not glamorous work, but exactly the kind of small compliance chore that tends to hide in a backlog until someone has a bad afternoon.

The agent uses different kinds of tools for different parts of the job. It asks a hosted web search tool what accessibility requires. It checks the page with local functions. It resolves the image file locally. It asks a smaller image-description agent to produce concise alt text. Then it applies a controlled edit to the page. The point is not that this one page needed heroic automation. The point is that an agent can move between outside knowledge, local files, specialist helpers, and edits without that feeling conceptually exotic.

What this sketch shows is modest but useful: “tool” is not a magic word. It is a way to give an agent a few well-shaped handles on real work. That turns a vague assistant into something that can check, decide, and act.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Multiple agents
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/WebpageImageRepairer)

::: /SIDEBAR :::
