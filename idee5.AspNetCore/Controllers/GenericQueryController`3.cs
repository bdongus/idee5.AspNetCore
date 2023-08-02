using idee5.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace idee5.AspNetCore.Controllers;

/// <summary>
/// Generic query controller.
/// </summary>
/// <typeparam name="TQueryHandler">Query handler type.</typeparam>
/// <typeparam name="TQuery">Query parameter type</typeparam>
/// <typeparam name="TResult">Result type.</typeparam>
[ApiController]
[Route("api/query/[controller]")]
[Authorize(Policy = "QueryPolicy")]
public class GenericQueryController<TQueryHandler, TQuery, TResult> : ControllerBase
    where TQueryHandler : class, IQueryHandlerAsync<TQuery, TResult>
    where TQuery : class, IQuery<TResult> {
    private readonly TQueryHandler _queryHandler;

    /// <summary>
    /// Initialize the wrapper controller.
    /// </summary>
    /// <param name="queryHandler">The wrapped <see cref="IQueryHandlerAsync{TQuery, TResult}"/></param>
    public GenericQueryController(TQueryHandler queryHandler) {
        _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
    }

    /// <summary>
    /// Execute the query.
    /// </summary>
    /// <param name="query">Query parameters.</param>
    /// <param name="cancellationtoken">Token to abort the operation</param>
    /// <returns>A <see cref="Task"/> returning the query result.</returns>
    public Task<TResult> Execute([FromQuery] TQuery query, CancellationToken cancellationtoken) {
        return _queryHandler.HandleAsync(query, cancellationtoken);
    }
}
