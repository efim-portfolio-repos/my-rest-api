using System.Collections.Generic;

namespace MyRestApi.Presentation.Dto
{
  public class ProductResponse
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public CategoryResponse Category { get; set; }
    public IEnumerable<TagResponse> Tags { get; set; }
    public IEnumerable<CommentResponse> Comments { get; set; }
    public IEnumerable<FeatureResponse> Features { get; set; }
  }
}