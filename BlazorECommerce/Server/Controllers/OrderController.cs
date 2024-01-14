using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
    {
        var response = await _orderService.PlaceOrder();
        
        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<OrderOverviewResponseDto>>>> GetOrders()
    {
        var result = await _orderService.GetOrders();

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{orderId}")]
    public async Task<ActionResult<ServiceResponse<OrderDetailsResponseDto>>> GetOrderDetails(int orderId)
    {
        var result = await _orderService.GetOrderDetails(orderId);

        return Ok(result);
    }
}
