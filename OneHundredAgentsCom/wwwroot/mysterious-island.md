# Mysterious Island

**Mysterious Island** is a small experiment in using Microsoft Foundry as a managed home for a code-first agent. The agent lets a user discuss Jules Verne’s *The Mysterious Island*, but it is not meant to show that a model has read a famous old novel. The point is to test whether Foundry can host custom agent code while also providing built-in retrieval, so the agent can look up the uploaded book text and private reading notes before answering.

That matters because many business agents need the same pattern. They need to answer from specific source material, not from whatever the model happens to remember. In this case the source is a novel and a few private notes. In a work setting it could be policies, product manuals, contract notes, or project records. The book is just friendlier than a procurement policy.

The useful lesson is that Foundry is not only a chat interface. It is an agent runtime. A developer can bring code, deploy it into a managed environment, and attach built-in tools such as file search, web search, and code interpretation. This agent is modest, but it points toward a practical deployment path for more serious agents.

::: SIDEBAR :::

Language: Markdown
Platform: [Microsoft Foundry](https://ai.azure.com/)
Pattern: Code-first agent
Model: [GPT 4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/MysteriousIsland)

::: /SIDEBAR :::
