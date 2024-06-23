using ApplicationLayer.DTOs.Auth.Login;
using EVO.DomainLayer.Entity.Models.Auth;

namespace ApplicationLayer.Contracts.Auth
{
    public interface IUserRepository
    {
        bool IsUserExist(LoginRequestDto loginRequest);
        Task<User?> GetUserByLoginRequestAsync(LoginRequestDto loginRequestDto);
    }
}
