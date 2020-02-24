using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MyRestApi.Presentation.Dto;
using MyRestApi.Infrastructure.Repositories;
using MyRestApi.Infrastructure.Entities;

namespace MyRestApi.Domain.UseCases
{
  public class ProductUseCase : IProductUseCase
  {
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductUseCase(
      IProductRepository productRepository,
      IMapper mapper
    )
    {
      _productRepository = productRepository;
      _mapper = mapper;
    }

    public async Task<int> Add(CreateProductRequest request)
    {
      Product product = _mapper.Map<CreateProductRequest, Product>(request);
      await _productRepository.Add(product);
      return product.Id;
    }

    public async Task<ProductResponse> GetById(int id)
    {

      var product = await _productRepository.GetById(id);
      var response = _mapper.Map<Product, ProductResponse>(product);

      return response;
    }

    public async Task<ProductResponse> Remove(int id)
    {
      var removedProduct = await _productRepository.Remove(id);
      return removedProduct != null ?
        _mapper.Map<Product, ProductResponse>(removedProduct) :
        null;
    }

    public async Task<ProductResponseList> Search()
    {
      var searchResult = await _productRepository.Search();
      var productResponses = _mapper.Map<IEnumerable<Product>, List<ProductResponse>>(searchResult);
      return new ProductResponseList
      {
        Products = productResponses
      };
    }

    public Task<int> Update(int id, UpdateProductRequest request)
    {
      Product product = _mapper.Map<UpdateProductRequest, Product>(request);
      product.Id = id;
      return _productRepository.Update(product);
    }
  }
}