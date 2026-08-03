# Local stable diffusion

**Local stable diffusion** explores a very practical question: what can a laptop do when the whole image workflow stays local? It uses Stable Diffusion&nbsp;1.5 to generate images from prompts, then uses a local Ollama model to inspect one generated image and check whether it meets a simple requirement. In this case, the requirement is wonderfully unforgiving: exactly five green apples in a wooden bowl. Counting apples sounds easy until the model cheerfully gives you four, six, or seven.

The agent sketches a business capability that could matter in creative review, catalogue imagery, or brand QA: generate an asset, inspect it, and try a better prompt when the result misses the brief. It is not just image generation. It is a small feedback loop across local image creation, local image inspection, and local prompt revision.

The useful finding is not that this setup is ready for real work. It is not. On a mid-level laptop with no GPU, Stable Diffusion&nbsp;1.5 can run, but slowly, and exact visual control remains shaky. The value is seeing the shape of a local creative agent without pretending the laptop has secretly become a production studio.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Model: [Stable Diffusion 1.5](https://huggingface.co/stable-diffusion-v1-5/stable-diffusion-v1-5)
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalStableDiffusion)

::: /SIDEBAR :::
