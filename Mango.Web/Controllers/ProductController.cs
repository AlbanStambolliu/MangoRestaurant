using Mango.Web.Models;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productServices;


        public ProductController(IProductService productServices)
        {
            this._productServices = productServices;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> products = new List<ProductDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productServices.GetAllProductAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

            }
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productServices.CreateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            }
            return View();
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            ProductDto product = new ProductDto();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productServices.GetAProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productServices.UpdateProductAsync<ResponseDto>(model, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            }
            return View();
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ProductDto product = new ProductDto();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productServices.GetAProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productServices.DeleteProductAsync<ResponseDto>(model.ProductId,accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));

                }
            }
            return View(model);
        }
    }
}
