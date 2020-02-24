using System.Collections.Generic;

namespace MyRestApi.Presentation.Dto
{
  public class ProductResponseList
  {
    public IEnumerable<ProductResponse> Products { get; set; }
  }
}