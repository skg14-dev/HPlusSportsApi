using HPlusSportApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext shopContext;

        public ProductsController(ShopContext shopContext)
        {
            this.shopContext=shopContext;
            this.shopContext.Database.EnsureCreated();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await shopContext.Products.ToListAsync();
            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(products);

        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProducts([FromBody] Product product)
        {
            shopContext.Products.Add(product);
            await shopContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await shopContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, [FromBody] Product productToUpdate)
        {
            if (id != productToUpdate.Id)
            {
                return BadRequest();
            }
            if (!shopContext.Products.Any(p => p.Id == productToUpdate.Id))
            {
                return NotFound();
            }
            shopContext.Entry(productToUpdate).State = EntityState.Modified;

            await shopContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {

            var product = await shopContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            shopContext.Products.Remove(product);
            await shopContext.SaveChangesAsync();   
            return NoContent();
        }
    }
}
