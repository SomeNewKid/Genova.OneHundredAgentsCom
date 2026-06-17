# Movie Finder Agent

Movie Finder Agent tests a small but useful idea: what happens when an agent can look things up semantically instead of relying only on what the language model already carries around in its head. It uses IBM’s BeeAI framework to wrap a movie-search task in an agent, then gives that agent a retrieval tool backed by embeddings and a vector store.

The user can ask a loose question such as, “In what movie does a slacker get mixed up in a kidnapping plot?” The agent does not need the exact title. It turns the question into a semantic search, finds the closest matching movie record, and uses that result to answer in plain language. That is the interesting part: the search is based on meaning, not just matching specific words.

The business shape is easy to see. Replace movie summaries with policy documents, support notes, product manuals, or internal research, and the same pattern points toward agents that can consult company knowledge before answering. This proof of concept explores that connection without pretending the small demo is the finished machine.

::: SIDEBAR :::

Language: Python
Framework: [IBM BeeAI Framework](https://framework.beeai.dev)
Pattern: Single agent
Models: [Granite 3.3](https://www.ibm.com/granite) | [MiniLM-L6-v2](https://huggingface.co/sentence-transformers/all-MiniLM-L6-v2)
---

Repository: [GitHub](https://github.com/SomeNewKid/MovieFinderAgent)

::: /SIDEBAR :::
