using System;
using AutoMapper;

namespace EcommerceLite.Models.Profiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {
            CreateMap<Domain.Category, DTO.AddCategoryRequest>()
                .ReverseMap();

            CreateMap<Domain.Category, DTO.UpdateCategoryRequest>()
                .ReverseMap();
        }
    }
}

