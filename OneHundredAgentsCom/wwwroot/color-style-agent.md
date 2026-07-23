# Color style agent

**Color style agent** was built to test a practical question: can a business attach a style rule to an agent without rewriting every agent one by one? It uses Google's Agent Development Kit to add a custom plugin around a small writing agent. The agent writes a short Markdown article about CSS colour rules. The plugin then checks the final prose and changes American spelling to Australian spelling while leaving code examples alone.

The business idea is larger than the toy article. A company might have dozens of agents writing support replies, reports, summaries, or training material. Each one could be asked politely to follow the style guide, and each one could also decide to be creative at the worst possible moment. A plugin gives the business a second place to apply the rule, outside the agent's own prompt.

This project is not a finished governance system. It is a small test of where that kind of control might live. The useful part is the pattern: plugins can help standardise validation, style, and output quality across many agents, not just one carefully supervised demo.

::: SIDEBAR :::

Language: Python
Framework: [Agent Development Kit](https://adk.dev/)
Pattern: Single agent
Models: [Gemini 3.5 Flash](https://ai.google.dev/gemini-api/docs/models/gemini-3.5-flash) | [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/ColorStyleAgent)

::: /SIDEBAR :::
