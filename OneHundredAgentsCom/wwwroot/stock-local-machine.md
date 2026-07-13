# Stock Local Machine

Sandbox Tester is a small proof of concept built to answer an uncomfortable question: what can an AI agent do when it runs on a normal local machine, with no hardening, using the user’s own identity? The answer is not soothing. It can check access to files, processes, environment variables, browser traces, credentials, network targets, installed tools, source control, hardware, scheduled tasks, logs, and more. In other words, it tests the shape of the keys the agent has been handed.

The stock local sandbox version deliberately does not add much sandbox. That is the point. It prepares a few known test areas, runs the Sandbox Tester against the current machine, and records what worked, what failed, and what was not available. This gives a business reader something more useful than a vague security claim: a report showing what an agent could actually touch when launched like any other local tool.

The lesson is blunt. If an AI agent runs as you, it may inherit a surprising amount of you. That is useful for automation, and also a very good reason not to confuse convenience with containment.

::: SIDEBAR :::

Language: Python
Framework: None
Pattern: Single agent
Sandbox: None
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxTester)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-local-stock" title="Sandbox Report - Local Stock Machine" :::
