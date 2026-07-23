# Contact assistant

**Contact assistant** is a small experiment in using Haystack to answer questions over a set of contact details. The business shape is simple: instead of searching through notes yourself, you ask, “Who do I know in Paris?” and get a direct answer. That is a useful little test because many real business questions start exactly there, buried in documents, records, emails, or half-tidy notes that nobody quite wants to organize by hand.

Haystack has long been strong at connecting documents to language models, and this sample tests that reputation in a deliberately narrow setting. The agent looks up relevant contact information, gives that context to the model, and returns a plain response such as “You know Jean, who lives in Paris.” No dashboard, no confetti, no heroic claims about replacing a CRM by lunchtime.

What matters is the pattern. A user asks a natural question, the system finds the relevant material, and the model answers from that material. For contact lookup, that points toward assistants that can make business knowledge easier to query without turning every employee into a part-time database archaeologist.

::: SIDEBAR :::

Language: Python
Framework: [Haystack AI](https://haystack.deepset.ai/)
Pattern: Single agent
Model: [GPT 4o mini](https://developers.openai.com/api/docs/models/gpt-4o-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/ContactAssistant)

::: /SIDEBAR :::
