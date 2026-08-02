// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a single row from the embedded local model evaluation JSON.
/// Property names are mapped to the JSON keys (some of which contain spaces).
/// </summary>
internal sealed class LocalModelEvaluation
{
    /// <summary>
    /// Gets or sets the name of the model.
    /// </summary>
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the size of the model in gigabytes (GB).
    /// </summary>
    [JsonPropertyName("Size")]
    public double Size { get; set; }

    /// <summary>
    /// Gets or sets the overall evaluation score of the model,
    /// on a scale from 0 to 100.
    /// </summary>
    [JsonPropertyName("Score")]
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for the model's extraction
    /// capabilities, on a scale from 0 to 5.
    /// </summary>
    [JsonPropertyName("Extraction")]
    public int Extraction { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for the model's ability
    /// to adhere to a schema, on a scale from 0 to 5.
    /// </summary>
    [JsonPropertyName("Schema")]
    public int Schema { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for the model's accuracy
    /// in generating reports, on a scale from 0 to 5.
    /// </summary>
    [JsonPropertyName("Report Accuracy")]
    public int ReportAccuracy { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for the model's style
    /// in generating reports, on a scale from 0 to 5.
    /// </summary>
    [JsonPropertyName("Report Style")]
    public int ReportStyle { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for the model's ability
    /// to call a tool to retrieve an ID, on a scale from 0 to 5.
    /// </summary>
    [JsonPropertyName("ID Tool")]
    public int IDTool { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for the model's ability
    /// to call a tool to retrieve credit information, on a scale from 0 to 5.
    /// </summary>
    [JsonPropertyName("Credit Tool")]
    public int CreditTool { get; set; }
}
