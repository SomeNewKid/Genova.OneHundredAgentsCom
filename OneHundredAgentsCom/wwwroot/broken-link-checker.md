# Broken Link Checker

Broken Link Checker was built to test Cline CLI in a small but real business task: given a website, find links that point to dead or misleading destinations. The target was my own site, so the job was concrete enough to catch real behaviour rather than admire a demo. The agent eventually produced a command-line checker that crawled internal pages, tested external links, followed redirects, and identified a known bad link.

The more interesting result was not the link checker. It was the Cline CLI evaluation. Small local models were not useful for this task. They were too slow, too weak at tool use, or both. A hosted model did much better, but still needed narrow instructions and firm guardrails. Without them, Cline could misread tool results, damage files, or claim success too early.

So this agent sketches a useful business capability, but also offers a useful warning. Cline can generate working software when the task is bounded tightly. It is not magic. It is a sharp tool that still needs a careful hand nearby.

::: SIDEBAR :::

Language: Python
Framework: [Cline CLI](https://cline.bot/)
Pattern: Coding agent
Successful Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
Unsuccessful Models: [Qwen3 4B](https://huggingface.co/Qwen/Qwen3-4B) | [Qwen3 1.7B](https://huggingface.co/Qwen/Qwen3-1.7B) | [Phi-4-mini](https://huggingface.co/microsoft/Phi-4-mini-instruct)  
---
Repository: [GitHub](https://github.com/SomeNewKid/BrokenLinkChecker)

::: /SIDEBAR :::
# Broken Link Checker
