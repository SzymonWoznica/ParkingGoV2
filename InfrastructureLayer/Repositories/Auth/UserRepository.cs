using ApplicationLayer.Contracts.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using EVO.DomainLayer.Entity.Models.Auth;
using EVO.InfrastructureLayer.Data.Auth;
using Microsoft.EntityFrameworkCore;

namespace EVO.InfrastructureLayer.Repositories.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _appDbContext;

        public UserRepository(AuthDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool IsUserExist(LoginRequestDto loginRequest)
        {
            return _appDbContext.Users.Any(u =>
                u.EmailAddress == loginRequest.EmailAddress
                && u.Password == loginRequest.Password);
        }

        public async Task<User?> GetUserByLoginRequestAsync(LoginRequestDto loginRequestDto)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(user =>
                user != null
                && user.EmailAddress == loginRequestDto.EmailAddress
                && user.Password == loginRequestDto.Password);
        }
    }
}
