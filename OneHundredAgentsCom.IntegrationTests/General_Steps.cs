// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Reqnroll;

namespace Genova.OneHundredAgentsCom.IntegrationTests;

[Binding]
public class General_Steps()
    : Testing.IntegrationTests.General_Steps(new StepsContext())
{
}
