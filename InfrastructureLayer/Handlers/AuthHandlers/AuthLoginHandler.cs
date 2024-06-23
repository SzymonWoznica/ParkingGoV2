using ApplicationLayer.Contracts.Auth;
using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using AutoMapper;
using Azure.Core;
using EVO.InfrastructureLayer.Data.Auth;
using FluentValidation;
using InfrastructureLayer.Implementation.Tokens.Creators;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLayer.Handlers.AuthHandlers
{
    public class AuthLoginHandler : IRequestHandler<AuthLoginCommand, LoginResponseDto>
    {

        #region Fields

        private readonly IMapper _mapper;
        private readonly AuthDbContext _appDbContext;
        private readonly IConfiguration _config;
        private readonly IValidator<AuthLoginCommand> _validator;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Ctor
        public AuthLoginHandler(
            AuthDbContext appDbContext, 
            IConfiguration config, 
            IValidator<AuthLoginCommand> validator,
            IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _config = config;
            _validator = validator;
            _userRepository = userRepository;
        }

        #endregion

        public async Task<LoginResponseDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
                return await CreateTokensAndResponse(request.AuthLoginRequest);

            else
                return new LoginResponseDto() { Message = validationResult.Errors.Select(e => e.ErrorMessage).ToList() };

        }

        private async Task<LoginResponseDto> CreateTokensAndResponse(LoginRequestDto loginRequest)
        {
            var result = new LoginResponseDto();

            TokenCreator tokenCreator = new TokenCreator(_appDbContext, _config);
            var userInfo = await _userRepository.GetUserByLoginRequestAsync(loginRequest);
            tokenCreator.CreateTokens(userInfo);

            result.AccessToken = tokenCreator.AccessToken;
            result.RefreshToken = tokenCreator.RefreshToken;
            result.IsSuccessful = true;

            return result;
        }
    }
}
