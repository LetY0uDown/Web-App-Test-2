using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController, Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ProductController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _dbContext.Products;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(); 

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Product product)
        {
            var existingProduct = await _dbContext.Products.FindAsync(product.ID);

            if (existingProduct is null)
                return NotFound();

            _dbContext.Entry(existingProduct).CurrentValues.SetValues(product);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}