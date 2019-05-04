using System.Threading.Tasks;
using Lightyear.Catalog.API.Abstractions;
using Lightyear.Catalog.Application.Abstractions;
using Lightyear.Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lightyear.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductEventPublisher _productEventPublisher;

        public ProductsController(IProductService productService, IProductEventPublisher productEventPublisher)
        {
            _productService = productService;
            _productEventPublisher = productEventPublisher;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productService.Get());
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_productService.Find(id));
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Product product)
        {
            await _productService.AddAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]Product product)
        {
            var notifyUpdatedPrice = false;

            if (id != product.Id)
                return BadRequest();

            var oldProduct = _productService.Find(id);
            if (oldProduct == null)
                return NotFound(id);

            notifyUpdatedPrice = oldProduct.Value != product.Value;

            await _productService.UpdateAsync(product);

            if (notifyUpdatedPrice)
            {
                await _productEventPublisher.NotifyUpdatedPriceAsync(new
                {
                    ProductID = product.Id,
                    NewValue = product.Value
                });
            }

            return Ok();
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = _productService.Find(id);

            if (product == null)
                return NotFound();

            await _productService.RemoveAsync(product);

            return Ok();
        }
    }
}
