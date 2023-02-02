using idee5.Common;

namespace idee5.AspNetCore.TestApi {
    public class DoNothingCommand {
    }
    public class DoNothingCommandHandler : ICommandHandlerAsync<DoNothingCommand> {
        public Task HandleAsync(DoNothingCommand command, CancellationToken cancellationToken = default) {
            return Task.CompletedTask;
        }
    }
}
