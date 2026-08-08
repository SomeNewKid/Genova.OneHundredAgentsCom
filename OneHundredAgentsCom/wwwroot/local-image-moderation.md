# Local image moderation

**Local image moderation** tests a simple but important question: when a user uploads an image, should an AI application keep processing it, or stop because the content may be unsafe? The agent compares specialist moderation tools rather than asking a general-purpose vision model to improvise a safety judgement. That matters. A model that can describe a picture is not automatically the right tool for deciding whether the picture is safe to handle.

The agent runs the same image set through NudeNet, Marqo’s NSFW classifier, ShieldGemma 2, and the OpenAI Moderation API. It records each tool’s raw response, maps the results into shared safety categories, and times each evaluation. This makes the trade-offs visible instead of hand-wavy.

NudeNet and Marqo are fast and practical, but narrow. They are useful for specific signals, not broad policy coverage. ShieldGemma 2 points toward richer local moderation, including custom policy checks, but it is painfully slow on ordinary hardware. The useful lesson is not that one model wins. It is that image safety needs purpose-built moderation tools, chosen with eyes open.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Models: [NudeNet](https://pypi.org/project/nudenet/) | [Marqo](https://huggingface.co/Marqo/nsfw-image-detection-384) | [ShieldGemma 2](https://huggingface.co/google/shieldgemma-2-4b-it) | [OpenAI Moderation](https://developers.openai.com/api/docs/models/omni-moderation-latest)
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalImageModeration)

::: /SIDEBAR :::
