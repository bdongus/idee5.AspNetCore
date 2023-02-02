using idee5.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace idee5.AspNetCore;
/// <summary>
/// Web Api wrapper controller around a <see cref="ICommandHandlerAsync{TCommand}"/>.
/// </summary>
/// <typeparam name="TCommandHandler">The corresponding command handler type.</typeparam>
/// <typeparam name="TCommand">The command parameter type.</typeparam>
[ApiController]
[Route("api/command/[controller]")]
[Authorize(Policy = "CommandPolicy")]
public class GenericCommandController<TCommandHandler, TCommand> : ControllerBase
        where TCommandHandler : class, ICommandHandlerAsync<TCommand> {
        private readonly TCommandHandler _commandHandler;

    /// <summary>
    /// Initialize the wrapper controller.
    /// </summary>
    /// <param name="commandHandler">The wrapped <see cref="ICommandHandlerAsync{TCommand}"/>.</param>
    public GenericCommandController(TCommandHandler commandHandler) {
        _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
    }

    /// <summary>
    /// Execute the <see cref="ICommandHandlerAsync{TCommand}"/>.
    /// </summary>
    /// <param name="command">The command parameters.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>An awaitable <see cref="Task"/>.</returns>
    [HttpPost]
    public Task Execute([FromBody] TCommand command, CancellationToken cancellationToken = default) {
        return _commandHandler.HandleAsync(command, cancellationToken);
    }
}
