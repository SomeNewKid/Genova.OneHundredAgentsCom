// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Genova.Common.Attributes;
using Genova.Common.Websites;
using Genova.Engine;
using Genova.PlainOldWebCom;

namespace Genova.OneHundredAgentsCom.Host;

/// <summary>
/// Entry point for the Genova Host application.
/// </summary>
[CodeQuality(Public = true, Justification = "Intended for instantiation by a Host.")]
public sealed class Program
{
    /// <summary>
    /// Prevents a default instance of the <see cref="Program"/> class from being created.
    /// </summary>
    // <notes>
    // 1. SonarQube: csharpsquid:S1118A
    //    Utility classes should not have public constructors
    //    (https://rules.sonarsource.com/csharp/RSPEC-1118)
    //    While this warning could also be resolved by making the `Program` class `static`,
    //    it is not recommended to do so because it makes integration testing very difficult.
    // </notes>
    [ExcludeFromCodeCoverage]
    private Program()
    {
    }

    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    public static string Name => "Program";

    /// <summary>
    /// The main method, which is the entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    // <notes>
    // 1. Excluded from code coverage because `application.BuildAndRun()` is reported as having
    //    not been covered by any tests. (Coverlet never sees this code as having completed.)
    // </notes>
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Common.Hosts.IHost hostess = new Host();

        IWebsite[] websites =
        [
            new Website(configuration),
        ];

        Application application = new(hostess, websites);
        application.BuildAndRun(args);
    }
}
