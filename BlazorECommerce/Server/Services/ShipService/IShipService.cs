namespace BlazorECommerce.Server.Services.UserInfoService;

public interface IShipService
{
    Task<ServiceResponse<ShipInfo>> GetShipInfo();
    Task<ServiceResponse<ShipInfo>> CreateOrUpdateShipInfo(ShipInfo address);
}
