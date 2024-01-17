using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ShipController : ControllerBase
{
    private readonly IShipService _shipService;

    public ShipController(IShipService shipService)
    {
        _shipService = shipService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<ShipInfo>>> GetShipInfo()
    {
        return await _shipService.GetShipInfo();
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<ShipInfo>>> CreateOrUpdateShipInfo(ShipInfo shipInfo)
    {
        return await _shipService.CreateOrUpdateShipInfo(shipInfo);
    }
}
