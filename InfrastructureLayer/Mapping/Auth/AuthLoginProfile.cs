using ApplicationLayer.DTOs.Auth.Login;
using AutoMapper;
using EVO.DomainLayer.Entity.Models.Auth;

namespace InfrastructureLayer.Mapping.Auth
{
    public class AuthLoginProfile : Profile
    {
        public AuthLoginProfile()
        {
            CreateMap<LoginRequestDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));


        }
    }
}
