# Designer Chase

**Designer Chase** tests whether a Google agent and Gemini model can take a requirements document and a set of content assets, then build a complete static site. That is a useful business-shaped task. Many teams have the same problem in miniature: the brief exists, the content exists, and someone still has to turn it into pages that look considered rather than assembled during a calendar emergency.

The encouraging result was that Chase did generate the needed website files, and the design quality was strong. It read the brief, copied the source assets, wrote the pages, added styling, and checked the result in a browser. The site it produced was not merely technically present. It had real visual judgement.

The limits were also clear. Gemini 3.6 Flash is not a raster image-generation model, so bitmap artwork needs a separate tool or model. The agent also showed some tendency to run into token limits. Still, as a proof of concept, Chase points toward a practical pattern: use a Google agent for site assembly and design direction, then pair it with specialist tools where needed.

::: SIDEBAR :::

Language: Python
Framework: [Agent Development Kit](https://adk.dev/)
Pattern: Single agent
Model: [Gemini 3.6 Flash](https://ai.google.dev/gemini-api/docs/models/gemini-3.6-flash)
---

Repository: [GitHub](https://github.com/SomeNewKid/Designer Chase)

::: /SIDEBAR :::
