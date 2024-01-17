namespace BlazorECommerce.Client.Services.UserInfoService;

public interface IShipService
{
    Task<ShipInfo> GetShipInfo();
    Task<ShipInfo> CreateOrUpdateShipInfo(ShipInfo shipInfo);
}
