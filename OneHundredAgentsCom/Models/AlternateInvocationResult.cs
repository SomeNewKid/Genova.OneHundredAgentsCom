// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents the aggregated result of alternate invocation attempts recorded by the Sandbox Tester.
/// When a capability is probed the tester may try alternate shell commands or techniques;
/// this record captures the overall outcome, a short summary, and the individual attempts.
/// </summary>
internal sealed record AlternateInvocationResult
{
    /// <summary>
    /// Gets the overall outcome of the alternate invocation (for example: "Allowed", "Denied",
    /// "Not applicable", or "Failed").
    /// </summary>
    [JsonPropertyName("outcome")]
    public string Outcome { get; init; } = string.Empty;

    /// <summary>
    /// Gets a short human-readable summary describing the result of the alternate invocation attempts.
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; init; } = string.Empty;

    /// <summary>
    /// Gets the list of individual alternate attempts performed as part of this invocation.
    /// </summary>
    [JsonPropertyName("attempts")]
    public List<AlternateAttemptResult> Attempts { get; init; } = [];
}
