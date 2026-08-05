# Local OCR

**Local OCR** (optical character recognition) was built to test a simple question: can local vision models do useful OCR work without bringing in a separate OCR engine? The agent takes images such as an article, a handwritten note, and a newspaper advertisement, then asks local models to return the text they can see. It also allows comparison with hosted frontier models, which is useful when “good enough locally” needs a sanity check from the expensive end of town.

The business shape is easy to imagine. A team has scans, photos, notices, labels, forms, or clippings, and wants the text out of them without sending every image to an online service. This proof of concept sketches that workflow. It treats OCR as something a local model can attempt directly, not as a special-purpose pipeline with a ceremonial procession of preprocessing steps.

The interesting result is not that every vision model is magically great at reading text. That would be suspiciously convenient. The useful lesson is more practical: specialized OCR models are generally faster and more accurate than general vision models, and can get close enough to hosted frontier models to make local OCR worth testing seriously.

::: SIDEBAR :::

Language: Python
Frameworks: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Models: [Gemma 4 12B](https://ollama.com/library/gemma4) | [GLM-OCR](https://ollama.com/library/glm-ocr) | [Granite 3.2 Vision](https://ollama.com/library/granite3.2-vision) | [MiniCPM-V](https://ollama.com/library/minicpm-v) | [DeepSeek-OCR](https://ollama.com/library/deepseek-ocr) | [LLaVA 13B](https://ollama.com/library/llava)
---

Repository: [GitHub](https://github.com/SomeNewKid/LocalOCR)

::: /SIDEBAR :::

::: OCR-REPORT name="local-model-ocr" title="Local OCR evaluation" :::
