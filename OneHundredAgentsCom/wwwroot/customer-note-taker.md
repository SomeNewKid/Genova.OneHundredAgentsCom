# Customer Note Taker

Customer Note Taker is a small proof of concept for a very ordinary business problem: turning a customer email into a structured note that another system can handle later.

The interesting part is not the note itself. It is the set of gates around the work. The agent uses LangChain to test how guardrails can sit before and after the agent, before and after model calls, and around tool use. That makes the project less about “can an LLM summarize an email?” and more about “can we stop the wrong email from going through the normal path?”

The agent reads a sample customer email, asks a model to produce a CustomerNote, looks up an account number, and saves the result as JSON when processing is allowed. Some inputs are blocked. Some content is changed first, such as masking sensitive information.

::: SIDEBAR :::

Language: Python
Framework: [LangChain](https://www.langchain.com/)
Pattern: Single agent
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/CustomerNoteTaker)

::: /SIDEBAR :::
