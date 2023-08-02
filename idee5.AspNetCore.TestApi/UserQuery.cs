using idee5.Common;

namespace idee5.AspNetCore.TestApi;

public class UserQuery : IQuery<string> {
    public string Name { get; set; } = "";
}
public class UserQueryHandler : IQueryHandlerAsync<UserQuery, string> {
    private readonly ICurrentUserIdProvider currentUserIdProvider;

    public UserQueryHandler(ICurrentUserIdProvider currentUserIdProvider) {
        this.currentUserIdProvider = currentUserIdProvider ?? throw new ArgumentNullException(nameof(currentUserIdProvider));
    }
    public Task<string> HandleAsync(UserQuery query, CancellationToken cancellationToken = default) {
        return Task.FromResult(currentUserIdProvider.GetCurrentUserId());
    }
}