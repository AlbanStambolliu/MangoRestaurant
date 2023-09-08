using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
	public class ProductServices : BaseServices, IProductService
	{
		private readonly IHttpClientFactory _clientFactory;
		public ProductServices(IHttpClientFactory  clientFactory) : base(clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<T> CreateProductAsync<T>(ProductDto productDto)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = productDto,
				Url = SD.ProductAPIBase + "/api/products",
				AccessToken = ""
			});
			
		}

		public async Task<T> DeleteProductAsync<T>(int id)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = SD.ProductAPIBase + "/api/products/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> GetAllProductAsync<T>()
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/api/products",
				AccessToken = ""
			});
		}

		public async Task<T> GetAProductByIdAsync<T>(int id)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.ProductAPIBase + "/api/products/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
		{
			return await this.SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = productDto,
				Url = SD.ProductAPIBase + "/api/products",
				AccessToken = ""
			});
		}
	}
}
