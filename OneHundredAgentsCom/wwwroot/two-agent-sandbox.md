# Two-agent sandbox

The **two-agent sandbox** was built to test a practical question: can two AI agents run as separate workers inside a temporary Docker network, with enough shared infrastructure around them to behave like one small system? The answer is yes, at least in proof-of-concept form. One agent acts as the entry point. It gathers business data and prepares an HTML page. A second agent is called through an agent-to-agent exchange and adds company context to the page before handing it back.

The interesting part is not the page itself. Nobody needs to summon two agents just to add a header, unless they are trying to make a point with a very small hammer. The useful lesson is the environment. Each agent runs in its own container, declares its own needs, and shares temporary sidecars for network access, database routing, and MCP tools and resources.

This sketches a pattern for business workflows where agents divide work but still operate inside a controlled, disposable private network. It points toward multi-agent systems that are easier to reason about than a heap of processes all waving at the same laptop.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Multiple agents
Sandbox: Docker localnet | AI agent containers | Squid proxy sidecar | MCP Server sidecar | Jina Reader sidecar | Code execution sidecar | HAProxy sidecar
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/TwoAgentSandbox)

::: /SIDEBAR :::
