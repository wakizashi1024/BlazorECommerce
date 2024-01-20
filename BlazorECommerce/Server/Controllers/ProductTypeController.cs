using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class ProductTypeController : ControllerBase
{
    private readonly IProductTypeService _productTypeService;

    public ProductTypeController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<ProductType>>>> GetProductType()
    {
        var response = await _productTypeService.GetProductTypesAsync();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<IEnumerable<ProductType>>>> GreateProductType(ProductType productType)
    {
        var response = await _productTypeService.CreateProductTypesAsync(productType);

        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<IEnumerable<ProductType>>>> UpdateProductType(ProductType productType)
    {
        var response = await _productTypeService.UpdateProductTypesAsync(productType);

        return Ok(response);
    }
}
