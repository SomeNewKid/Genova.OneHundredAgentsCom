# Local model evaluation

**Local model evaluation** was built to test a practical question: which local language models can do useful agent work on an ordinary laptop, rather than merely sound clever in a chat window? The task was a small sales workflow. A local BeeAI agent read a customer email, extracted the order into structured data, called tools to look up customer and product details, and wrote a report for the sales team. An OpenAI-based evaluator then scored the results.

The scoring was deliberately business-shaped. Models earned points for accurate extraction, valid structure, clear reporting, and correct tool use. They lost ground when they hallucinated customer IDs, credit amounts, stock details, or other confident nonsense. Speed was recorded, but not scored, because Ollama had not been tuned for performance. This was about capability first, patience second.

The results were useful. Many small local models could extract data, but struggled with tool use. Gemma 4 stood out: `gemma4:12b` matched an online frontier GPT model at 100%, while several instruction-tuned Gemma 4 variants and `gpt-oss:20b` came close. On a 16GB laptop, that is a result worth noticing, even if it took the scenic route.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/) | [IBM BeeAI Framework](https://framework.beeai.dev)
Pattern: Multiple agents
Models: Many
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalModelEvaluation)

::: /SIDEBAR :::

::: EVALUATION-REPORT name="local-model-evaluation" title="Local model evaluation" :::
