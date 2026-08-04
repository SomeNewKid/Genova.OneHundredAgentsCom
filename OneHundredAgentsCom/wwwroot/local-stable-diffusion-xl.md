# Local Stable Diffusion XL

**Local Stable Diffusion XL** was built to test a simple question: how far can local image generation go on ordinary hardware, without sending the job to a hosted image service? It uses Stable Diffusion XL through ComfyUI to generate images from a small evaluation set, then uses a local model to inspect one counting task and suggest a sharper prompt when the image misses the mark.

The business shape is easy to imagine. A team could ask for product-style visuals, campaign concepts, or internal mockups, then have another local model check whether a visible requirement was met. This version is deliberately small, but it sketches a useful pattern: generate, inspect, revise, and keep a record of what happened. No dashboards, no procurement theatre, no “AI transformation journey” laminated for the boardroom.

The result is encouraging but modest. Stable Diffusion XL produces noticeably better images than the earlier Stable Diffusion 1.5 version, even on a mid-level laptop with no GPU. It is not fast, and it is not ready for real workloads. But it shows that a fully local image loop is practical enough to learn from.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/) | [ComfyUI](https://github.com/comfy-org/comfyui)
Pattern: Single agent
Models: [Stable Diffusion XL](https://stablediffusionxl.com/)
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalStableDiffusionXL)

::: /SIDEBAR :::
