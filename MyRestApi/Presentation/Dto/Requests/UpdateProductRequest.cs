using System.Collections.Generic;

namespace MyRestApi.Presentation.Dto
{
  public class UpdateProductRequest
  {
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }

    public int? CategoryId { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public IEnumerable<UpdateFeatureRequest> Features { get; set; }
  }
}