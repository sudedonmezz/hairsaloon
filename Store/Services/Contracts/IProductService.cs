using Entities.Models;
namespace Services.Contracts;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts(bool trackChanges);

    Product? GetOneProduct(int id,bool trackChanges);

     IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges);


    void CreateProduct(Product product);

    void UpdateOneProduct(Product product);

    void DeleteOneProduct(int id);

    bool HasAppointmentsForProduct(int productId);
}
