using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentWebApp.Interfaces;
using TalentWebApp.Models;

namespace TalentWebApp.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService productsService, IAppLogger logger)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var result = await _productsService.GetAllProducts();
            return Ok(result); 
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var result = await _productsService.GetById(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost()]
        public async Task<ActionResult<Product>> AddNew(Product p)
        {
            var result = await _productsService.AddNewProduct(p);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProcuct(int id)
        {
            try
            {
                await _productsService.DeleteProduct(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product p)
        {
            try
            {
                var productWithUpdates = await _productsService.UpdateProduct(id, p);
                return Ok(productWithUpdates);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound();
            }
        }


    }
}
