// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a single AI agent documented by the website.
/// </summary>
internal sealed class AgentEntry
{
    private string _slug = string.Empty;

    /// <summary>
    /// Gets or sets the display name of the agent.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the agent.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL slug used for the agent page and related image assets. If no value is set, the slug is
    /// derived from the agent name.
    /// </summary>
    public string Slug
    {
        get
        {
            if (!string.IsNullOrEmpty(_slug))
            {
                return _slug;
            }

            return Name.ToLowerInvariant().Replace(' ', '-');
        }

        set
        {
            _slug = value;
        }
    }
}
