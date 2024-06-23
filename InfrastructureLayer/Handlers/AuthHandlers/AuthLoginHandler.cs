using ApplicationLayer.Contracts.Auth;
using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using AutoMapper;
using EVO.DomainLayer.Entity.Models.Auth;
using EVO.InfrastructureLayer.Data.Auth;
using FluentValidation;
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
        private readonly IValidator<AuthLoginCommand> _validator;
        private readonly IUserRepository _userRepository;

        public AuthLoginHandler(
            IMapper mapper, 
            AuthDbContext appDbContext, 
            IConfiguration config, 
            IValidator<AuthLoginCommand> validator,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _config = config;
            _validator = validator;
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
        {
            //var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            var result = new LoginResponseDto();

            //if (validationResult.IsValid)
            if(true)
            {
                TokenCreator tokenCreator = new TokenCreator(_appDbContext, _config);
                var userInfo = _userRepository.GetUserByIdAsync(request.AuthLoginRequest);
                tokenCreator.CreateTokens(await userInfo);

                result.AccessToken = tokenCreator.AccessToken;
                result.RefreshToken = tokenCreator.RefreshToken;
                result.IsSuccessful = true;
            }

            else
            {
                //result.Message = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            return result;
        }



    }
}
