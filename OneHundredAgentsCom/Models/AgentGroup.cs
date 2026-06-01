// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a named group of related AI agents.
/// </summary>
internal sealed class AgentGroup
{
    /// <summary>
    /// Gets or sets the display title of the agent group.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the agent group.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the agents that belong to the group.
    /// </summary>
    public List<AgentEntry> Agents { get; set; } = [];
}
