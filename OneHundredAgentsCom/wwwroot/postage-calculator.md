# Postage calculator

**Postage calculator** is a small proof of concept built to test skills in Microsoft’s Agent Framework. The business task is deliberately plain: a user asks how much it costs to send a package from Perth to another city. The interesting part is not postage. Nobody needs a parade for a parcel calculator. The useful question is whether an agent can turn the request into a task, choose the right specialist capability, and use it.

The agent separates the work into domestic and international delivery skills. If the destination is in Australia, it uses the domestic skill. If the destination is overseas, it uses the international shipping skill. Each skill carries its own instructions and script, so the agent is not just calling one general calculator. It is selecting from packaged capabilities that know what kind of work they are meant to handle.

This sketches a useful pattern for business agents. Skills can bundle focused knowledge and executable steps, while the agent decides which one fits the current request. That is the part worth testing.

::: SIDEBAR :::

Language: C#
Framework: [Agent Framework](https://learn.microsoft.com/agent-framework/)
Pattern: Multi-agent
Model: [GPT-4o-mini](https://developers.openai.com/api/docs/models/gpt-4o-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/PostageCalculator)

::: /SIDEBAR :::
