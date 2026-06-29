# Scribble Sorter

Scribble Sorter is a small proof of concept built to test workflows in Microsoft’s Agent Framework. The business sketch is simple: give the system an image of a scribble, and let the workflow decide what kind of work should happen next. If the scribble is a drawing, it turns the idea into a photorealistic generated image. If it is anything else, such as a rough checklist, it creates a Markdown text file that represents the contents.

The useful part is not the napkin. The useful part is the shape of the workflow. One executor uses an AI model to inspect the image and return a structured result. The next step depends on that result. One branch uses AI generation. The other branch is plain file creation. That mix matters, because many business processes are not “all AI” or “no AI”. They are a chain of judgement, routing, and ordinary work.

This project shows that Microsoft’s Agent Framework can make that chain visible. Conditional edges are the interesting bit here: the workflow can ask the model what it is looking at, then choose the next action without turning the whole thing into a tangle of glue code.

::: SIDEBAR :::

Language: C#
Framework: [Agent Framework](https://learn.microsoft.com/agent-framework/)
Pattern: Single agent
Models: [GPT-5.5](https://developers.openai.com/api/docs/models/gpt-5.5) | [GPT-image-1](https://developers.openai.com/api/docs/models/gpt-image-1)
---

Repository: [GitHub](https://github.com/SomeNewKid/ScribbleSorter)

::: /SIDEBAR :::
