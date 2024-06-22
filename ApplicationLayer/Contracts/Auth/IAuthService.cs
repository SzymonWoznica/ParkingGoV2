using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.Auth.Login;
using EVO.DomainLayer.Entity.Models.Auth;

namespace ApplicationLayer.Contracts.Auth
{
    public interface IUserRepository
    {
        bool IsUserExist(LoginRequestDto loginRequest);
        Task<User?> GetUserByIdAsync(LoginRequestDto loginRequestDto);
    }
}
