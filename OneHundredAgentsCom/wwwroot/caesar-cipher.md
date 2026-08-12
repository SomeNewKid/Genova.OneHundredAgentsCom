# Caesar cipher

**Caesar cipher** is a small prompt agent built to test a very specific next step in Microsoft Foundry: can a no-code prompt agent call a tool that lives outside the agent, exposed through a remote Model Context Protocol server? The business story is deliberately simple. A user wants to send a secret message, asks the agent to encode it, and gets back the transformed text.

The Caesar cipher is not the star here. It is the test weight. It is easy to recognize when the answer is right, which makes it useful for checking whether the agent really called the remote tool instead of improvising. The Foundry agent handles the conversation, while the Azure Function app provides the tool through MCP. That split is the useful part: the agent can stay lightweight, while specialist actions sit behind a managed service boundary.

This project shows the shape of a practical pattern. A business team can create a prompt agent in Foundry, connect it to remote tools, and test the whole loop in a managed agent environment. Tiny cipher today; more useful business function tomorrow.

::: SIDEBAR :::

Language: Markdown
Platform: [Microsoft Foundry](https://ai.azure.com/)
Pattern: Prompt agent
Model: [GPT 5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/CaesarCipher)

::: /SIDEBAR :::
