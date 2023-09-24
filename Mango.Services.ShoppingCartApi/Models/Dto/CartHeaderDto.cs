using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartApi.Models.Dto
{
    public class CartHeaderDto
    {
        [Key]
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }


    }
}
