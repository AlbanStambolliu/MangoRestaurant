using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
	public interface IProductService : IBaseService
	{
		Task<T> GetAllProductAsync<T>();
		Task<T> GetAProductByIdAsync<T>(int id);
		Task<T> CreateProductAsync<T>(ProductDto productDto);
		Task<T> UpdateProductAsync<T>(ProductDto productDto);
		Task<T> DeleteProductAsync<T>(int id);

	}
}
