# Price Checker

Price Checker is a small CrewAI experiment built to test one practical idea: can agents use tools to fetch business facts before making a judgement? The scenario is deliberately plain. Give the crew a product SKU, and it checks the company’s price, checks a competitor’s price, then reports whether the company is cheaper, equal, or more expensive.

The useful part is not that price comparison needs three agents. It does not. A normal script would do the job with less ceremony. The point is to see how CrewAI lets each agent reach outside the model through a tool. One agent looks up internal pricing. Another gets the competitor price by scraping its website. A third receives both results and writes the comparison in business language.

That makes the project a good first test of CrewAI tools. It shows the shape of an agent workflow where the model is not guessing from memory, but asking small, focused tools for current facts.

::: SIDEBAR :::

Language: Python
Framework: [CrewAI](https://crewai.com/)
Pattern: Multi-agent
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/TopicInstructor)

::: /SIDEBAR :::
