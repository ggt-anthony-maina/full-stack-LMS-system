
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
public interface ITokenizer
{
    string GenerateJwtToken(User user);
      User ValidateJwtToken(string token);
}

public class Tokenizer : ITokenizer
{
    private readonly byte[] _key;
    private readonly IUserService _userService;

    public Tokenizer(IUserService userService)
    {
        _userService = userService;
        _key = Generate256BitKey();
    }

    public string GenerateJwtToken(User user)
    {
        if (user.Id == null)
        {
            throw new ArgumentNullException(nameof(user.Id), "User Id cannot be null.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
               
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public User ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(_key);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
            var username = jwtToken.Claims.First(x => x.Type == "username").Value;

            var user = _userService.GetUserByIdAsync(userId).Result;
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid token.", ex);
        }
    }

    private byte[] Generate256BitKey()
    {
        using (var generator = RandomNumberGenerator.Create())
        {
            var key = new byte[32]; // 256 bits
            generator.GetBytes(key);
            return key;
        }
    }
}
