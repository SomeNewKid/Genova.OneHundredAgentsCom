// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Html;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Utilities;

namespace Genova.OneHundredAgentsCom.Html;

/// <summary>
/// Wraps content between WRAPPER instructions in a div, applying any attributes declared by the start instruction.
/// </summary>
internal sealed class WrapperModifier : IHtmlModifier
{
    /// <inheritdoc/>
    public void Initialize(IExecutionContext executionContext, IWebsite website)
    {
        // No initialization required.
    }

    /// <inheritdoc/>
    public void Modify(IDocument document)
    {
        List<IElement> paragraphs = document.QuerySelectorAll("p").ToList();

        int startIndex = -1;
        int endIndex = -1;
        Dictionary<string, string> attributes = [];

        for (int i = 0; i < paragraphs.Count; i++)
        {
            string text = paragraphs[i].TextContent.Trim();
            if (IsWrapperStart(text))
            {
                startIndex = i;
                attributes = InstructionHelper.ParseAttributes(text);
            }
            else if (IsWrapperEnd(text))
            {
                endIndex = i;
                break;
            }
        }

        if (startIndex == -1 || endIndex == -1 || endIndex <= startIndex)
        {
            return;
        }

        List<IElement> elementsToWrap = paragraphs.GetRange(startIndex + 1, endIndex - startIndex - 1);

        IElement div = document.CreateElement("div");
        foreach (KeyValuePair<string, string> attribute in attributes)
        {
            div.SetAttribute(attribute.Key, attribute.Value);
        }

        foreach (IElement element in elementsToWrap)
        {
            div.AppendChild(element);
        }

        IElement? parent = paragraphs[startIndex].ParentElement;
        parent?.InsertBefore(div, paragraphs[endIndex]);

        paragraphs[startIndex].Remove();
        paragraphs[endIndex].Remove();
    }

    private static bool IsWrapperStart(string text)
    {
        return text.StartsWith(":::", StringComparison.Ordinal) &&
               text.Contains(" WRAPPER ", StringComparison.OrdinalIgnoreCase) &&
               text.EndsWith(":::", StringComparison.Ordinal);
    }

    private static bool IsWrapperEnd(string text)
    {
        return text.StartsWith(":::", StringComparison.Ordinal) &&
               text.Contains(" /WRAPPER ", StringComparison.OrdinalIgnoreCase) &&
               text.EndsWith(":::", StringComparison.Ordinal);
    }
}
