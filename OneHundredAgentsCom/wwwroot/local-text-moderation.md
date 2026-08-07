# Local text moderation

**Local text moderation** explores a simple but useful question: can a local model catch clearly unsafe user input before the application sends that input any further? The agent tests local models that specialize in moderation, then compares their decisions with the OpenAI Moderation API. The business shape is easy to see. If the local model says an input is clearly unsafe, the application can stop early, avoid extra processing, and avoid sending obviously problematic text downstream.

One lesson was fairly blunt: general-purpose local models are not a good fit for this job. When asked to classify risky content, they often did the very model-ish thing of refusing to answer the moderation question at all. That may be polite, but it is not a useful gate. Specialized moderation models behaved much more like classifiers, returning usable safety judgments that could be compared across categories such as sexual content, violence, self-harm, harassment, illicit activity, and child-related risk.

This proof of concept points toward a practical layered approach. Local moderation can provide an early stop sign. But anything that continues to a frontier model should still go through that frontier provider’s own moderation endpoint.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Models: [Granite Guardian 3](https://ollama.com/library/granite3-guardian) | [Granite Guardian 3 8B](https://ollama.com/library/granite3-guardian) | [Granite Guardian 4.1 8B](https://ollama.com/library/granite4.1-guardian) | [Llama Guard 3 1B](https://ollama.com/library/llama-guard3) | [Llama Guard 3 8B](https://ollama.com/library/llama-guard3) | [ShieldGemma 2B](https://ollama.com/library/shieldgemma)
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalTextModeration)

::: /SIDEBAR :::
