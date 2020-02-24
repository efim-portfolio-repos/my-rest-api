using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyRestApi.Infrastructure.Entities;
using MyRestApi.Infrastructure;

namespace MyRestApi.Infrastructure.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly MyRestApiContext _context;

    private readonly IQueryable<Product> _deepProductQuery;

    public ProductRepository(MyRestApiContext context)
    {
      _context = context;
      _deepProductQuery = _context.Products
        .Include(product => product.Category)
        .Include(product => product.Comments)
        .Include(product => product.TagProducts)
        .ThenInclude(tagProduct => tagProduct.Tag)
        .Include(product => product.Features);
    }

    public async Task<Product> Add(Product newProduct)
    {
      newProduct.Id = 0;
      await _context.AddAsync(newProduct);
      await SaveChanges();
      return newProduct;
    }

    public Task<Product> GetById(int id)
    {
      return _deepProductQuery
        .FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task<Product> Remove(int id)
    {
      Product dbEntry = _context.Products.FirstOrDefault(e => e.Id == id);

      if (dbEntry != null)
      {
        _context.Products.Remove(dbEntry);
        await SaveChanges();
      }

      return dbEntry;
    }

    public Task<List<Product>> Search()
    {
      return _deepProductQuery.ToListAsync();
    }

    public Task<int> Update(Product product)
    {
      if (_context.Products.Any(product => product.Id == product.Id))
      {
        var missingFeatures = _context.Features.Where(f => f.ProductId == product.Id)
                        .ToList()
                        .Except(product.Features);
        _context.Features.RemoveRange(missingFeatures);
        _context.Products.Update(product);
      }

      return SaveChanges();
    }

    internal Task<int> SaveChanges()
    {
      return _context.SaveChangesAsync();
    }
  }
}