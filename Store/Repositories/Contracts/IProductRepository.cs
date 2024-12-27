using Entities.Models;
namespace Repositories.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    IQueryable<Product> GetAllProducts(bool trackChanges);

    Product? GetOneProduct(int id,bool trackChanges);

    void CreateProduct(Product product);

    void DeleteOneProduct(Product product);

    bool HasAppointmentsForProduct(int productId);

    // Kategorisine göre ürünleri getiren yeni metot
    IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges);

}
