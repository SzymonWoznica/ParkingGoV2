using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using AutoMapper;
using EVO.InfrastructureLayer.Data.Auth;
using InfrastructureLayer.Implementation.Tokens.Creators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer.Handlers.AuthHandlers
{
    public class AuthLoginHandler : IRequestHandler<AuthLoginCommand, LoginResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly AuthDbContext _appDbContext;
        private readonly IConfiguration _config;

        public AuthLoginHandler(IMapper mapper, AuthDbContext appDbContext, IConfiguration config)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _config = config;
        }
        public async Task<LoginResponseDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {

            var isExist = _appDbContext.Users.FirstOrDefaultAsync(x =>
                x.EmailAddress == request.AuthLoginRequest.EmailAddress
                && x.Password == request.AuthLoginRequest.Password);

            // Wrong login or password
            if (isExist.Result == null)
                return setLoginResponse(null);

            TokenCreator tokenCreator = new TokenCreator(_appDbContext, _config);
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
