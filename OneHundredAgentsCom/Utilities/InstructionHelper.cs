// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

namespace Genova.OneHundredAgentsCom.Utilities;

/// <summary>
/// Provides utility methods for working with Markdown instructions.
/// </summary>
internal static class InstructionHelper
{
    /// <summary>
    /// Parses a string for attribute key-value pairs in the format <c>name="value"</c>.
    /// </summary>
    /// <param name="text">
    /// The input string containing attribute definitions, typically from a placeholder element.
    /// </param>
    /// <returns>
    /// A dictionary containing all parsed attribute names and their corresponding values.
    /// </returns>
    internal static Dictionary<string, string> ParseAttributes(string text)
    {
        Dictionary<string, string> dictionary = new(StringComparer.OrdinalIgnoreCase);

        int i = 0;
        while (i < text.Length)
        {
            while (i < text.Length && (char.IsWhiteSpace(text[i]) || text[i] == ':'))
            {
                i++;
            }

            if (i >= text.Length)
            {
                break;
            }

            int keyStart = i;
            while (i < text.Length && text[i] != '=' && !char.IsWhiteSpace(text[i]) && text[i] != ':')
            {
                i++;
            }

            if (i >= text.Length || text[i] != '=')
            {
                i++;
                continue;
            }

            string key = text.Substring(keyStart, i - keyStart).Trim();

            i++;

            if (i < text.Length && text[i] == '"')
            {
                i++;
                int valueStart = i;
                while (i < text.Length && text[i] != '"')
                {
                    i++;
                }

                string value = text.Substring(valueStart, i - valueStart);
                dictionary[key] = value;
                if (i < text.Length)
                {
                    i++;
                }
            }
            else
            {
                while (i < text.Length && !char.IsWhiteSpace(text[i]) && text[i] != '"')
                {
                    i++;
                }
            }
        }

        return dictionary;
    }
}
