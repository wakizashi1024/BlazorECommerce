using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("products")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<CartProductResponseDto>>>> GetCartProducts(IEnumerable<CartItem> cartItems)
    {
        var result = await _cartService.GetCartProdcuts(cartItems);

        return Ok(result);
    }
}
