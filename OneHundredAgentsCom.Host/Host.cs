// This file is part of the Genova project licensed under the GNU General Public License v3.0.
// See the LICENSE file in the project root for more information.

using Genova.Engine.Execution;
using Genova.Engine.Websites;

namespace Genova.OneHundredAgentsCom.Host;

/// <summary>
/// Represents a module that integrates with the Engine and provides tenant ID resolution services.
/// </summary>
internal sealed class Host : Common.Hosts.IHost
{
    /// <summary>
    /// Configures the services required by the module.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which to configure the services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ITenantIdResolver, HostBasedTenantIdResolver>();
        services.AddScoped<IWebsiteIdResolver, HostBasedWebsiteIdResolver>();
    }
}
