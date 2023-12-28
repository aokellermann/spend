using System.Security.Claims;

namespace Spend.Graph.Infrastructure;

/// <summary>
///     Http user context.
/// </summary>
public class UserContext
{
    /// <summary>
    ///     Ctor.
    /// </summary>
    /// <param name="httpContext">Http context accessor.</param>
    public UserContext(IHttpContextAccessor httpContext)
    {
        HttpContext = httpContext.HttpContext ?? throw new ArgumentNullException(nameof(httpContext));

        var userIdString = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        UserId = userIdString != null && Guid.TryParse(userIdString, out var userId) ? userId : null;
    }

    /// <summary>
    ///     The http context.
    /// </summary>
    public HttpContext HttpContext { get; }

    /// <summary>
    ///     The authorized user identifier or null if anonymous.
    /// </summary>
    public Guid? UserId { get; }
}