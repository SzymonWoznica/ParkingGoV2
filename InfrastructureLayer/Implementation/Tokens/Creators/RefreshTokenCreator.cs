using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EVO.DomainLayer.Entity.Models.Auth;
using EVO.InfrastructureLayer.Data.Auth;
using InfrastructureLayer.Helper.OperationResult;

namespace InfrastructureLayer.Implementation.Tokens.Creators
{
    internal class RefreshTokenCreator
    {
        private AuthDbContext _dbContext { get; set; }

        public RefreshTokenCreator(AuthDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
                                                                                                                                                        
        /// <summary>
        /// Generate token and save in database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string CreateToken(Guid userId)
        {
            if (userId == null)
                throw new Exception($"{nameof(userId)} cannot be null");

            string newRefreshToken = this.generateNewToken();


            OperationResult saveInDatabase = new OperationResult();
            saveInDatabase = this.saveInDatabase(newRefreshToken, userId);

            if (saveInDatabase.HasError)
                throw new Exception($"Error Code: B74AA347 => Cannot save new Refresh Token in database.");

            return newRefreshToken;
        }

        #region Private Methods
        private string generateNewToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);

            }
        }

        private OperationResult saveInDatabase(string newRefreshToken, Guid userId)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var tokenExist = _dbContext.RefreshToken.FirstOrDefault(x => x.UserId == userId);

                if (tokenExist == null)
                {
                    RefreshToken_ newRefreshTokenData = new RefreshToken_()
                    {
                        UserId = userId,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = newRefreshToken,
                        IsActive = true,
                        ExpirationTime = DateTime.Now.AddHours(12)
                    };

                    _dbContext.Add(newRefreshTokenData);
                    _dbContext.SaveChanges();
                }

                else
                {
                    tokenExist.ExpirationTime = DateTime.Now.AddHours(12);
                    tokenExist.TokenId = new Random().Next().ToString();
                    tokenExist.RefreshToken = newRefreshToken;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                operationResult.SetError($"Error Code: 35CBEF08 => {e}");
                return operationResult;
            }

            return operationResult;
        }

        #endregion
    }
}
