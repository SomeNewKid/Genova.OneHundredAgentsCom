# Jina Reader sidecar

**Jina Sidecar** explores a simple question: what should an agent do when it needs to read the web, but we do not want it wandering around with a browser and a hopeful expression? This proof of concept puts Jina Reader beside the agent as a local sidecar. The agent asks for a web page and its related home page, Jina Reader fetches and cleans the content, and the MCP Server exposes that ability as a narrow tool the agent can call.

The business shape is content review. An agent can gather page context, turn messy web material into text that is easier for a model to work with, and produce a grounded summary of what a page provides. The interesting part is not the particular page. It is the pattern: give the agent the specific tool it needs, keep that tool inside a purpose-built Docker network, and route web access through controlled egress. That makes the agent feel less like a magic intern with Wi-Fi and more like a small, supervised process with a job to do.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Docker localnet | AI agent container | Squid proxy sidecar | MCP Server sidecar | Jina Reader sidecar
Model: [GPT-5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/JinaSidecar)

::: /SIDEBAR :::
