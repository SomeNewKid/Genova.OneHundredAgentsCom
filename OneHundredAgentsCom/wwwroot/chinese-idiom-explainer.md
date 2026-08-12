# Chinese idiom explainer

**Chinese idiom explainer** was built as a first, deliberately small step into Microsoft Foundry. The goal was not to invent a grand language-learning platform. It was to see how quickly a prompt agent could be created, given a clear job and one managed tool. In this case, the job is simple: an English-speaking Chinese learner asks about an idiom such as `画蛇添足`, and the agent explains the literal translation, the figurative meaning, and the cultural background.

The useful part is not that the agent explains idioms. Plenty of things can explain idioms, with varying levels of confidence and theatrical arm-waving. The useful part is that Foundry makes the agent setup almost boring: choose a model, add instructions, enable the built-in web search tool, and run it in the managed agent environment. No application code is needed for this first version.

That makes this project a good sanity check. It sketches a small business capability: a focused assistant that can answer domain-specific questions while checking public sources when needed. More importantly, it shows that Foundry’s prompt-agent path is approachable without giving up the shape of a larger deployment environment.

::: SIDEBAR :::

Language: Markdown
Platform: [Microsoft Foundry](https://ai.azure.com/)
Pattern: Prompt agent
Model: [GPT 5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/ChineseIdiomExplainer)

::: /SIDEBAR :::
