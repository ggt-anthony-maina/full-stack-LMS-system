
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;




[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly PasswordHasher _passwordHasher;
    private readonly ITokenizer _tokenizer;

    public AuthController(IUserService userService, PasswordHasher passwordHasher, ITokenizer tokenizer)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _tokenizer = tokenizer;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO userDto)

    {

        if (userDto.Username == null || userDto.Password == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        var user = await _userService.Authenticate(userDto.Username, userDto.Password);

        if (user == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        // Generate JWT token
        var token = _tokenizer.GenerateJwtToken(user);

        return Ok(new
        {
            Id = user.Id,
            Username = user.Username,
            Token = token
        });
    }


}
