# Movie poster team

**Movie poster team** is a small proof of concept built to test whether multiple AI agents can work together without pretending that every job is instant. A manager agent asks a writer agent for a movie idea, then asks an artist agent to create an illustration, then asks a poster agent to turn the idea and artwork into a final poster. The business sketch is simple: a creative team passing work between specialists. The interesting part is not the poster. The interesting part is the handoff.

Image generation takes time, so this project uses agent-to-agent tasks rather than treating every request as a quick chat message. The artist and poster agents can accept work, run it separately, and return results when the task is complete. That is a more honest shape for many business workflows, where one agent may need to wait on a model, a tool, or another system before the next step can begin.

The agents also run in separate Docker containers with declared capabilities. That makes the experiment less hand-wavy. It shows how A2A can support a coordinated team of agents while keeping each agent's role and permissions visible.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Multiple agents
Sandbox: Docker localnet | AI agent containers | Squid proxy sidecar
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/MoviePosterTeam)

::: /SIDEBAR :::

::: WRAPPER class="generated-image" :::

Here is the final poster created by the AI agent team.

![Movie poster created by the AI agent team](/-/images/movie-poster-team.jpg)

::: /WRAPPER :::
