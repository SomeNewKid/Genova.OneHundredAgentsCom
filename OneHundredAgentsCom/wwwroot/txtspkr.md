# TxtSpkr

TxtSpkr is a small CrewAI experiment about giving agents skills, not about solving the global crisis of adults using complete sentences. It takes a natural language message and turns it into terse TXT speak with emoji decoration, so a plain “Thank you. I’ll see you later.” can become something closer to the sort of compressed message that makes parents squint at their phones.

The useful part is not the slang itself. The useful part is how the behaviour is shaped. One specialist agent is guided by an SMS-speak skill. Another is guided by an emoji-selection skill. A final agent combines their outputs. The skills act as extra instructions and context inside the agents’ prompts, helping each agent handle its part of the job in a more deliberate way.

That makes TxtSpkr a modest workbench for testing where CrewAI skills fit. They are not magic plug-ins, secret tools, or little scripts that give an agent new powers. They are a way to package task-specific guidance so an agent can be steered more consistently.

::: SIDEBAR :::

Language: Python
Framework: [CrewAI](https://crewai.com/)
Pattern: Multi-agent
Model: [GPT-4.1-mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/TxtSpkr)

::: /SIDEBAR :::
