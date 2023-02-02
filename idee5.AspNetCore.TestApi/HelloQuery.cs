using idee5.Common;

namespace idee5.AspNetCore.TestApi; 
public class HelloQuery : IQuery<string> {
    public string Name { get; set; }
}
public class HelloQueryHandler : IQueryHandlerAsync<HelloQuery, string> {
    public Task<string> HandleAsync(HelloQuery query, CancellationToken cancellationToken = default) {
        return Task.FromResult($"Hello {query.Name}");
    }
}
