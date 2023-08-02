using idee5.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace idee5.AspNetCore;
/// <summary>
/// The service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions {
    /// <summary>
    /// Registers the query handlers.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="serviceLifetime">The service lifetime.</param>
    public static void RegisterQueryHandlers(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) {
        services.RegisterHandlers(typeof(IQueryHandlerAsync<,>), serviceLifetime);
    }
    /// <summary>
    /// Register the command handlers.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="serviceLifetime">The service lifetime.</param>
    public static void RegisterCommandHandlers(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) {
        services.RegisterHandlers(typeof(ICommandHandlerAsync<>), serviceLifetime);
    }
    /// <summary>
    /// Registers the handlers. Skips validation handlers ("Validat" in their name), as they are .
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="handlerType">The handler type. E.g. typeof(ICommandHandlerAsync<>)
    /// </param>
    /// <param name="serviceLifetime">The service lifetime.</param>
    public static void RegisterHandlers(this IServiceCollection services, Type handlerType, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) {
        IEnumerable<TypeInfo> implementations = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.DefinedTypes.Where(t => !t.IsAbstract && t.IsClass && !t.IsGenericType && t.IsPublic
            && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType)
            && !t.DeclaredConstructors.Any(c => c.GetParameters().Any(p => t.ImplementedInterfaces.Contains(p.ParameterType)))));
        foreach (TypeInfo? item in implementations) {
            var service = new ServiceDescriptor(item.GetInterfaces().Single(i => i.GetGenericTypeDefinition() == handlerType), item, serviceLifetime);
            services.Add(service);
        }
    }
}
