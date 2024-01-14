using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<IEnumerable<CartProductResponseDto>>>> StoreCartItems(ICollection<CartItem> cartItems)
    {
        // var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        // var result = await _cartService.StoreCartItems(cartItems, userId);
        var result = await _cartService.StoreCartItems(cartItems);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
    {
        return await _cartService.GetCartItemsCount();
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<CartProductResponseDto>>>> GetDbCartProudcts()
    {
        var result = await _cartService.GetDbCartProducts();

        return Ok(result);
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
    {
        var result = await _cartService.AddToCart(cartItem);

        return Ok(result);
    }

    [Authorize]
    [HttpPut("update-quantity")]
    public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantity(CartItem cartItem)
    {
        var result = await _cartService.UpdateQuantity(cartItem);

        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{productId}/{productTypeId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCart(int productId, int productTypeId)
    {
        var result = await _cartService.RemoveItemFromCart(productId, productTypeId);

        return Ok(result);
    }
}
