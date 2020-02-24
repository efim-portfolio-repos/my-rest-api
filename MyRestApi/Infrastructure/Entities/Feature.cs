namespace MyRestApi.Infrastructure.Entities
{
  public class Feature : BaseEntity
  {
    public string Name { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}