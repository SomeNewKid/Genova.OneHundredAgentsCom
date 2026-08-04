# Local image understanding

**Local image understanding** is a small proof of concept built to test a very practical question: can a local Gemma 4 model look at a photograph and describe it well enough to be useful? The agent takes a local image, sends it to the model, and records the model’s description. That simple loop sketches a business task that comes up often: turning visual material into searchable, reviewable text without sending every image to a hosted service.

The interesting part is the multimodal model running locally. In this experiment, the 12-billion parameter Gemma 4 model produced a strong description of the photograph, naming the street scene, people, dog, cactus bed, buildings, lighting, and visible signs with useful detail. That is the kind of output that could support cataloguing, evidence review, accessibility notes, or first-pass content inspection.

The catch is speed. On a mid-level laptop, the local model was not exactly sprinting. It was more like watching someone carefully read a menu through binoculars. Still, the result was real: a local model could describe the image well, just slowly.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Model: [Gemma 4](https://deepmind.google/models/gemma/gemma-4/)
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalImageUnderstanding)

::: /SIDEBAR :::
