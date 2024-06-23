using DomainLayer.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using EVO.InfrastructureLayer.Data.Auth;
using EVO.DomainLayer.Entity.Models.Auth;

namespace InfrastructureLayer.Implementation.Tokens.Creators
{
    public class TokenCreator
    {
        #region Fields

        private AuthDbContext _dbContext;
        private readonly IConfiguration _config;

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

        public TokenCreator(AuthDbContext dbContext, IConfiguration config)
        {
            this._dbContext = dbContext;
            _config = config;
        }

        /// <summary>
        /// Create and save in database Refresh Token and generate Access Token
        /// </summary>
        /// <returns></returns>
        public void CreateTokens(User? userInfo)
        {

            JwtGenerator jwtCreator = new JwtGenerator(_config);
            RefreshTokenCreator refreshTokenCreator = new RefreshTokenCreator(this._dbContext);

            accessToken = jwtCreator.GenerateToken(userInfo);
            refreshToken = refreshTokenCreator.CreateToken(new Guid(userInfo.UserId));
        }
    }
}
