using System.Data;
using System.Net;
using Catalog.Host.Data.Entities;
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

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
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

    [HttpPost]
    [ProducesResponseType(typeof(GetByResponse<IEnumerable<CatalogItemDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrands(GetByRequest<string> request)
    {
        var result = await _catalogService.GetAllBrandAsync(request.Request);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetByResponse<IEnumerable<CatalogItemDto>>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes(GetByRequest<string> request)
    {
        var result = await _catalogService.GetAllTypeAsync(request.Request);
        return Ok(result);
    }
}