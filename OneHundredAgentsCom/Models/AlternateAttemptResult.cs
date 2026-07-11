// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a single alternate shell attempt recorded by the Sandbox Tester.
/// Maps to the JSON object describing an alternate attempt performed when probing
/// a capability via alternate shell commands or techniques.
/// </summary>
internal sealed record AlternateAttemptResult
{
    /// <summary>
    /// Gets the identifier of the alternate attempt (for example, "A01").
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Gets the human-readable title of the alternate attempt.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the outcome of the attempt (for example: "Allowed", "Denied", "Not applicable", "Failed").
    /// </summary>
    [JsonPropertyName("outcome")]
    public string Outcome { get; init; } = string.Empty;

    /// <summary>
    /// Gets the bypass classification used to categorise the attempt (for example: "alternate_command").
    /// </summary>
    [JsonPropertyName("bypass_class")]
    public string BypassClass { get; init; } = string.Empty;

    /// <summary>
    /// Gets the command family or mechanism used by the attempt (for example: "cmd/environment", "powershell").
    /// </summary>
    [JsonPropertyName("command_family")]
    public string CommandFamily { get; init; } = string.Empty;

    /// <summary>
    /// Gets any supporting evidence collected for the attempt, such as command output or file paths.
    /// </summary>
    [JsonPropertyName("evidence")]
    public string Evidence { get; init; } = string.Empty;
}
