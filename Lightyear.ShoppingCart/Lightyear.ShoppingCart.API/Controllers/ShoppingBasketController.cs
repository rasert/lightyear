using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lightyear.ShoppingCart.Application.Abstractions;
using Lightyear.ShoppingCart.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lightyear.ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService _shoppingBasketService;

        public ShoppingBasketController(IShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
        }

        // GET: api/shoppingbasket
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _shoppingBasketService.GetAllAsync());
        }

        // GET api/shoppingbasket/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return Ok(await _shoppingBasketService.FindAsync(id));
        }

        // POST api/shoppingbasket
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ShoppingBasket basket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _shoppingBasketService.AddAsync(basket);
            return CreatedAtAction(nameof(PostAsync), basket);
        }

        // PUT api/shoppingbasket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]ShoppingBasket updatedBasket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ShoppingBasket currentBasket = await _shoppingBasketService.FindAsync(id);
            if (currentBasket == null)
                return NotFound(id);

            await _shoppingBasketService.UpdateAsync(updatedBasket);
            return Ok();
        }

        // DELETE api/shoppingbasket/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ShoppingBasket basket = await _shoppingBasketService.FindAsync(id);
            if (basket == null)
                return NotFound(id);

            await _shoppingBasketService.RemoveAsync(basket);
            return NoContent();
        }
    }
}
