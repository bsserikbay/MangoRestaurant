using AutoMapper;
using Mango.Servces.ProductAPI.Models;
using Mango.Servces.ProductAPI.Models.Dto;

namespace Mango.Servces.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });

            return mappingConfig;
        }
    }
}
