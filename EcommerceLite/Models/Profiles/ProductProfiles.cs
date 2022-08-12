using AutoMapper;
namespace EcommerceLite.Models.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Domain.Product, DTO.AddProductRequest>()
                .ReverseMap();

            CreateMap<Domain.Product, DTO.UpdateProductRequest>()
                .ReverseMap();
        }
    }
}

