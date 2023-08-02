using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;

namespace idee5.AspNetCore;
public static class EndpointRouteBuilderExtensions {
    /// <summary>
    /// Add a simple route dump to the application
    /// </summary>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/> to add the route to</param>
    /// <param name="dumpRoute">Route pattern for the dump</param>
    /// <returns>A <see cref="IEndpointConventionBuilder"/> that can be used to further customize the endpoint.</returns>
    public static IEndpointConventionBuilder MapRouteDump(this IEndpointRouteBuilder builder, string dumpRoute) {
        return builder.MapGet(dumpRoute, (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints.OfType<RouteEndpoint>().Select(ep =>
            $"{ep.RoutePattern.RawText}: {ep.Metadata.GetMetadata<ControllerActionDescriptor>()?.DisplayName ?? ep.DisplayName}"))));
    }
}
