// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using FluentAssertions;
using Genova.Common.Execution;
using Genova.Common.Websites;
using Genova.OneHundredAgentsCom.Html;

namespace Genova.OneHundredAgentsCom.UnitTests.Html;

public class SidebarModifier_Tests : Modifier_Tests
{
    [Fact]
    public async Task Modify_should_convert_sidebar_paragraphs_to_a_definition_list()
    {
        string html = """
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <article>
                    <p>Before the sidebar.</p>
                    <p>::: SIDEBAR :::</p>
                    <p>Language: Python</p>
                    <p>Framework: BeeAI Framework</p>
                    <p>::: /SIDEBAR :::</p>
                    <p>After the sidebar.</p>
                  </article>
                </main>
              </body>
            </html>
            """;
        IDocument document = await ParseHtmlAsync(html);

        Modify(document);

        IElement sidebar = AssertSidebar(document);
        sidebar.QuerySelectorAll("dl").Should().HaveCount(1);
        sidebar.QuerySelectorAll("dt").Select(element => element.TextContent).Should().Equal("Language", "Framework");
        sidebar.QuerySelectorAll("dd").Select(element => element.TextContent).Should().Equal("Python", "BeeAI Framework");
        AssertSidebarPosition(document);
    }

    [Fact]
    public async Task Modify_should_start_a_new_definition_list_after_a_horizontal_rule()
    {
        string html = """
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <article>
                    <p>Before the sidebar.</p>
                    <p>::: SIDEBAR :::</p>
                    <p>Language: Python</p>
                    <hr/>
                    <p>Repository: GitHub</p>
                    <p>::: /SIDEBAR :::</p>
                    <p>After the sidebar.</p>
                  </article>
                </main>
              </body>
            </html>
            """;
        IDocument document = await ParseHtmlAsync(html);

        Modify(document);

        IElement sidebar = AssertSidebar(document);
        IHtmlCollection<IElement> lists = sidebar.QuerySelectorAll("dl");
        lists.Should().HaveCount(2);
        lists[0].QuerySelector("dt")!.TextContent.Should().Be("Language");
        lists[0].QuerySelector("dd")!.TextContent.Should().Be("Python");
        lists[1].QuerySelector("dt")!.TextContent.Should().Be("Repository");
        lists[1].QuerySelector("dd")!.TextContent.Should().Be("GitHub");
    }

    [Fact]
    public async Task Modify_should_preserve_inline_HTML_in_definition_descriptions()
    {
        string html = """
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <article>
                    <p>Before the sidebar.</p>
                    <p>::: SIDEBAR :::</p>
                    <p>Repository: <a href="https://github.com/SomeNewKid/TravelLandmarkAgent">GitHub</a></p>
                    <p>::: /SIDEBAR :::</p>
                    <p>After the sidebar.</p>
                  </article>
                </main>
              </body>
            </html>
            """;
        IDocument document = await ParseHtmlAsync(html);

        Modify(document);

        IElement sidebar = AssertSidebar(document);
        IElement? link = sidebar.QuerySelector("dd a");
        link.Should().NotBeNull();
        link!.GetAttribute("href").Should().Be("https://github.com/SomeNewKid/TravelLandmarkAgent");
        link.TextContent.Should().Be("GitHub");
    }

    [Fact]
    public async Task Modify_should_convert_pipe_separated_descriptions_to_an_unordered_list()
    {
        string html = """
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <article>
                    <p>Before the sidebar.</p>
                    <p>::: SIDEBAR :::</p>
                    <p>Integrations: MediaWiki API | Open-Meteo Geocoding API | Open-Meteo Forecast API</p>
                    <p>::: /SIDEBAR :::</p>
                    <p>After the sidebar.</p>
                  </article>
                </main>
              </body>
            </html>
            """;
        IDocument document = await ParseHtmlAsync(html);

        Modify(document);

        IElement sidebar = AssertSidebar(document);
        IElement? description = sidebar.QuerySelector("dd");
        description.Should().NotBeNull();

        IElement? list = description!.QuerySelector("ul");
        list.Should().NotBeNull();
        list!.QuerySelectorAll("li").Select(element => element.TextContent).Should().Equal(
            "MediaWiki API",
            "Open-Meteo Geocoding API",
            "Open-Meteo Forecast API");
    }

    [Fact]
    public async Task Modify_should_preserve_inline_HTML_in_pipe_separated_list_items()
    {
        string html = """
            <!DOCTYPE html>
            <html lang="en">
              <body>
                <main>
                  <article>
                    <p>Before the sidebar.</p>
                    <p>::: SIDEBAR :::</p>
                    <p>Integrations: MediaWiki API | <a href="https://open-meteo.com/">Open-Meteo Geocoding API</a>, <a href="https://creativecommons.org/licenses/by/4.0/">CC BY 4.0</a> | <a href="https://open-meteo.com/">Open-Meteo Forecast API</a>, <a href="https://creativecommons.org/licenses/by/4.0/">CC BY 4.0</a></p>
                    <p>::: /SIDEBAR :::</p>
                    <p>After the sidebar.</p>
                  </article>
                </main>
              </body>
            </html>
            """;
        IDocument document = await ParseHtmlAsync(html);

        Modify(document);

        IElement sidebar = AssertSidebar(document);
        IHtmlCollection<IElement> listItems = sidebar.QuerySelectorAll("dd li");
        listItems.Should().HaveCount(3);
        listItems[0].TextContent.Should().Be("MediaWiki API");

        listItems[1].QuerySelectorAll("a").Should().HaveCount(2);
        listItems[1].QuerySelectorAll("a")[0].GetAttribute("href").Should().Be("https://open-meteo.com/");
        listItems[1].QuerySelectorAll("a")[0].TextContent.Should().Be("Open-Meteo Geocoding API");
        listItems[1].QuerySelectorAll("a")[1].GetAttribute("href").Should()
            .Be("https://creativecommons.org/licenses/by/4.0/");
        listItems[1].QuerySelectorAll("a")[1].TextContent.Should().Be("CC BY 4.0");

        listItems[2].QuerySelectorAll("a").Should().HaveCount(2);
        listItems[2].QuerySelectorAll("a")[0].GetAttribute("href").Should().Be("https://open-meteo.com/");
        listItems[2].QuerySelectorAll("a")[0].TextContent.Should().Be("Open-Meteo Forecast API");
        listItems[2].QuerySelectorAll("a")[1].GetAttribute("href").Should()
            .Be("https://creativecommons.org/licenses/by/4.0/");
        listItems[2].QuerySelectorAll("a")[1].TextContent.Should().Be("CC BY 4.0");
    }

    private static void Modify(IDocument document)
    {
        IExecutionContext executionContext = MockExecutionContextWithPathAndQuery("/travel-landmark-agent");
        IWebsite website = MockWebsite("en");

        SidebarModifier modifier = new();
        modifier.Initialize(executionContext, website);
        modifier.Modify(document);
    }

    private static IElement AssertSidebar(IDocument document)
    {
        document.QuerySelectorAll("p")
            .Should()
            .NotContain(paragraph => paragraph.TextContent.Contains("::: SIDEBAR", StringComparison.OrdinalIgnoreCase));

        document.QuerySelectorAll("p")
            .Should()
            .NotContain(paragraph => paragraph.TextContent.Contains("::: /SIDEBAR", StringComparison.OrdinalIgnoreCase));

        IElement? sidebar = document.QuerySelector("article > div.sidebar");
        sidebar.Should().NotBeNull();
        return sidebar!;
    }

    private static void AssertSidebarPosition(IDocument document)
    {
        IElement? article = document.QuerySelector("article");
        article.Should().NotBeNull();

        List<IElement> articleChildren = article!.Children.ToList();
        IElement? beforeParagraph = articleChildren.FirstOrDefault(element =>
            element.TagName == "P" && element.TextContent.Trim() == "Before the sidebar.");
        IElement? afterParagraph = articleChildren.FirstOrDefault(element =>
            element.TagName == "P" && element.TextContent.Trim() == "After the sidebar.");
        IElement? sidebar = articleChildren.FirstOrDefault(element =>
            element.TagName == "DIV" && element.ClassList.Contains("sidebar"));

        beforeParagraph.Should().NotBeNull();
        afterParagraph.Should().NotBeNull();
        sidebar.Should().NotBeNull();

        int beforeIndex = articleChildren.IndexOf(beforeParagraph!);
        int sidebarIndex = articleChildren.IndexOf(sidebar!);
        int afterIndex = articleChildren.IndexOf(afterParagraph!);

        sidebarIndex.Should().BeGreaterThan(beforeIndex);
        sidebarIndex.Should().BeLessThan(afterIndex);
    }
}
