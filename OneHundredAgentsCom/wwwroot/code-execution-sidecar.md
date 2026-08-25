# Code execution sidecar

Code Sidecar explores a narrow but important question: what happens when an AI agent needs to calculate something exactly, rather than confidently wave at the answer from memory? The agent uses the OpenAI Agents SDK, but the interesting part is the extra capability around it. When the task needs real computation, the agent can ask for a small Python script to be run through a code execution tool exposed by an MCP server.

The business shape is simple. A user asks for a result that depends on calculation or structured processing. The agent writes the small script, runs it, reads the output, and returns the answer. That is more useful than asking a language model to do arithmetic in its head, which remains a heroic act of spreadsheet cosplay.

The point is not the sample calculation. The point is the boundary. Code execution is powerful, so this project tests putting that power in a separate hardened container with limits and audit trails. It points toward a useful pattern: give agents sharper tools, but keep those tools specific, constrained, and observable.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Docker localnet | AI agent container | Squid proxy sidecar | MCP Server sidecar | Code execution sidecar
Model: [GPT-5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/CodeSidecar)

::: /SIDEBAR :::
