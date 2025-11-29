using System;
using System.Security.Claims;
using System.Text;
using LoginApp.Models;
using LoginApp.Services.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LoginApp.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration; 

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string CreateToken(User user, IList<string> roles)
    {
        var jwt = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));  

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.Id ?? user.Email ?? string.Empty)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role)); 
        }

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(jwt.GetValue<int>("ExpireMinutes"));  

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            SigningCredentials = credentials,
            Issuer = jwt["Issuer"],
            Audience = jwt["Audience"]
        };

        var tokenHandler = new JsonWebTokenHandler(); 

        return tokenHandler.CreateToken(tokenDescriptor); 

    }

}
