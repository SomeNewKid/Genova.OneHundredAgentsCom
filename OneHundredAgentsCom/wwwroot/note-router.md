# Note Router

Note Router began as a simple test of OpenClaw: could it take a small, local task and turn it into an agent workflow without too much ceremony? The task was deliberately plain. A folder contains text notes in different languages. The agent checks each note, identifies the language, and moves the file into a matching folder such as `en`, `zh`, or `fr`. Nobody is retiring on this business model, but the shape is familiar: sort incoming documents so the next person or system starts with less mess.

The interesting part was not language detection. It was using OpenClaw with a custom skill to describe the workflow and let the agent carry it out. That worked. The skill gave the agent a repeatable job, and OpenClaw connected the model to local file actions. It felt like a real agent framework rather than a decorated API call, at least once the right incantations were found.

The experiment also made the warning label hard to ignore. File-moving agents need tight boundaries, and OpenClaw’s convenience did not make security feel simple. It is promising, but not a tool to casually trust with serious folders.

::: SIDEBAR :::

Language: Python
Framework: [OpenClaw](https://openclaw.ai/)
Pattern: Single agent
Model: [GPT-5.5](https://developers.openai.com/api/docs/models/gpt-5.5)
---

Repository: [GitHub](https://github.com/SomeNewKid/NoteRouter)

::: /SIDEBAR :::
