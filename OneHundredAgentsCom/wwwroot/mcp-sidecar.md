# MCP sidecar

**MCP sidecar** is a small test of a bigger idea: an AI agent should not have to roam the internet or collect its own tools one by one. This agent runs in its own Docker container, but it gets help from two neighbours. A Squid proxy sidecar controls network access. A new MCP Server sidecar provides the agent with approved resources and tools. The agent becomes an MCP host inside the sandbox, asking the sidecar for what it needs instead of reaching directly into every outside system.

The business task is deliberately plain: produce a short document with a C# and .NET 8 code sample for obtaining an `HttpClient`. The useful part is how it does that. The code sample comes through Microsoft’s MCP server, while the answer structure comes from a resource provided by the local **MCP sidecar**. So the response is not just “whatever the model felt like today,” which is a brave but unwise document strategy.

This points toward a practical control pattern. A company could give many agents centralized access to approved tools, shared instructions, internal resources, and audited external services, while keeping each agent boxed into a narrower runtime.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Docker localnet | AI agent container | Squid proxy sidecar | MCP Server sidecar
Model: [GPT-5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/McpSidecar)

::: /SIDEBAR :::
