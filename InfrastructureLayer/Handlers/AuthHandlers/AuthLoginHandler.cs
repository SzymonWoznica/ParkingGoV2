using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using AutoMapper;
using DomainLayer.Entity;
using DomainLayer.JWT;
using InfrastructureLayer.Data;
using InfrastructureLayer.Implementation.Tokens.Creators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InfrastructureLayer.Handlers.AuthHandlers
{
    public class AuthLoginHandler : IRequestHandler<AuthLoginCommand, LoginResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthLoginHandler(IMapper mapper, AppDbContext appDbContext, IOptions<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _jwtSettings = jwtSettings;
        }
        public async Task<LoginResponseDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {

            var isExist = _appDbContext.Users.FirstOrDefaultAsync(x =>
                x.EmailAddress == request.AuthLoginRequest.EmailAddress
                && x.Password == request.AuthLoginRequest.Password);

            // Wrong login or password
            if (isExist.Result == null)
                return setLoginResponse(null);

            TokenCreator tokenCreator = new TokenCreator(_appDbContext, _jwtSettings);
            tokenCreator.CreateTokens(isExist.Result);

            // Correct logged
            return setLoginResponse(tokenCreator);
        }

        #region Private methods

        private LoginResponseDto setLoginResponse(TokenCreator? generatedToken)
        {
            return new LoginResponseDto()
            {
                RefreshToken = generatedToken == null ? String.Empty : generatedToken.RefreshToken,
                AccessToken = generatedToken == null ? String.Empty : generatedToken.AccessToken,
                Flag = generatedToken != null,
                Message = generatedToken == null ? "Login or password is incorrect" :"Successfully logged.",
                Role = EnumRoleUser.User
            };
        }

        #endregion

    }
}
