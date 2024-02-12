using Microsoft.IdentityModel.Tokens;
using NTierArchitecture.Entities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NTierArchitecture.WebAPI.Service
{
    public static class JwtService
    {
        public static string CreatToken(AppUser user)
        {
            var claims = new Claim[]
            {
           new("UserId",user.Id.ToString()),
           new("UserName",user.UserName)
            };

            JwtSecurityToken handler = new(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My secret key my secret key aşlsdkaskdlşaskşldşklasd")), SecurityAlgorithms.HmacSha256));
            string token = new JwtSecurityTokenHandler().WriteToken(handler);

            return token;
        }
    }
}
