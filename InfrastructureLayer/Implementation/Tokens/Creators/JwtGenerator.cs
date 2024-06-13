using DomainLayer.JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entity;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer.Implementation.Tokens.Creators
{
    internal class JwtGenerator
    {
        #region Fields

        private IOptions<JwtSettings> jwtSetting;

        #endregion
        public JwtGenerator(IOptions<JwtSettings> jwtSetting)
        {
            this.jwtSetting = jwtSetting;
        }

        public string GenerateToken(User user)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] securityKey = Encoding.UTF8.GetBytes(jwtSetting.Value.SecurityKey);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        //new Claim("IdUser", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleUser.ToString()),
                    }),
                Expires = DateTime.Now.AddSeconds(500),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.UtcNow,
                Issuer = "Issuer",
                Audience = "Audience"
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(token);

            return accessToken;
        }

    }
}
