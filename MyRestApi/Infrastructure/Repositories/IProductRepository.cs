using System.Threading.Tasks;
using System.Collections.Generic;
using MyRestApi.Infrastructure.Entities;

namespace MyRestApi.Infrastructure.Repositories
{
  public interface IProductRepository
  {
    Task<Product> Add(Product newProduct);
    Task<List<Product>> Search();
    Task<Product> GetById(int id);
    Task<int> Update(Product product);
    Task<Product> Remove(int id);
  }
}