using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Products;

namespace DinhQuangTung_ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProduct _publicProduct;

        public ProductController(IPublicProduct publicProduct)
        {
            _publicProduct = publicProduct;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProduct.GetAll();
            return Ok(products);
        }
    }
}
