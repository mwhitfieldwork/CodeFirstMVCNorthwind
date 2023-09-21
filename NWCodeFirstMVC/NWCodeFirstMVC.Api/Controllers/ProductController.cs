using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWCodeFirstMVC.App.Contracts;
using NWCodeFirstMVC.Domain;
using NWCodeFirstMVC.Domain.Dto;
using NWCodeFirstMVC.Domain.Models;

namespace NWCodeFirstMVC.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAProduct(int id)
        {
            var product = await _productService.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /*
        [HttpGet("GetLuxUsProd")]
        public IActionResult GetLuxuryUSProduct() //IActionResult returns the json data
        {
            //returning a response ( ok, 404, ect) 
            // use the inteface which is used a  service
            return Ok(_productService.GetLuxuryUSProduct()); 
        }

        [HttpGet("GetProdOrders")]
        public IActionResult GetProdOrders() //IActionResult returns the json data
        {
            //returning a response ( ok, 404, ect) 
            // use the inteface which is used a  service
            var results = _productService.GetProductWithHighQuantityOrders();

            return Ok(results);
        }*/

        [HttpPost("AddProduct")]
        public ActionResult AddProduct(ProductDto createproduct)
        {
            var product = mapper.Map<Product>(createproduct);
            var results = _productService.AddAsync(product);
            return CreatedAtAction("GetAllProduct", new { ProductId = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updateProductDto)
        {
            if(id != updateProductDto.ProductId)
            {
                return BadRequest("Invalid ID");
            }

           if(updateProductDto == null)
            {
                return NotFound();
            }
            var product = mapper.Map<Product>(updateProductDto);
            try
            {
              await _productService.UpdateAsync(product);
            }
            catch(DbUpdateConcurrencyException)
            {
                if(product.ProductId != id)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                   
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteAsync(id);

            return NoContent();

        }
    }
}
