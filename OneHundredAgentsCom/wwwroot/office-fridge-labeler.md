# Office fridge labeler

**Office fridge labeler** is a small agent built to test a very practical question: can an agent use shared business rules and tools without having all of them baked into its own code? The scenario is deliberately ordinary. Someone describes a food item in plain language, and the agent generates a label for the office fridge.

The agent uses the OpenAI Agents SDK, and gets its fridge rules and label-making tool from a real Model Context Protocol server. That means the policy and the action live outside the agent itself. The agent can discover and use them, while the MCP server becomes the place where shared resources and approved tools can be managed.

This sketches a bigger business capability in miniature. Many agents could use the same governed tools and resources, instead of every team wiring its own private version. The fridge is just a low-risk place to test it.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Protocol: [Model Context Protocol](https://modelcontextprotocol.io/)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/OfficeFridgeLabeler)

::: /SIDEBAR :::
