# Ghost story raconteur

**Ghost story raconteur** is a small proof of concept for testing LlamaIndex as a way to ask questions about private documents. The user asks questions about the ghost stories in a private book, and the agent looks for relevant passages before answering. The useful idea is not the spooky subject matter. It is the pattern: a business could point the same shape of agent at internal manuals, contracts, policy notes, research archives, or any other pile of text that is important but awkward to search.

The project explores retrieval-augmented generation without dressing it up as magic. LlamaIndex makes the first version surprisingly direct: load private text, build a vector index, retrieve matching excerpts, and pass only those excerpts into the chat model. That is a practical workflow for teams that want answers grounded in their own material rather than whatever the model half-remembers from the internet.

The experiment also showed the less glamorous part. Retrieval quality depends heavily on the embedding model. A weaker embedding setup missed obvious passages; a better one found them. LlamaIndex provides the scaffolding, but the embeddings decide whether the agent finds the right passages in the book.

::: SIDEBAR :::

Language: Python
Framework: [LlamaIndex](https://llamaindex.ai/llamaindex)
Pattern: Single agent
Model: [GPT-5](https://developers.openai.com/api/docs/models/gpt-5)
---

Repository: [GitHub](https://github.com/SomeNewKid/GhostStoryRaconteur)

::: /SIDEBAR :::
