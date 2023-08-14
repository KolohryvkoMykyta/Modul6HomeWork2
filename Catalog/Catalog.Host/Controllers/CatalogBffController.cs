using System.Data;
using System.Net;
using Catalog.Host.Data.Entities;
using Catalog.Host.Enums;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICatalogBrandService _brandService;
    private readonly ICatalogTypeService _typeService;

    public CatalogBffController(
        ICatalogTypeService catalogTypeService,
        ICatalogBrandService brandService,
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _brandService = brandService;
        _typeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetByResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(GetByRequest<int> request)
    {
        var result = await _catalogService.GetItemByIdAsync(request.Request);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetByResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrand(GetByRequest<string> request)
    {
        var result = await _catalogService.GetItemByBrandAsync(request.Request);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetByResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(GetByRequest<string> request)
    {
        var result = await _catalogService.GetItemByTypeAsync(request.Request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        var result = await _brandService.GetAllBrandsAsync();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _typeService.GetAllTypeAsync();
        return Ok(result);
    }
}