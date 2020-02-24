namespace MyRestApi.Infrastructure.Entities
{
  public class Comment : BaseEntity
  {
    public string UserName { get; set; }
    public string Text { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}