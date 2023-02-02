using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace idee5.AspNetCore;

/// <summary>
/// Add the generic controller types to the available controllers.
/// </summary>
public class QueryControllerProvider : IApplicationFeatureProvider<ControllerFeature> {
    private readonly Assembly assembly;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryControllerProvider"/> class.
    /// </summary>
    /// <param name="assembly">The assembly containing the queries.</param>
    public QueryControllerProvider(Assembly assembly) {
        this.assembly = assembly;
    }

    /// <inheritdoc/>
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
        if (feature == null) throw new ArgumentNullException(nameof(feature));
        feature.AddAsyncQueryControllers(assembly);
    }
}
