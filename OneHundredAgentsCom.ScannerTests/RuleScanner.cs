// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using System.Text;
using Genova.Crawler;
using Genova.Scanner;
using Genova.Testing.ScannerTests;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Genova.OneHundredAgentsCom.ScannerTests;

[TestClass]
public class RuleScanner : RuleScanner_Base<Host.Program>
{
    readonly Website _website;

    public RuleScanner()
    {
        using MemoryStream stream = new(Encoding.UTF8.GetBytes(AppSettings));
        IConfiguration configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
        _website = new(configuration);
    }

    protected override string AppSettings
    {
        get
        {
            return """
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
                                "Setting1": "Value5.1",
                                "Setting2": "Value5.2"
                            },
                            "Localization": {
                                "DefaultCulture": "en"
                            }
                        }
                    ]
                }        
                """;
        }
    }

    protected override string Host
    {
        get
        {
            return "https://www.100agentsin100days.com";
        }
    }

    protected override CrawlOptions CrawlOptions
    {
        get
        {
            return new CrawlOptions
            {
                PauseBetweenRequests = 100,
                StartingPaths = ["/hello/scanner"],
            };
        }
    }

    protected override ScanOptions ScanOptions
    {
        get
        {
            return new()
            {
                CorsPolicy = _website.GetCorsPolicy(),
                Thoroughness = 0.5m,
                CssNamePatterns = [ "layout-cols=*", "bump-*" ],
            };
        }
    }
}
