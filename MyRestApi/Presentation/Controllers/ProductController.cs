using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyRestApi.Presentation.Dto;
using MyRestApi.Domain.UseCases;


namespace MyRestApi.Presentation.Controllers
{
  [ApiController]
  [Route("products")]
  public class ProductController : ControllerBase
  {
    private readonly IProductUseCase _productUseCase;
    public ProductController(IProductUseCase productUseCase)
    {
      _productUseCase = productUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
      ProductResponseList list = await _productUseCase.Search();
      return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      ProductResponse response = await _productUseCase.GetById(id);
      return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateProductRequest newProduct)
    {
      int id = await _productUseCase.Add(newProduct);
      var response = new { Id = id };
      return CreatedAtAction(nameof(GetById), response, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request)
    {
      int updatedEntitiesCount = await _productUseCase.Update(id, request);
      if (updatedEntitiesCount < 1)
      {
        return BadRequest();
      }

      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      var removedProduct = await _productUseCase.Remove(id);
      if (removedProduct == null)
      {
        return NotFound();
      }

      return Ok();
    }
  }
}