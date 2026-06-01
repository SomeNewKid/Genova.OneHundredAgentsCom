// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using FluentAssertions;

namespace Genova.OneHundredAgentsCom.UnitTests;

public class Required_Test
{
    [Fact]
    public void Test1()
    {
        string message = "CI/CD pipeline requires at least one unit test.";
        message.Should().NotBeNullOrEmpty();
    }
}
