using System.Collections.Generic;

namespace MyRestApi.Infrastructure.Entities
{
  public class Tag : BaseEntity
  {
    public string Name { get; set; }

    public IEnumerable<TagProduct> TagProducts { get; set; }
  }
}