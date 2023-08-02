using idee5.Common;

namespace idee5.AspNetCore.TestApi {
    public class DoNothingCommand {
    }
    public class DoNothingCommandHandler : ICommandHandlerAsync<DoNothingCommand> {
        public Task HandleAsync(DoNothingCommand command, CancellationToken cancellationToken = default) {
            return Task.CompletedTask;
        }
    }
    public class DoNothingCommandDecoratorHandler : ICommandHandlerAsync<DoNothingCommand> {
        private readonly ICommandHandlerAsync<DoNothingCommand> decoratee;

        public DoNothingCommandDecoratorHandler(ICommandHandlerAsync<DoNothingCommand> decoratee) {
            this.decoratee = decoratee;
        }

        public Task HandleAsync(DoNothingCommand command, CancellationToken cancellationToken = default) {
            return decoratee.HandleAsync(command, cancellationToken);
        }
    }
}
