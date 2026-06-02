// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using FluentAssertions;
using Genova.Common.Execution;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Html;

namespace Genova.OneHundredAgentsCom.UnitTests.Html;

public class WrapperModifier_Tests : Modifier_Tests
{
    [Fact]
    public async Task Modify_should_not_throw_if_article_missing()
    {
        string html = """
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <h1>No Article</h1>
                </main>
              </body>
            </html>
            """;
        IDocument document = await ParseHtmlAsync(html);

        IExecutionContext executionContext = MockExecutionContextWithPathAndQuery("/travel-landmark-agent");
        IWebsite website = MockWebsite("en");

        WrapperModifier modifier = new();
        modifier.Initialize(executionContext, website);

        modifier.Invoking(candidate => candidate.Modify(document)).Should().NotThrow();
    }

    [Fact]
    public async Task Modify_should_use_an_empty_div_if_the_instruction_has_no_attributes()
    {
        IDocument document = await CreateDocumentAsync("::: WRAPPER :::");

        Modify(document);

        AssertWrappedParagraphs(document);
        document.QuerySelector("main > div")!.Attributes.Should().BeEmpty();
    }

    [Fact]
    public async Task Modify_should_use_a_wrapper_with_the_specified_id()
    {
        IDocument document = await CreateDocumentAsync("::: WRAPPER id=\"my-wrapper\" :::");

        Modify(document);

        AssertWrappedParagraphs(document);
        document.QuerySelector("main > div")!.GetAttribute("id").Should().Be("my-wrapper");
    }

    [Fact]
    public async Task Modify_should_use_a_wrapper_with_the_specified_class()
    {
        IDocument document = await CreateDocumentAsync("::: WRAPPER class=\"reading-area\" :::");

        Modify(document);

        AssertWrappedParagraphs(document);
        document.QuerySelector("main > div")!.GetAttribute("class").Should().Be("reading-area");
    }

    [Fact]
    public async Task Modify_should_use_a_wrapper_with_the_specified_id_and_class()
    {
        IDocument document = await CreateDocumentAsync("::: WRAPPER id=\"my-wrapper\" class=\"reading-area\" :::");

        Modify(document);

        AssertWrappedParagraphs(document);
        IElement div = document.QuerySelector("main > div")!;
        div.GetAttribute("id").Should().Be("my-wrapper");
        div.GetAttribute("class").Should().Be("reading-area");
    }

    private static async Task<IDocument> CreateDocumentAsync(string wrapperStart)
    {
        string html = $$"""
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <h1>No Article</h1>
                  <p>Before the wrapper.</p>
                  <p>{{wrapperStart}}</p>
                  <p>First paragraph.</p>
                  <p>Second paragraph.</p>
                  <p>::: /WRAPPER :::</p>
                  <p>After the wrapper.</p>
                  <p>::: OTHER INSTRUCTION :::</p>
                </main>
              </body>
            </html>
            """;

        return await ParseHtmlAsync(html);
    }

    private static void Modify(IDocument document)
    {
        IExecutionContext executionContext = MockExecutionContextWithPathAndQuery("/travel-landmark-agent");
        IWebsite website = MockWebsite("en");

        WrapperModifier modifier = new();
        modifier.Initialize(executionContext, website);
        modifier.Modify(document);
    }

    private static void AssertWrappedParagraphs(IDocument document)
    {
        document.QuerySelectorAll("p")
            .Should()
            .NotContain(paragraph => paragraph.TextContent.Contains("::: WRAPPER", StringComparison.OrdinalIgnoreCase));

        document.QuerySelectorAll("p")
            .Should()
            .NotContain(paragraph => paragraph.TextContent.Contains("::: /WRAPPER", StringComparison.OrdinalIgnoreCase));

        document.QuerySelectorAll("p")
            .Should()
            .ContainSingle(paragraph =>
                paragraph.TextContent.Contains("::: OTHER INSTRUCTION :::", StringComparison.OrdinalIgnoreCase));

        IElement? div = document.QuerySelector("main > div");
        div.Should().NotBeNull();

        div!.Children.Should()
            .ContainSingle(element => element.TagName == "P" && element.TextContent.Trim() == "First paragraph.");
        div.Children.Should()
            .ContainSingle(element => element.TagName == "P" && element.TextContent.Trim() == "Second paragraph.");

        IElement? main = document.QuerySelector("main");
        main.Should().NotBeNull();

        List<IElement> mainChildren = main!.Children.ToList();
        IElement? beforeParagraph = mainChildren.FirstOrDefault(element =>
            element.TagName == "P" && element.TextContent.Trim() == "Before the wrapper.");
        IElement? afterParagraph = mainChildren.FirstOrDefault(element =>
            element.TagName == "P" && element.TextContent.Trim() == "After the wrapper.");
        IElement? divInMain = mainChildren.FirstOrDefault(element => element.TagName == "DIV");

        beforeParagraph.Should().NotBeNull();
        afterParagraph.Should().NotBeNull();
        divInMain.Should().NotBeNull();

        int beforeIndex = mainChildren.IndexOf(beforeParagraph!);
        int divIndex = mainChildren.IndexOf(divInMain!);
        int afterIndex = mainChildren.IndexOf(afterParagraph!);

        divIndex.Should().BeGreaterThan(beforeIndex);
        divIndex.Should().BeLessThan(afterIndex);
    }
}
