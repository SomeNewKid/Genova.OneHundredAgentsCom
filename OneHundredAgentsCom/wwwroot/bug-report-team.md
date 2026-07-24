# Bug report team

**Bug report team** is a small proof of concept built to test whether several AI agents can split one business task without politely waiting in line. A manager agent receives a fictional bug report, then asks three specialist agents to assess it from different angles: frontend, backend, and database. Each worker returns a likelihood score and reasons. The manager collates those views and assigns a likely category and priority.

The interesting part is not the bug triage itself. That job is deliberately simple. The point is the coordination pattern. The agents use agent-to-agent (A2A) tasks so the manager can start all three workstreams in parallel, poll for completion, and combine the results when they are ready. Each worker runs in its own Docker container inside a hardened sandbox, which makes the boundaries between agents visible instead of hand-wavy. Tiny bug reports, real orchestration. A fair trade.

This sketches a useful business shape: a manager agent can broadcast work to specialists, gather independent opinions, and make a final call. A2A gives that pattern a concrete mechanism rather than another diagram with arrows and optimism.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Multiple agents
Sandbox: Docker localnet | AI agent containers | Squid proxy sidecar | MCP Server sidecar | Jina Reader sidecar | Code execution sidecar | HAProxy sidecar
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/BugReportTeam)

::: /SIDEBAR :::
