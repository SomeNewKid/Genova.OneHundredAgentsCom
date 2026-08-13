# Customs team

**Customs team** is a small proof of concept built to test the next step with Microsoft Foundry: three prompt agents coordinating through the agent-to-agent protocol. The user asks a customs official a plain question, such as whether juggling balls with seeds inside can be brought into Australia. The customs official does not answer alone. It asks a dangerous goods official, then asks a food stuffs official, and combines their decisions into one response.

The business shape is familiar: one front-line agent handles the conversation, while specialist agents check narrower rules. In this case, the dangerous goods agent finds no problem, while the food stuffs agent blocks the item because it contains seeds. The customs official then gives the user a clear “not permitted” answer and explains why. It is not real customs advice, thankfully for everyone who owns suspicious juggling equipment.

What matters here is the coordination. Foundry made it reasonably simple to connect managed agents with A2A tools, in the same environment that also supports MCP-style tool connections. The agents are deliberately small, but the pattern points toward larger workflows where specialists can be added without turning one agent into a giant tangle of instructions.

::: SIDEBAR :::

Language: Markdown
Platform: [Microsoft Foundry](https://ai.azure.com/)
Pattern: Multiple prompt agents
Model: [GPT 4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/CustomsTeam)

::: /SIDEBAR :::
