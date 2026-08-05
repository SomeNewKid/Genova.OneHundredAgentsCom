// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Genova.OneHundredAgentsCom.Models;

/// <summary>
/// Represents a single row from the embedded local OCR evaluation JSON.
/// Property names are mapped to the JSON keys (which use snake_case).
/// </summary>
internal sealed class LocalOcrEvaluation
{
    /// <summary>
    /// Gets or sets the model name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the average score for the model.
    /// </summary>
    [JsonPropertyName("average_score")]
    public double AverageScore { get; set; }

    /// <summary>
    /// Gets or sets the speed score for the model.
    /// </summary>
    [JsonPropertyName("speed_score")]
    public double SpeedScore { get; set; }

    /// <summary>
    /// Gets or sets the final combined score for the model.
    /// </summary>
    [JsonPropertyName("final_score")]
    public double FinalScore { get; set; }

    /// <summary>
    /// Gets or sets the total duration as presented in the JSON (e.g. "01m 02s").
    /// </summary>
    [JsonPropertyName("total_duration")]
    public string TotalDuration { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the model size in gigabytes (GB).
    /// </summary>
    [JsonPropertyName("size")]
    public float Size { get; set; }

    /// <summary>
    /// Gets or sets the article sub-evaluation (score and duration).
    /// </summary>
    [JsonPropertyName("article")]
    public OcrCategoryResult Article { get; set; } = new();

    /// <summary>
    /// Gets or sets the hardwriting sub-evaluation (score and duration).
    /// </summary>
    [JsonPropertyName("hardwriting")]
    public OcrCategoryResult Hardwriting { get; set; } = new();

    /// <summary>
    /// Gets or sets the poster sub-evaluation (score and duration).
    /// </summary>
    [JsonPropertyName("poster")]
    public OcrCategoryResult Poster { get; set; } = new();

    /// <summary>
    /// Represents a per-task result in the OCR evaluation JSON.
    /// </summary>
    internal sealed class OcrCategoryResult
    {
        /// <summary>
        /// Gets or sets the integer score for this category.
        /// </summary>
        [JsonPropertyName("score")]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the duration string for this category (e.g. "00m 37s").
        /// </summary>
        [JsonPropertyName("duration")]
        public string Duration { get; set; } = string.Empty;
    }
}
