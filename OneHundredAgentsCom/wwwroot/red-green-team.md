# Red green team

**Red green team** explores a simple question: can multiple AI agents work together in a disciplined generator-critic loop, instead of one model trying to do everything in a single heroic wobble? The project sketches a manager agent with a software requirement. It asks a coder agent to produce an implementation, then asks a tester agent to challenge that work with unit tests. If the tests fail, the manager sends the failures back to the coder and the loop continues.

The useful part is not the tiny coding task. It is the shape of the collaboration. Each agent runs in its own Docker container, with a shared workspace and controlled sidecars for tool use and code execution. The agents coordinate through the agent-to-agent protocol, so the workflow is explicit rather than hidden inside one long prompt. That makes the pattern easier to inspect, repeat, and reason about

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Multiple agents
Sandbox: Docker localnet | AI agent containers | Squid proxy sidecar | MCP Server sidecar | Jina Reader sidecar | Code execution sidecar | HAProxy sidecar
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/RedGreenTeam)

::: /SIDEBAR :::
