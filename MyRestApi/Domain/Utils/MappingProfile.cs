using System.Linq;
using AutoMapper;
using MyRestApi.Infrastructure.Entities;
using MyRestApi.Presentation.Dto;

namespace MyRestApi.Domain.Utils
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Product, ProductResponse>()
        .ForMember(
          pr => pr.Tags,
          p => p.MapFrom(p =>
            p.TagProducts.Select(tp => new TagResponse
            {
              Id = tp.Tag.Id,
              Name = tp.Tag.Name
            })));

      CreateMap<CreateProductRequest, Product>();
      CreateMap<UpdateProductRequest, Product>();

      CreateMap<Category, CategoryResponse>();

      CreateMap<CreateFeatureRequest, Feature>();
      CreateMap<UpdateFeatureRequest, Feature>();
      CreateMap<Feature, FeatureResponse>();
    }
  }
}