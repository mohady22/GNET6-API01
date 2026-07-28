using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    
    public class BasketController : ApiBaseController
    {
        private readonly IBasketServices basketServices;

        public BasketController(IBasketServices basketServices)
        {
            this.basketServices = basketServices;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct)
        {
            var basket = await basketServices.GetBasketAsync(id, ct);

            return ToActionResult(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket , CancellationToken ct)
        {
            var result = await basketServices.CreateOrUpdateBasketAsync(basket, ct);

            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct)
        {
            var result = await basketServices.DeleteBasketAsync(id, ct);

            return ToActionResult(result);
        }
    }
}
