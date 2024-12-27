using Entities.Models;
using Repositories.Contracts;
namespace Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext context) : base(context)
    {

    }
    public IQueryable<Product> GetAllProducts(bool trackChanges) => FindAll(trackChanges);

    public Product? GetOneProduct(int id, bool trackChanges)
    {
        return FindByCondition(p => p.ProductId == id, trackChanges)
               .SingleOrDefault(); // IQueryable -> Product dönüşümü
    }

    public void CreateProduct(Product product) => Create(product);

    public void DeleteOneProduct(Product product) =>Remove(product);

     public IEnumerable<Product> GetProductsByCategoryId(int categoryId, bool trackChanges) =>
        FindByCondition(p => p.CategoryId == categoryId, trackChanges).ToList();



  
// Yeni Metot: Bir ürünün aktif randevuları olup olmadığını kontrol eder.
public bool HasAppointmentsForProduct(int productId)
{
    return FindByCondition(a => a.ProductId == productId, trackChanges: false).Any();
}

    
}
