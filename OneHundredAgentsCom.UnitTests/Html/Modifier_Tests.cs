// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using AngleSharp;
using AngleSharp.Dom;
using Genova.Common.Execution;
using Genova.Common.Websites;
using Moq;

namespace Genova.OneHundredAgentsCom.UnitTests.Html;

public abstract class Modifier_Tests
{
    protected static async Task<IDocument> ParseHtmlAsync(string html)
    {
        return await ParseHtmlAsync(html, "http://www.example.com");
    }

    protected static async Task<IDocument> ParseHtmlAsync(string html, string baseUri)
    {
        IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        return await context.OpenAsync(request => request.Content(html).Address(baseUri));
    }

    protected static IExecutionContext MockExecutionContext()
    {
        return MockExecutionContext("en");
    }

    protected static IExecutionContext MockExecutionContext(string culturePrefix)
    {
        Mock<IExecutionContext> executionContextMock = new();
        Mock<ICultureContext> cultureContextMock = new();
        cultureContextMock.Setup(context => context.Request).Returns(culturePrefix);
        executionContextMock.Setup(context => context.CultureContext).Returns(cultureContextMock.Object);
        return executionContextMock.Object;
    }

    protected static IExecutionContext MockExecutionContextWithPathAndQuery(string pathAndQuery)
    {
        Mock<IExecutionContext> executionContextMock = new();
        Mock<IRequestContext> requestContextMock = new();
        requestContextMock.Setup(context => context.PathAndQuery).Returns(pathAndQuery);
        executionContextMock.Setup(context => context.RequestContext).Returns(requestContextMock.Object);
        return executionContextMock.Object;
    }

    protected static IWebsite MockWebsite(string defaultCulture)
    {
        return MockWebsite(defaultCulture, string.Empty);
    }

    protected static IWebsite MockWebsite(string defaultCulture, string name)
    {
        Mock<IWebsite> websiteMock = new();
        websiteMock.Setup(website => website.DefaultCulture).Returns(defaultCulture);
        websiteMock.Setup(website => website.Name).Returns(name);
        return websiteMock.Object;
    }
}
