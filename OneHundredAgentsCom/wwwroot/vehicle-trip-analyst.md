# Vehicle trip analyst

**Vehicle trip analyst** was built to test Hugging Face’s smolagents framework on a small, useful business task: asking questions about vehicle trip data in plain English. The user can ask things like which driver took the most trips, which vehicle travelled the furthest, or which vehicles have no recorded journeys. The agent turns the question into a database query, checks the result, and replies in normal language.

The learning focus was smolagents itself. The “smol” idea fits the project well: keep the agent small, keep the code understandable, and see how quickly a code-focused agent can be wired to a real tool. For this kind of proof of concept, the framework felt direct and approachable.

The more interesting part came after the first success. Once the agent could query the database, safeguards were added so it only performed safe read-only queries. That changed the experiment from “can it produce SQL?” to “can it be useful while staying inside a clear boundary?” That is the shape of the business capability: natural language access to operational data, with controls around what the agent is allowed to do.

::: SIDEBAR :::

Language: Python
Framework: [smolagents](https://huggingface.co/docs/smolagents/index)
Pattern: Single agent
Model: [Llama-3.1-8B-Instruct](https://huggingface.co/meta-llama/Llama-3.1-8B-Instruct)
---

Repository: [GitHub](https://github.com/SomeNewKid/VehicleTripAnalyst)

::: /SIDEBAR :::
