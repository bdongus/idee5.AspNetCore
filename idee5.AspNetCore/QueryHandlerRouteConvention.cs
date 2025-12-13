using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace idee5.AspNetCore;

/// <summary>
/// Replace the generic query controller names with meaningful names.
/// </summary>
public class QueryHandlerRouteConvention : IControllerModelConvention {
    private const string _suffix = "Query";

    /// <inheritdoc/>
    public void Apply(ControllerModel controller) {
        ArgumentNullException.ThrowIfNull(controller);

        // Only handle generic queries from the web api namespace
        if (controller.ControllerType.IsGenericType) {
            string parameterName = controller.ControllerType.GenericTypeArguments[1].Name;
            if (parameterName.EndsWith(_suffix, StringComparison.Ordinal)) {
                // The second type parameter is a query parameter, set the controller name
                controller.ControllerName = parameterName.Replace(_suffix, "", StringComparison.Ordinal);
            }
        }
    }
}
