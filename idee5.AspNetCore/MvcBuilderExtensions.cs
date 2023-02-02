using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace idee5.AspNetCore;
/// <summary>
/// The MVC builder extensions.
/// </summary>
public static class MvcBuilderExtensions {
    /// <summary>
    /// Add CQRS handlers.
    /// </summary>
    /// <param name="services">The MVC builder.</param>
    /// <param name="queryAssembly">The assembly containing the queries.</param>
    /// <param name="commandAssembly">The assembly containing the commands.</param>
    /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
    /// <returns>An IMvcBuilder to enable further fluent configuration.</returns>
    public static IMvcBuilder AddCQRSHandlers(this IMvcBuilder services, Assembly queryAssembly, Assembly commandAssembly) {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (queryAssembly is null) throw new ArgumentNullException(nameof(queryAssembly));
        if (commandAssembly is null) throw new ArgumentNullException(nameof(commandAssembly));

        return services
            .AddMvcOptions(opt => {
                opt.Conventions.Add(new QueryHandlerRouteConvention());
                opt.Conventions.Add(new CommandHandlerRouteConvention());
            })
            .ConfigureApplicationPartManager(apm => {
                apm.FeatureProviders.Add(new QueryControllerProvider(queryAssembly));
                apm.FeatureProviders.Add(new CommandControllerProvider(commandAssembly));
            });
    }
}
