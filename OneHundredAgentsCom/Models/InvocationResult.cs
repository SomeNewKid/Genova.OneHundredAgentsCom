// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents the result of a single invocation performed by the Sandbox Tester.
/// The sandbox tester runs the same capability check via different invocation paths
/// (for example: shell command, AI tool code, or alternate shell attempts).
///  Thisrecord holds the outcome for one such invocation.
/// Typical <see cref="Outcome"/> values include "Allowed", "Denied", "Not applicable", or "Failed".
/// </summary>
internal sealed record InvocationResult
{
    /// <summary>
    /// Gets the outcome of the invocation (for example: "Allowed", "Denied", "Not applicable", "Failed").
    /// </summary>
    [JsonPropertyName("outcome")]
    public string Outcome { get; init; } = string.Empty;

    /// <summary>
    /// gets a short human-readable summary describing what happened during the invocation.
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; init; } = string.Empty;

    /// <summary>
    /// Gets any supporting evidence collected for the invocation, such as command output,
    /// file paths, or other text that substantiates the outcome.
    /// </summary>
    [JsonPropertyName("evidence")]
    public string Evidence { get; init; } = string.Empty;
}
