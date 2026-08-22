# Designer Blake

**Designer Blake** explores whether an Anthropic agent and model can take a pile of requirements and content assets, then turn them into a complete static website. The agent reads the brief, uses the supplied articles and images, creates the site files, and checks the result in a browser. That makes it a small but useful test of a business workflow: handing a structured web brief to an agent and asking for something that looks like a real first draft, not a folder full of wishful thinking.

The encouraging part is that Blake did produce the needed pages and assets, and the design quality was genuinely strong. It was able to follow the shape of the requirements, make layout choices, and refine the result after looking at the rendered site. For a proof of concept, that is the interesting bit.

The one clear boundary was bitmap image generation. Claude was much better at producing SVG and web code than inventing valid PNG files from text. That is not a disaster; it points toward a sensible production pattern: use the Anthropic agent for planning, design, and site assembly, then pair it with a dedicated image-generation model when raster artwork is required.

::: SIDEBAR :::

Language: Python
Framework: [Claude Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Pattern: Single agent
Model: [Claude Opus 5](https://www.anthropic.com/claude/opus)
---

Repository: [GitHub](https://github.com/SomeNewKid/DesignerBlake)

::: /SIDEBAR :::
