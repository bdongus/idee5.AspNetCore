using idee5.Common;
using Microsoft.AspNetCore.Http;

namespace idee5.AspNetCore;
/// <summary>
/// The http context current user provider.
/// </summary>
public class HttpContextCurrentUserProvider : ICurrentUserIdProvider {
    private readonly IHttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpContextCurrentUserProvider"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The http context accessor.</param>
    public HttpContextCurrentUserProvider(IHttpContextAccessor httpContextAccessor) {
        this.httpContextAccessor = httpContextAccessor;
    }
    /// <inheritdoc/>
    public string GetCurrentUserId() {
        return httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "anonymous";
    }
}
