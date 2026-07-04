# Obsidian Vault Summarizer

Obsidian Vault Summarizer is a small proof of concept built to test guardrails in the Otto Agent framework. The business task is simple: read a collection of notes and produce a short summary of the knowledge inside them. The more interesting question is what happens when the agent is allowed to use tools, but not trusted to use every tool result without supervision.

This version adds guardrails before and after the agent run, and before and after tool calls. In the sample vault, the agent can list and read notes, but a guardrail blocks access to a secret file. Instead of crashing the whole run, the agent records that the attempted tool call was blocked and continues with the knowledge it is allowed to use. That is the useful pattern: the agent can keep working without pretending it saw something it did not.

The project does not prove that guardrails make agents safe. They are not a sandbox, a permissions system, or a magic compliance hat. But they do give the harness places to inspect, block, or redact behaviour. That is a practical step toward safer agent workflows.

::: SIDEBAR :::

Language: Python
Framework: Bespoke Otto framework
Pattern: Single agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---
Repository: [GitHub](https://github.com/SomeNewKid/ObsidianVaultSummarizer)

::: /SIDEBAR :::
