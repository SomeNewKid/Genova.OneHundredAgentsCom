# Container Sandbox with a Syscomp Profile

Sandbox Tester running in a Docker container with a custom seccomp (Secure Computing Mode) profile is a proof of concept for a less glamorous but important question: what happens when an AI agent is allowed to keep doing its job, but loses access to some deeper operating system machinery? The agent runs inside the container and checks what the environment permits. It then reports which actions worked, failed, or did not apply.

This version keeps the earlier Docker hardening: restricted files, controlled network access, fewer ambient services, and fewer available command families. The new `syscall-control` profile adds a custom seccomp layer. That means the container can still run Python, Playwright, Chromium, and the OpenAI API test, but blocks selected low-level kernel requests around mounts, namespaces, keyrings, process inspection, kernel modules, BPF, and performance events.

The interesting result is almost boring, which is often the best kind of security result. The visible capability report did not change from the previous profile. No tested action moved from allowed to denied. But the sandbox gained a quieter guardrail underneath the agent. Fewer trapdoors, same useful work.

::: SIDEBAR :::

Language: Python
Framework: None
Pattern: Single agent
Sandbox: Ubuntu container, Docker
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxTester)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-container-restricted" title="Sandbox Report - Local Container with a Syscomp Profile" :::
