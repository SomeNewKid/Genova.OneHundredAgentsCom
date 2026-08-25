// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a single row from the embedded local coding evaluation JSON.
/// Property names are mapped to the JSON keys (some of which contain spaces).
/// </summary>
internal sealed class LocalCodeEvaluation
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
    /// Gets or sets the evaluation score for code generation (0..100).
    /// </summary>
    [JsonPropertyName("Code generation")]
    public int CodeGeneration { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for bug repair (0..100).
    /// </summary>
    [JsonPropertyName("Bug repair")]
    public int BugRepair { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for code completion (0..100).
    /// </summary>
    [JsonPropertyName("Code completion")]
    public int CodeCompletion { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for refactoring (0..100).
    /// </summary>
    [JsonPropertyName("Refactoring")]
    public int Refactoring { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for test generation (0..100).
    /// </summary>
    [JsonPropertyName("Test generation")]
    public int TestGeneration { get; set; }

    /// <summary>
    /// Gets or sets the evaluation score for code explanation (0..100).
    /// </summary>
    [JsonPropertyName("Code explanation")]
    public int CodeExplanation { get; set; }
}
