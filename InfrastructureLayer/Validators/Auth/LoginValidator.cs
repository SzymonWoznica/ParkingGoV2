using ApplicationLayer.Contracts.Auth;
using ApplicationLayer.CQRS.Commands.Auth;
using FluentValidation;

namespace EVO.InfrastructureLayer.Validators.Auth
{
    public  class LoginValidator : AbstractValidator<AuthLoginCommand>
    {
        private readonly IUserRepository _userRepository;

        public LoginValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.AuthLoginRequest.EmailAddress).NotEmpty().DependentRules(() => {
                RuleFor(x => x.AuthLoginRequest.Password).NotEmpty().DependentRules(() =>
                {
                    RuleFor(x => x)
                        .Must(ExistUser).WithMessage("Invalid login or password")
                        .OverridePropertyName("Login");

                }).WithMessage("Password is required");

            }).WithMessage("Login is required");
        }

        private bool ExistUser(AuthLoginCommand loginCommand)
        {
            return _userRepository.IsUserExist(loginCommand.AuthLoginRequest);
        }
    }
}
