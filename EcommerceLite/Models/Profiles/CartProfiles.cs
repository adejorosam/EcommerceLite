using System;
using AutoMapper;
namespace EcommerceLite.Models.Profiles
{
    public class CartProfiles : Profile
    {
        public CartProfiles()
        {
            CreateMap<Domain.Cart, DTO.AddToCartRequest>()
                .ReverseMap();

            //CreateMap<Domain.Cart, DTO.UpdateCategoryRequest>()
            //    .ReverseMap();
        }
    }
}

