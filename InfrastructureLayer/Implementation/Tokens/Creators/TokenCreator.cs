using DomainLayer.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entity;
using InfrastructureLayer.Data;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer.Implementation.Tokens.Creators
{
    public class TokenCreator
    {
        #region Fields

        private AppDbContext _dbContext;
        private IOptions<JwtSettings> _jwtSetting;

        private string accessToken;
        private string refreshToken;

        public string AccessToken
        {
            get => accessToken; 
            private set => accessToken = value;
        }

        public string RefreshToken
        {
            get => refreshToken;
            private set => refreshToken = value;
        }

        #endregion

        public TokenCreator(AppDbContext dbContext, IOptions<JwtSettings> jwtSetting)
        {
            this._dbContext = dbContext;
            this._jwtSetting = jwtSetting;
        }

        /// <summary>
        /// Create and save in database Refresh Token and generate Access Token
        /// </summary>
        /// <returns></returns>
        public void CreateTokens(User userInfo)
        {

            JwtGenerator jwtCreator = new JwtGenerator(_jwtSetting);
            RefreshTokenCreator refreshTokenCreator = new RefreshTokenCreator(this._dbContext);

            accessToken = jwtCreator.GenerateToken(userInfo);
            refreshToken = refreshTokenCreator.CreateToken(new Guid(userInfo.Id));
        }
    }
}
