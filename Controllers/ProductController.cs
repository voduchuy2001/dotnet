using Api.Attributes;
using Api.Requests;
using Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("product")]
[ApiController]
public class ProductController(IProductService productService, IServiceProvider serviceProvider) : ControllerBase
{
    private readonly IProductService _productService = productService;

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    [HttpGet]
    [HasPermission("get_products")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [HasPermission("get_product")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    [HasPermission("created_product")]
    public async Task<IActionResult> Create([FromBody] StoreProductRequest request)
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<StoreProductRequest>>();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return UnprocessableEntity(result.Errors);
        }

        await _productService.CreateProduct(request);
        return StatusCode(201);
    }

    [HttpPut("{id:int}")]
    [HasPermission("update_product")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var validator = _serviceProvider.GetRequiredService<IValidator<UpdateProductRequest>>();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return UnprocessableEntity(result.Errors);
        }

        var updatedProduct = await _productService.UpdateProduct(id, request);
        if (updatedProduct is null)
        {
            return NotFound();
        }
        return Ok(updatedProduct);
    }

    [HttpDelete("{id:int}")]
    [HasPermission("delete_product")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteProduct(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}