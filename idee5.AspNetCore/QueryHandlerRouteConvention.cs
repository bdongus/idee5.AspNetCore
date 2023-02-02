using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace idee5.AspNetCore;

/// <summary>
/// Replace the generic controller names with meaningful names.
/// </summary>
public class QueryHandlerRouteConvention : IControllerModelConvention {
    private const string suffix = "Query";

    /// <inheritdoc/>
    public void Apply(ControllerModel controller) {
        if (controller == null) throw new ArgumentNullException(nameof(controller));

        // Only handle generic queries from the web api namespace
        if (controller.ControllerType.IsGenericType && controller.ControllerType.Namespace == "idee5.AspNetCore") {
            string parameterName = controller.ControllerType.GenericTypeArguments[1].Name;
            if (parameterName.EndsWith(suffix, StringComparison.Ordinal)) {
                // The second type parameter is a query parameter, set the controller name
                controller.ControllerName = parameterName.Replace(suffix, "", StringComparison.Ordinal);
            }
        }
    }
}
