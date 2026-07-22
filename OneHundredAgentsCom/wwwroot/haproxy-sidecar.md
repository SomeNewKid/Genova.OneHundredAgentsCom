# HAProxy Sidecar

**HAProxy Sidecar** explores a small but important question: what happens when a sandboxed AI agent needs data that lives outside its sandbox? The agent runs inside a hardened Docker environment, but the useful information is in a database on the host machine. Instead of punching a wide hole through the sandbox, the project adds an HAProxy container as a controlled bridge.

The agent asks an MCP sidecar for business data, then uses a hosted GPT model to build a simple HTML page from the result. The page is not the interesting part. The interesting part is the route the data takes. The AI agent does not get direct database credentials or a direct line to the host. The MCP sidecar gets the narrow access it needs, and HAProxy handles the connection out of the local Docker network.

This proof of concept sketches a common business shape: agents that work in restricted environments still need approved access to real systems. HAProxy gives that access a clear place to live. It is not magic security dust, thankfully, but it is a practical boundary to inspect, configure, and improve.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Docker localnet | AI Agent container | Squid proxy sidecar | MCP Server sidecar | Jina Reader sidecar | Code execution sidecar | HAProxy sidecar
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/HAProxySidecar)

::: /SIDEBAR :::
