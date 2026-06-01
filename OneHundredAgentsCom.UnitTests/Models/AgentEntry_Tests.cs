// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.UnitTests.Models;

public class AgentEntry_Tests
{
    [Fact]
    public void Slug_should_return_the_set_value_when_the_set_value_is_not_empty()
    {
        AgentEntry entry = new()
        {
            Name = "Customer Complaint Agent",
            Slug = "customer-complaint-classifier",
        };

        entry.Slug.Should().Be("customer-complaint-classifier");
    }

    [Fact]
    public void Slug_should_return_a_derived_value_when_the_set_value_is_empty()
    {
        AgentEntry entry = new()
        {
            Name = "Customer Complaint Agent",
            Slug = "",
        };

        entry.Slug.Should().Be("customer-complaint-agent");
    }

    [Fact]
    public void Slug_should_return_a_derived_value_when_the_set_value_was_not_set()
    {
        AgentEntry entry = new()
        {
            Name = "Customer Complaint Agent",
        };

        entry.Slug.Should().Be("customer-complaint-agent");
    }
}
