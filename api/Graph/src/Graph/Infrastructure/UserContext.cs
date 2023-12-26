using System.Security.Claims;

namespace Graph.Infrastructure;

public class UserContext
{
    public UserContext(IHttpContextAccessor httpContext)
    {
        HttpContext = httpContext?.HttpContext ?? throw new ArgumentNullException(nameof(httpContext));

        var userIdString = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        UserId = userIdString != null && Guid.TryParse(userIdString, out var userId) ? userId : null;
    }

    public HttpContext HttpContext { get; set; }

    public Guid? UserId { get; set; }
}