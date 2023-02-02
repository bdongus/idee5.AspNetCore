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

    /// <summary>
    /// Add query controllers.
    /// </summary>
    /// <param name="feature">The feature.</param>
    /// <param name="assembly">The assembly containing the query handlers.</param>
    public static void AddAsyncQueryControllers(this ControllerFeature feature, Assembly assembly) {
        string interfaceName = typeof(IQueryHandlerAsync<,>).Name;
        // get all query handlers from idee5.Globalization
        IEnumerable<Type> types = assembly.GetExportedTypes().Where(t => !t.IsAbstract
                    && t.IsClass
                    && t.Name.EndsWith(_queryHandlerString, StringComparison.Ordinal)
                    && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == interfaceName));
        if (types.Any()) {
            foreach (Type item in types) {
                string controllerName = item.Name.Replace(_queryHandlerString, "Controller", StringComparison.Ordinal);
                if (!feature.Controllers.Any(c => c.Name == controllerName)) {
                    // There's no 'real' controller for this query, so add the generic version.
                    Type? interfaceType = item.GetInterface(typeof(IQueryHandlerAsync<,>).Name);
                    if (interfaceType != null) {
                        TypeInfo controllerType = typeof(GenericQueryController<,,>).MakeGenericType(interfaceType,
                            interfaceType.GenericTypeArguments[0], interfaceType.GenericTypeArguments[1]).GetTypeInfo();
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
    /// Add command controllers.
    /// </summary>
    /// <param name="feature">The feature.</param>
    /// <param name="assembly">The assembly containing the command handlers.</param>
    public static void AddAsyncCommandControllers(this ControllerFeature feature, Assembly assembly) {
        string interfaceName = typeof(ICommandHandlerAsync<>).Name;
        // get all command handlers from idee5.Globalization
        IEnumerable<Type> types = assembly.GetTypes()
            .Where(t => !t.IsAbstract
                    && t.IsClass
                    && t.Name.EndsWith(_commandHandlerString, StringComparison.Ordinal)
                    && !t.Name.Contains("Validat", StringComparison.Ordinal)
                    && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Name == interfaceName));
        if (types.Any()) {
            foreach (Type item in types) {
                string controllerName = item.Name.Replace(_commandHandlerString, "Controller", StringComparison.Ordinal);
                if (!feature.Controllers.Any(c => c.Name == controllerName)) {
                    // There's no 'real' controller for this command, so add the generic version.
                    Type? interfaceType = item.GetInterface(interfaceName);
                    if (interfaceType != null) {
                        TypeInfo controllerType = typeof(GenericCommandController<,>).MakeGenericType(interfaceType,
                            interfaceType.GenericTypeArguments[0]).GetTypeInfo();
                        feature.Controllers.Add(controllerType);
                        Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.ControllerRegistered, controllerName));
                    }
                    else {
                        Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Properties.Resources.TypeForNotFound, interfaceName));
                    }
                }
            }
        }
    }
}
