# Container sandbox with hardware control

Sandbox Tester running in a Docker container with limited hardware access explores a very plain question: what can an AI agent see about the machine around it? Not what the sandbox claims. Not what the diagram promises. What it can actually probe. This version focuses on nearby hardware, because most agents do not need to know about cameras, microphones, USB devices, serial ports, Bluetooth devices, or printers.

Before this step, the Docker sandbox had 223 allowed probe paths. After adding a `hardware-device-control` profile, the report moved to 207 allowed. Sixteen probe paths moved from allowed to denied.

That is a useful kind of boring. The agent can still do the intended work, but it is much less aware of the physical world around the container. Camera, microphone, USB, serial, Bluetooth, and printer inspection paths were blocked. GPU details were left visible for separate measurement. The result is not magic safety dust. It is a clearer boundary, measured rather than assumed.

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

::: SANDBOX-REPORT name="sandbox-container-controlled-hardware" title="Sandbox Report - Local Container with Hardware Control" :::
