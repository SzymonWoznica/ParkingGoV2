using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.Contracts.Auth;
using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using EVO.DomainLayer.Entity.Models.Auth;
using FluentValidation;

namespace EVO.InfrastructureLayer.Validators.Auth
{
    public  class LoginValidator : AbstractValidator<AuthLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public LoginValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(user => user.AuthLoginRequest.EmailAddress).NotEmpty().WithMessage("Login is required");
            RuleFor(user => user.AuthLoginRequest.Password).NotNull().WithMessage("Password is required");

            RuleFor(x => x)
                .Must(ExistUser).WithMessage("Invalid login or password")
                .OverridePropertyName("Login");
        }

        private bool ExistUser(AuthLoginCommand loginCommand)
        {
            return _userRepository.IsUserExist(loginCommand.AuthLoginRequest);
        }
    }
}
