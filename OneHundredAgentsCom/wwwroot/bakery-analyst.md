# Bakery analyst

**Bakery analyst** was built to test a very specific next step with Microsoft Foundry: can a no-code prompt agent use provided business data and do useful arithmetic over it? The business setting is deliberately familiar. A bakery has one week of production and sales data. Someone can ask plain questions like whether donuts made a profit, which products sold best, or when muffins lost the most money. The agent checks the supplied data, applies the basic calculations, and answers in business language.

The useful part is not that bakeries need yet another dashboard with frosting on it. It is that a prompt agent in Foundry can be set up quickly, given a small data resource, and tested through the managed chat interface without writing an application first. That makes it a good way to find out what is real before building anything larger.

This project sketches a simple analyst workflow: give the agent trusted data, ask natural questions, and get grounded answers instead of a spreadsheet stare-off. It is small, but it runs inside the same kind of managed agent environment that could support more serious deployments later.

::: SIDEBAR :::

Language: Markdown
Platform: [Microsoft Foundry](https://ai.azure.com/)
Pattern: Prompt agent
Model: [GPT 5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/BakeryAnalyst)

::: /SIDEBAR :::
