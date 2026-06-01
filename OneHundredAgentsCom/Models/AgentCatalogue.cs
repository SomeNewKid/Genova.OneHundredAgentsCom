// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Genova.Common.Utilities;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents the full catalogue of AI agents documented by the website.
/// </summary>
internal sealed class AgentCatalogue
{
    private const string CataloguePath = "Data/agent-catalogue.json";

    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Conflicting naming rules")]
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="AgentCatalogue"/> class.
    /// </summary>
    public AgentCatalogue()
    {
        Groups = LoadGroups();
    }

    /// <summary>
    /// Gets or sets the groups of agents in the catalogue.
    /// </summary>
    public List<AgentGroup> Groups { get; set; } = [];

    private static List<AgentGroup> LoadGroups()
    {
        string? json = GetEmbeddedText(CataloguePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return [];
        }

        AgentCatalogueData? data = JsonSerializer.Deserialize<AgentCatalogueData>(json, JsonSerializerOptions);
        return data?.Groups ?? [];
    }

    private static string? GetEmbeddedText(string filename)
    {
        string? content = null;
        Stream? stream = FileHelper.GetEmbeddedResourceStream(typeof(Website), filename);
        if (stream != null)
        {
            using StreamReader reader = new(stream);
            content = reader.ReadToEnd();
        }

        return content;
    }

    private sealed class AgentCatalogueData
    {
        public List<AgentGroup> Groups { get; set; } = [];
    }
}
