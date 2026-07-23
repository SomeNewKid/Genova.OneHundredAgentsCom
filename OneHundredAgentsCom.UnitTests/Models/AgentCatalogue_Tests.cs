// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.UnitTests.Models;

public class AgentCatalogue_Tests
{
    [Fact]
    public void AgentCatalogue_should_have_a_group_for_the_OpenAI_Agents_SDK()
    {
        AgentCatalogue catalogue = new();

        catalogue.Groups.Should().NotBeEmpty();
        catalogue.Groups.Should().ContainSingle(group => group.Title == "OpenAI Agents SDK");
    }

    [Fact]
    public void AgentCatalogue_should_have_the_Travel_Landmark_Agent_in_the_IBM_BeeAI_Framework_group()
    {
        AgentCatalogue catalogue = new();

        AgentGroup group = catalogue.Groups.Should()
            .ContainSingle(group => group.Title == "IBM BeeAI framework")
            .Subject;

        group.Agents.Should().Contain(agent => agent.Name == "Travel landmark agent");
    }
}
