using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<ProductDto, Product>();
				config.CreateMap<Product, ProductDto>();
				
				//Alternative we can use ReverseMap instead of two lines of code
				//config.CreateMap<Product, ProductDto>().ReverseMap();
			});
			return mappingConfig;
		}
	}
}
