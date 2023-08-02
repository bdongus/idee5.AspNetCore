using idee5.AspNetCore.Controllers;
using idee5.Common;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace idee5.AspNetCore;
/// <summary>
/// The controller feature exensions.
/// </summary>
public static class ControllerFeatureExensions {
    private const string _queryHandlerString = "QueryHandler";
    private const string _commandHandlerString = "CommandHandler";
    private const string _controllerSuffix = "Controller";

    /// <summary>
    /// Add query controllers. Replaces "Handler" with "Controller" as controller naming convention.
    /// </summary>
    /// <param name="feature">The feature.</param>
    /// <param name="assembly">The assembly containing the query handlers.</param>
    public static void AddAsyncQueryControllers(this ControllerFeature feature, Assembly assembly) {
        Type interfaceType = typeof(IQueryHandlerAsync<,>);
        // get all query handlers from given assembly
        IEnumerable<Type> types = assembly.GetImplementationsWithoutDecorators(interfaceType);
        if (types.Any()) {
            foreach (Type item in types) {
                string controllerName = item.Name.Replace(_queryHandlerString, _controllerSuffix, StringComparison.Ordinal);
                if (!feature.Controllers.Any(c => c.Name == controllerName)) {
                    // There's no 'real' controller for this query, so add the generic version.
                    Type? queryInterfaceType = item.GetInterface(typeof(IQueryHandlerAsync<,>).Name);
                    if (queryInterfaceType != null) {
                        TypeInfo controllerType = typeof(GenericQueryController<,,>).MakeGenericType(queryInterfaceType,
                            queryInterfaceType.GenericTypeArguments[0], queryInterfaceType.GenericTypeArguments[1]).GetTypeInfo();
                        feature.Controllers.Add(controllerType);
                        Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.ControllerRegistered, controllerType.GenericTypeArguments[1].Name.Replace("Query", "", StringComparison.Ordinal)));
                    }
                    else {
                        Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.TypeForNotFound, typeof(IQueryHandlerAsync<,>).Name));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Add command controllers. Replaces "Handler" with "Controller" as controller naming convention.
    /// </summary>
    /// <param name="feature">The feature.</param>
    /// <param name="assembly">The assembly containing the command handlers.</param>
    public static void AddAsyncCommandControllers(this ControllerFeature feature, Assembly assembly) {
        // get all command handlers from the given assembly
        Type interfaceType = typeof(ICommandHandlerAsync<>);
        IEnumerable<Type> types = assembly.GetImplementationsWithoutDecorators(interfaceType);
        if (types.Any()) {
            foreach (Type item in types) {
                string controllerName = item.Name.Replace(_commandHandlerString, _controllerSuffix, StringComparison.Ordinal);
                if (!feature.Controllers.Any(c => c.Name == controllerName)) {
                    // There's no 'real' controller for this command, so add the generic version.
                    Type? commandInterfaceType = item.GetInterface(interfaceType.Name);
                    if (commandInterfaceType != null) {
                        TypeInfo controllerType = typeof(GenericCommandController<,>).MakeGenericType(commandInterfaceType,
                            commandInterfaceType.GenericTypeArguments[0]).GetTypeInfo();
                        feature.Controllers.Add(controllerType);
                        Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.ControllerRegistered, controllerName));
                    }
                    else {
                        Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.TypeForNotFound, interfaceType.Name));
                    }
                }
            }
        }
    }
}
