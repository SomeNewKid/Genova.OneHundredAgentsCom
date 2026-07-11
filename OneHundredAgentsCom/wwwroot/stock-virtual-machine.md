# Stock Local Machine

Sandbox Tester is a proof-of-concept agent built to answer an uncomfortable question: what can this agent actually do from inside its runtime? It runs through a catalogue of capabilities and reports whether each one is allowed, denied, not applicable, or failed. That includes obvious checks, like reading files, and less obvious ones, like looking at home directories, shared drives, environment details, local services, tools, and approval boundaries.

The business value is not that the agent performs a glamorous task. It does something more useful before the glamour starts: it shines a light into the sandbox. If an AI agent can list a network drive, read configuration files, invoke commands, or reach local services, that matters. Those powers may be accidental. They may also be exactly where the risk lives.

Built around the OpenAI Agents SDK, Sandbox Tester sketches a repeatable way to compare environments. Run it on a normal machine, then run it in a hardened sandbox, and the report should change. If it does not, the sandbox may be mostly decorative. That is awkward, but useful awkward.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: None
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxTester)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-vm-stock" title="Sandbox Report - Local Virtual Machine" :::
