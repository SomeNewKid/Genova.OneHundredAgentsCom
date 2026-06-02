// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.OneHundredAgentsCom.Utilities;

namespace Genova.OneHundredAgentsCom.UnitTests.Utilities;

public class InstructionHelper_Tests
{
    [Fact]
    public void Parses_single_attribute()
    {
        Dictionary<string, string> result = InstructionHelper.ParseAttributes(@"id=""eliza""");
        result.Should().ContainKey("id");
        result["id"].Should().Be("eliza");
        result.Should().HaveCount(1);
    }

    [Fact]
    public void Parses_multiple_attributes()
    {
        Dictionary<string, string> result =
            InstructionHelper.ParseAttributes(@"id=""eliza"" endpoint=""/api/eliza"" name=""Eliza""");
        result.Should().ContainKey("id");
        result["id"].Should().Be("eliza");
        result.Should().ContainKey("endpoint");
        result["endpoint"].Should().Be("/api/eliza");
        result.Should().ContainKey("name");
        result["name"].Should().Be("Eliza");
        result.Should().HaveCount(3);
    }

    [Fact]
    public void Ignores_non_attribute_text()
    {
        Dictionary<string, string> result =
            InstructionHelper.ParseAttributes(@"MINIATURE id=""eliza"" foo bar endpoint=""/api/eliza""");
        result.Should().ContainKey("id");
        result.Should().ContainKey("endpoint");
        result.Should().NotContainKey("foo");
        result.Should().NotContainKey("bar");
    }

    [Fact]
    public void Ignores_unquoted_values()
    {
        Dictionary<string, string> result = InstructionHelper.ParseAttributes(@"id=eliza endpoint=""/api/eliza""");
        result.Should().NotContainKey("id");
        result.Should().ContainKey("endpoint");
        result["endpoint"].Should().Be("/api/eliza");
    }

    [Fact]
    public void Handles_extra_colons_and_whitespace()
    {
        Dictionary<string, string> result =
            InstructionHelper.ParseAttributes(@"::: MINIATURE   id=""eliza""   endpoint=""/api/eliza""   :::");
        result.Should().ContainKey("id");
        result["id"].Should().Be("eliza");
        result.Should().ContainKey("endpoint");
        result["endpoint"].Should().Be("/api/eliza");
    }

    [Fact]
    public void Handles_empty_string()
    {
        Dictionary<string, string> result = InstructionHelper.ParseAttributes(string.Empty);
        result.Should().BeEmpty();
    }

    [Fact]
    public void Handles_no_attributes()
    {
        Dictionary<string, string> result = InstructionHelper.ParseAttributes("MINIATURE :::");
        result.Should().BeEmpty();
    }

    [Fact]
    public void Handles_attribute_with_empty_value()
    {
        Dictionary<string, string> result = InstructionHelper.ParseAttributes(@"id="""" endpoint=""/api/eliza""");
        result.Should().ContainKey("id");
        result["id"].Should().BeEmpty();
        result.Should().ContainKey("endpoint");
        result["endpoint"].Should().Be("/api/eliza");
    }

    [Fact]
    public void Handles_attribute_with_spaces_in_value()
    {
        Dictionary<string, string> result =
            InstructionHelper.ParseAttributes(@"name=""Eliza Chatbot"" endpoint=""/api/eliza""");
        result.Should().ContainKey("name");
        result["name"].Should().Be("Eliza Chatbot");
        result.Should().ContainKey("endpoint");
        result["endpoint"].Should().Be("/api/eliza");
    }

    [Fact]
    public void Handles_attribute_with_colon_in_value()
    {
        Dictionary<string, string> result = InstructionHelper.ParseAttributes(@"id=""eliza:1"" endpoint=""/api/eliza""");
        result.Should().ContainKey("id");
        result["id"].Should().Be("eliza:1");
        result.Should().ContainKey("endpoint");
        result["endpoint"].Should().Be("/api/eliza");
    }
}
