using ApplicationLayer.DTOs.Auth.Login;
using MediatR;

namespace ApplicationLayer.CQRS.Commands.Auth
{
    public class AuthLoginCommand : IRequest<LoginResponseDto>
    {
        public LoginRequestDto AuthLoginRequest { get; set; }
    }
}
