using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IProductService _productService;
		private readonly ICartService _cartService;

		public HomeController(ILogger<HomeController> logger,
			IProductService productService,
            ICartService cartService)
		{
			_logger = logger;
			_productService = productService;
			_cartService = cartService;
		}

		public async Task<IActionResult> Index()
		{
			List<ProductDto> products = new List<ProductDto>();
			var response = await _productService.GetAllProductAsync<ResponseDto>("");
			if (response != null && response.IsSuccess)
			{
				products = JsonConvert.DeserializeObject<List<ProductDto>>(response.Result.ToString());
			}
			return View(products);
		}

		[Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto product = new ProductDto();
            var response = await _productService.GetAProductByIdAsync<ResponseDto>(productId, "");
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(response.Result.ToString());
            }
            return View(product);
        }

		[HttpPost]
		[ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
			CartDto cartDto = new CartDto()
			{
				CartHeader = new CartHeaderDto()
				{
					UserId = User.Claims.Where(u => u.Type == "sub")?.
					FirstOrDefault()?.Value
				}
			};

			CartDetailsDto cartDetailsDto = new CartDetailsDto()
			{
				Count= productDto.Count,
				ProductId = productDto.ProductId,
			};
			var resp = await _productService.GetAProductByIdAsync<ResponseDto>(productDto.ProductId, null);

			if (resp!= null && resp.IsSuccess)
			{
				cartDetailsDto.Product = JsonConvert.DeserializeObject<ProductDto>(resp.Result.ToString());
			}

			List<CartDetailsDto> cartDetailsDtos = new();
			cartDetailsDtos.Add(cartDetailsDto);
			cartDto.CartDetails = cartDetailsDtos;

			var accessToken = await HttpContext.GetTokenAsync("access_token");
			var addToCartResp = await _cartService.AddToCartAsync<ResponseDto>(cartDto, accessToken);
			if (addToCartResp!= null && addToCartResp.IsSuccess)
			{
				return RedirectToAction(nameof(Index));
			}

            return View(productDto);
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[Authorize]
		public async Task<IActionResult> Login()
		{
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}
	}
}