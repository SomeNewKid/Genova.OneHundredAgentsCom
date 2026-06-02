# Travel Landmark Agent

The Travel Landmark Agent was built as a first practical test of IBM’s BeeAI Framework, not as an attempt to replace a travel desk. It sketches a small business travel assistant that can take a structured travel request, identify the destination and travel context, and bring back the kinds of information a travelling employee might need: expected weather, a landmark worth knowing about, and any relevant company travel policies.

The useful part is not the travel advice itself. The useful part is the shape of the agent. BeeAI gives the build a way to connect tools, resources, and narrower helper capabilities so the agent can assemble an answer rather than simply improvise one. That matters for business agents, because real work usually means checking more than one thing before giving a response. “Can I go there, what should I expect, and what rules apply?” is a better test than asking the model to write another cheerful paragraph about Rome.

This build stays deliberately small. It shows the outline of a business agent: one question, several supporting checks, one practical answer. That is enough for a proof of concept.

::: SIDEBAR :::

Language: Python
Framework: IBM BeeAI Framework
Pattern: Multi-agent
Integrations: MediaWiki API | Open-Meteo Geocoding API | Open-Meteo Forecast API
Model: IBM Granite 3.3
---
Repository: [GitHub](https://github.com/SomeNewKid/TravelLandmarkAgent)

::: /SIDEBAR :::
