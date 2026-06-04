# Marcie Chatbot Agent

Marcie Chatbot Agent was built as a first pass at LangChain: not to make a grand assistant, but to see what it feels like to connect a chat model to tools and let the framework manage the conversation. The agent runs as a simple command-line chatbot. It can answer ordinary questions from the model, but it can also call tools for things that should not be left to confident guesswork, such as weather lookups, date differences, and calculator operations.

The useful business shape is a small support-style assistant that can mix conversation with specific actions. A user can ask about tomorrow’s weather, calculate a number, or ask how many days sit between two dates, without needing to know which tool should be used. LangChain handles the agent pattern, while the project explores how much guidance the model needs to choose tools sensibly.

The interesting lesson is practical rather than glamorous. LangChain made it fairly quick to wire together a local model, a prompt, and a few tools. It also made the rough edges visible: models can guess, tools can fail, and prompts are not magic contracts. That is exactly why this was worth building.

::: SIDEBAR :::

Language: Python
Framework: [LangChain](https://www.langchain.com/)
Pattern: Single-agent, Chatbot
Integrations: [Open-Meteo Geocoding API](https://open-meteo.com/) | [Open-Meteo Forecast API](https://open-meteo.com/)
Model: [IBM Granite 4.1](https://www.ibm.com/granite)
---

Repository: [GitHub](https://github.com/SomeNewKid/MarcieChatbotAgent)

::: /SIDEBAR :::

