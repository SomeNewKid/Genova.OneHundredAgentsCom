// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents the result of a single capability check performed by the Sandbox Tester.
/// Each capability has an identifier and title and contains results for the different
/// invocation paths exercised by the tester (shell command, tool invocation, and any alternates).
/// </summary>
internal sealed record CapabilityResult
{
    /// <summary>
    /// Gets the identifier of the capability (for example, "T01").
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Gets the human-readable title of the capability being tested.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the result produced when the capability was exercised via a shell command.
    /// </summary>
    [JsonPropertyName("shell")]
    public InvocationResult Shell { get; init; } = new();

    /// <summary>
    /// Gets the result produced when the capability was exercised via the agent's tool (Python code).
    /// </summary>
    [JsonPropertyName("tool")]
    public InvocationResult Tool { get; init; } = new();

    /// <summary>
    /// Gets the aggregated results for alternate invocation attempts (if any) used to probe the capability.
    /// </summary>
    [JsonPropertyName("alternates")]
    public AlternateInvocationResult Alternates { get; init; } = new();
}
