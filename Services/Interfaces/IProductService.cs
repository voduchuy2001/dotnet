using Api.Models;
using Api.Requests;

namespace Api.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task<bool> CreateProduct(StoreProductRequest request);
    Task<bool?> UpdateProduct(int id, UpdateProductRequest request);
    Task<bool> DeleteProduct(int id);
}