using System.Security.Claims;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenizer _tokenizer;
    private readonly IUserService _userService;

    public AuthMiddleware(RequestDelegate next, ITokenizer tokenizer, IUserService userService)
    {
        _next = next;
        _tokenizer = tokenizer;
        _userService = userService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // Validate token and set user principal
        var user = ValidateTokenAndGetUser(token);

        if (user == null)
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        // Set user principal
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            // Add other claims as needed
        };
        var identity = new ClaimsIdentity(claims, "Bearer");
        context.User = new ClaimsPrincipal(identity);

        await _next(context);
    }

    private User ValidateTokenAndGetUser(string token)
    {
        // Basic implementation for token validation
         try
    {
        var user = _tokenizer.ValidateJwtToken(token); // Assuming ValidateJwtToken returns the User object
        return user;
    }
    catch (Exception)
    {
        return null;
    }
    }
}
