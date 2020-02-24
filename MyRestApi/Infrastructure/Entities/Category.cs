using System.Collections.Generic;

namespace MyRestApi.Infrastructure.Entities
{
  public class Category : BaseEntity
  {
    public string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }
  }
}