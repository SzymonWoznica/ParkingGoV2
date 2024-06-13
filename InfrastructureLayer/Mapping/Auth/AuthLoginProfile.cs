using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.Auth.Login;
using AutoMapper;
using DomainLayer.Entity;

namespace InfrastructureLayer.Mapping.Auth
{
    public class AuthLoginProfile : Profile
    {
        public AuthLoginProfile()
        {
            CreateMap<LoginRequestDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));


        }
    }
}
