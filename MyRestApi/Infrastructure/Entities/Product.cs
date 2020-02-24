using System.Collections.Generic;

namespace MyRestApi.Infrastructure.Entities
{
  public class Product : BaseEntity
  {
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }

    public int? CategoryId { get; set; }
    public Category Category { get; set; }

    public IEnumerable<TagProduct> TagProducts { get; set; }

    public IEnumerable<Comment> Comments { get; set; }

    public IEnumerable<Feature> Features { get; set; }
  }
}