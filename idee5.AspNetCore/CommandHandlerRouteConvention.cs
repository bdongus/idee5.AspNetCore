using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace idee5.AspNetCore;

/// <summary>
/// Replace the generic controller names with meaningful names.
/// </summary>
public class CommandHandlerRouteConvention : IControllerModelConvention {
    private const string suffix = "Command";

    /// <inheritdoc/>
    public void Apply(ControllerModel controller) {
        ArgumentNullException.ThrowIfNull(controller);

        // Only handle generic commands from the web api namespace
        if (controller.ControllerType.IsGenericType) {
            string parameterName = controller.ControllerType.GenericTypeArguments[1].Name;
            if (parameterName.EndsWith(suffix, StringComparison.Ordinal)) {
                // The second type parameter is a command parameter, set the controller name
                controller.ControllerName = parameterName.Replace(suffix, "", StringComparison.Ordinal);
            }
        }
    }
}
