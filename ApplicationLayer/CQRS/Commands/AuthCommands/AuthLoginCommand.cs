using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.Auth.Login;
using DomainLayer.Entity;
using MediatR;

namespace ApplicationLayer.CQRS.Commands.Auth
{
    public class AuthLoginCommand : IRequest<LoginResponseDto>
    {
        public LoginRequestDto AuthLoginRequest { get; set; }
    }
}
