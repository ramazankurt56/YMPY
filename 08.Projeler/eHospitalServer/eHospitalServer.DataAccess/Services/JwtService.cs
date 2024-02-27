using eHospitalServer.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace eHospitalServer.DataAccess.Services;
internal sealed class JwtService
{
    public string CreateToken(User appUser)
    {
        string token = string.Empty;

        List<Claim> claims = new();
        claims.Add(new("UserId", appUser.Id.ToString()));
        claims.Add(new("Email", appUser.Email ?? string.Empty));
        claims.Add(new("UserName", appUser.UserName ?? string.Empty));
        claims.Add(new("UserType", appUser.UserType.ToString()));
        JwtSecurityToken jwtSecurityToken = new(
            
            issuer: "Ramazan Kurt",
            audience: "eHospital App",
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key 1234 ... my secret key my secret key my secret key 1234 ...")), SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();
        token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
