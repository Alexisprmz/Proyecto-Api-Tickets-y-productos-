using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService {
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config) { _config = config; }

    public string GenerateToken(string username, string role) {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Role, role)
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
