// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;
using Genova.OneHundredAgentsCom.Html;
using Genova.OneHundredAgentsCom.Models;

namespace Genova.OneHundredAgentsCom.UnitTests.Html;

public class SandboxReportModifier_Tests
{
    [Fact]
    public void All_allowed_results_in_capability_allowed()
    {
        CapabilityResult capability = BuildCapability("Allowed", "Allowed", "Allowed");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-allowed");
    }

    [Fact]
    public void Tool_allowed_shell_denied_alternates_allowed_results_in_capability_allowed()
    {
        CapabilityResult capability = BuildCapability("Allowed", "Denied", "Allowed");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-allowed");
    }

    [Fact]
    public void Tool_allowed_shell_denied_alternates_na_results_in_capability_allowed()
    {
        CapabilityResult capability = BuildCapability("Allowed", "Denied", "N/A");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-allowed");
    }

    [Fact]
    public void Mixed_not_applicable_and_denied_with_at_least_one_denied_results_in_capability_denied()
    {
        // Tool: Not applicable, Shell: Denied, Alternates: Not applicable
        CapabilityResult capability = BuildCapability("N/A", "Denied", "N/A");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-denied");
    }

    [Fact]
    public void Mixed_not_applicable_and_denied_with_tool_capability_denied()
    {
        // Tool: Denied, Shell: Not applicable, Alternates: Not applicable
        CapabilityResult capability = BuildCapability("Denied", "N/A", "N/A");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-denied");
    }

    [Fact]
    public void Mixed_not_applicable_and_denied_with_shell_capability_denied()
    {
        // Tool: Not applicable, Shell: Denied, Alternates: Not applicable
        CapabilityResult capability = BuildCapability("N/A", "Denied", "N/A");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-denied");
    }

    [Fact]
    public void Mixed_not_applicable_and_denied_with_alternates_capability_denied()
    {
        // Tool: Not applicable, Shell: Not applicable, Alternates: Denied
        CapabilityResult capability = BuildCapability("N/A", "N/A", "Denied");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-denied");
    }

    [Fact]
    public void All_not_applicable_results_in_capability_na()
    {
        CapabilityResult capability = BuildCapability("N/A", "N/A", "N/A");

        string result = SandboxReportModifier.GetCapabilityRowClass(capability);

        result.Should().Be("capability-na");
    }

    private static CapabilityResult BuildCapability(string toolOutcome, string shellOutcome, string alternatesOutcome)
    {
        return new CapabilityResult
        {
            Id = "T01",
            Title = "Test capability",
            Tool = new InvocationResult
            {
                Outcome = toolOutcome,
                Summary = string.Empty,
                Evidence = string.Empty
            },
            Shell = new InvocationResult
            {
                Outcome = shellOutcome,
                Summary = string.Empty,
                Evidence = string.Empty
            },
            Alternates = new AlternateInvocationResult
            {
                Outcome = alternatesOutcome,
                Summary = string.Empty,
                Attempts = []
            }
        };
    }
}
