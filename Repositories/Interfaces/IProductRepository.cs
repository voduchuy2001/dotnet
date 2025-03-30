using Api.Models;

namespace Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<bool> AddAsync(Product product);
    Task<bool?> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
}