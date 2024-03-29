﻿using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace idee5.AspNetCore;

/// <summary>
/// Add the generic controller types to the available controllers.
/// </summary>
public class CommandControllerProvider : IApplicationFeatureProvider<ControllerFeature> {
    private readonly Assembly _assembly;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandControllerProvider"/> class.
    /// </summary>
    /// <param name="assembly">The assembly containing the commands.</param>
    public CommandControllerProvider(Assembly assembly) {
        this._assembly = assembly;
    }
    /// <inheritdoc/>
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
        ArgumentNullException.ThrowIfNull(feature);
        feature.AddAsyncCommandControllers(_assembly);
    }
}
