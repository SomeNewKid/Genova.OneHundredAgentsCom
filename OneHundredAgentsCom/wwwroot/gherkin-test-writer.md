# Gherkin test writer

**Gherkin test writer** is a small proof of concept built to test BeeAI’s agent workflow pattern. The business task is deliberately plain: take a software bug report and turn it into a Gherkin feature that describes the intended behaviour. That is useful enough, but the more interesting part is not the document itself. It is the way the work is divided and ordered.

The agent uses a writer and a reviewer in a fixed sequence. The writer reads the bug report and drafts the feature. The reviewer checks the draft and repairs it before the final result is accepted. This is closer to a familiar business workflow than a loose chat with one all-purpose assistant. Someone does the first pass, someone checks it, and only then does the output move forward.

The project shows where agent workflows can fit among multi-agent patterns. Sometimes you want agents to collaborate freely. Sometimes you want a queue, a checkpoint, and a little adult supervision. BeeAI’s workflow model gives this experiment that shape: model-driven drafting, tool-backed review, and a controlled order of interaction.

::: SIDEBAR :::

Language: Python
Framework: [IBM BeeAI Framework](https://framework.beeai.dev)
Pattern: Multiple agents workflow
Models: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/GherkinTestWriter)

::: /SIDEBAR :::
