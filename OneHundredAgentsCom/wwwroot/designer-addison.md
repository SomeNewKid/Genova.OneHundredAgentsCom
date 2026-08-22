# Designer Addison

**Designer Addison** explores a simple but uncomfortable question: can an OpenAI agent take a requirements document, a bundle of content assets, and build a complete static website? The answer was yes, in the practical sense. It could read the brief, copy images and supporting files, create pages, serve the result locally, and use a browser to check screenshots and console errors. That is a real business shape: turn a structured content package into a first-pass website without asking a person to place every link and image by hand.

The more interesting answer was also yes, but please lower your expectations before opening the curtains. Addison could meet many requirements, yet its design judgement was weak. It produced files, navigation, and page structure, but the visual result showed why “the model will design it” is not much of a design strategy.

For a production version, the lesson is not to abandon the agent. It is to move taste, layout rules, and brand direction out of the agent’s vague instincts and into human-owned systems. Let the agent assemble and check. Let people decide what good looks like.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Model: [GPT-5](https://developers.openai.com/api/docs/models/gpt-5)
---

Repository: [GitHub](https://github.com/SomeNewKid/DesignerAddison)

::: /SIDEBAR :::
