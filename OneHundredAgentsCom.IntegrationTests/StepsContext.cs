// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Reqnroll;

namespace Genova.OneHundredAgentsCom.IntegrationTests;

[Binding]
public class StepsContext : Testing.IntegrationTests.StepsContext<Host.Program>
{
    private const string AppSettings = """
            {
                "Logging": {
                    "LogLevel": {
                        "Default": "Information",
                        "Microsoft.AspNetCore": "Warning",
                        "Microsoft.AspNetCore.Mvc.Razor": "Debug",
                        "Microsoft.ReverseProxy.Matching": "Debug",
                        "Microsoft.Hosting.Lifetime": "Information"
                    }
                },
                "AllowedHosts": "*",
                "Websites": [
                    {
                        "Name": "onehundredagents-com",
                        "WebsiteId": "e5b3c7a1-2f4d-4a6b-8c9d-1e2f3a4b5c6d",
                        "TenantId": "3f9a7e2b-6c1d-4b5a-9f2e-8d7c6b5a4e3f",
                        "Hosts": [
                            "www.100agentsin100days.com"
                        ],
                        "Settings": {
                            "Setting1": "Value3.1",
                            "Setting2": "Value3.2"
                        },
                        "Localization": {
                            "DefaultCulture": "en"
                        }
                    }
                ]
            }        
            """;

    public StepsContext() : base(AppSettings)
    {
    }
}
