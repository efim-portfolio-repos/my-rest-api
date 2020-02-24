using System.Collections.Generic;
using System.Threading.Tasks;
using MyRestApi.Presentation.Dto;

namespace MyRestApi.Domain.UseCases
{
  public interface IProductUseCase
  {
    Task<int> Add(CreateProductRequest newProduct);
    Task<ProductResponseList> Search();
    Task<ProductResponse> GetById(int id);
    Task<int> Update(int id, UpdateProductRequest product);
    Task<ProductResponse> Remove(int id);
  }
}