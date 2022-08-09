using System;
using AutoMapper;

namespace EcommerceLite.Models.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Domain.User, DTO.User>()
                .ReverseMap();

            CreateMap<Domain.User, DTO.RegisterRequest>()
                .ReverseMap();

            CreateMap<Domain.User, DTO.LoginRequest>()
                .ReverseMap();
        }
    }
}