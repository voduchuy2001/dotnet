using Api.Models;
using Api.Repositories.Interfaces;
using Api.Requests;
using Api.Services.Interfaces;

namespace Api.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<bool> CreateProduct(StoreProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        };
        await _productRepository.AddAsync(product);
        return true;
    }

    public async Task<bool?> UpdateProduct(int id, UpdateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
        };

        await _productRepository.UpdateAsync(id, product);
        return true;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        return await _productRepository.DeleteAsync(id);
    }
}