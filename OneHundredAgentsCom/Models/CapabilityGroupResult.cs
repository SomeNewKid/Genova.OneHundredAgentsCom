// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a group of capability results produced by the Sandbox Tester.
/// A capability group contains an identifier, a human-readable title,
/// and the list of capability checks that belong to this category
/// (for example, "G01" / "Runtime identity and execution context").
/// </summary>
internal sealed record CapabilityGroupResult
{
    /// <summary>
    /// Gets the identifier of the capability group (for example, "G01").
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Gets the human-readable title of the capability group.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the list of capability results that belong to this group.
    /// </summary>
    [JsonPropertyName("capabilities")]
    public List<CapabilityResult> Capabilities { get; init; } = [];
}
