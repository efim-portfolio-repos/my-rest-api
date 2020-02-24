using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyRestApi.Infrastructure.Entities;

namespace MyRestApi.Infrastructure
{
  public class MyRestApiContext : DbContext
  {

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagProduct> TagProducts { get; set; }

    private readonly ILoggerFactory _loggerFactory;

    public MyRestApiContext(
      DbContextOptions<MyRestApiContext> options,
      ILoggerFactory loggerFactory) : base(options)
    {
      _loggerFactory = loggerFactory;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<TagProduct>()
        .HasKey(tagProduct => new { tagProduct.ProductId, tagProduct.TagId });

      modelBuilder.Entity<TagProduct>()
        .HasOne(tagProduct => tagProduct.Product)
        .WithMany(product => product.TagProducts)
        .HasForeignKey(tagProduct => tagProduct.ProductId);

      modelBuilder.Entity<TagProduct>()
        .HasOne(tagProduct => tagProduct.Tag)
        .WithMany(tag => tag.TagProducts)
        .HasForeignKey(tagProduct => tagProduct.TagId);

      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseLoggerFactory(_loggerFactory);
    }

    public async static Task Seed(IServiceProvider serviceProvider)
    {
      await SeedDatabase(serviceProvider);
    }

    private async static Task SeedDatabase(IServiceProvider serviceProvider)
    {
      MyRestApiContext context = serviceProvider.GetRequiredService<MyRestApiContext>();


      if (!context.Products.Any())
      {
        Product newProduct = new Product
        {
          Name = "Product1",
          Price = 3.5,
          Description = "Product description",
          Category = new Category
          {
            Name = "Category1"
          },
        };

        await context.Products.AddAsync(newProduct);
        await context.SaveChangesAsync();
      }
    }
  }
}