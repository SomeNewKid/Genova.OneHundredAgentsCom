# Local model coding

**Local model coding** explores a question that matters before anyone lets local models near real development work: are they actually good at coding, or are they just good at sounding confident in monospace? It asks local models to handle six common coding tasks: writing code, fixing bugs, completing an existing design, refactoring, generating tests, and reviewing code. Each answer is scored by a hosted GPT-5 evaluator against a detailed rubric, so the comparison is about coding behaviour rather than vibes.

The point is not that every benchmark prompt is heroic. It is that coding is a different skill from general language fluency. A model that writes a polished paragraph may still miss a boundary case, mutate input data, or produce tests that politely test nothing. This workbench separates those abilities and makes the failures visible.

The most useful result was practical: OpenAI’s smaller open-weight model performed best on a mid-level laptop with 16GB RAM, with scores approaching frontier online models. It was much slower, and speed was recorded rather than treated as a fair score, because Ollama performance was not heavily tuned.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Models: Many
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalModelCoding)

::: /SIDEBAR :::

::: CODING-REPORT name="local-model-coding" title="Local model coding" :::
