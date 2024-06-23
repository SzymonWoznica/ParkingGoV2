using EVO.DomainLayer.Entity.Models.Auth;
using EVO.InfrastructureLayer.Data.Auth;
using Microsoft.Extensions.Configuration;

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
