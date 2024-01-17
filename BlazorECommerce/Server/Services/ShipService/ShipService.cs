
using BlazorECommerce.Shared;

namespace BlazorECommerce.Server.Services.UserInfoService;

public class ShipService : IShipService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;

    public ShipService(DataContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<ServiceResponse<ShipInfo>> CreateOrUpdateShipInfo(ShipInfo shipInfo)
    {
        var response = new ServiceResponse<ShipInfo>();

        var dbShipInfo = (await GetShipInfo()).Data;
        if (dbShipInfo is null)
        {
            shipInfo.UserId = _authService.GetUserId();
            _context.ShipInfos.Add(shipInfo);

            response.Data = shipInfo;
        }
        else
        {
            dbShipInfo.FirstName = shipInfo.FirstName;
            dbShipInfo.LastName = shipInfo.LastName;
            dbShipInfo.Country = shipInfo.Country;
            dbShipInfo.State = shipInfo.State;
            dbShipInfo.City = shipInfo.City;
            dbShipInfo.Line1 = shipInfo.Line1;
            dbShipInfo.Line2 = shipInfo.Line2;
            dbShipInfo.PostalCode = shipInfo.PostalCode;
            dbShipInfo.Phone = shipInfo.Phone;

            response.Data = dbShipInfo;
        }

        await _context.SaveChangesAsync();

        return response;
    }

    public async Task<ServiceResponse<ShipInfo>> GetShipInfo()
    {
        int userId = _authService.GetUserId();
        var shipInfo = await _context.ShipInfos
            .FirstOrDefaultAsync(a => a.UserId == userId);

        return new ServiceResponse<ShipInfo> 
        { 
            Data = shipInfo, 
        };
    }
}
