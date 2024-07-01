using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Products;
using ViewModels.Catalog.Product;

namespace DinhQuangTung_ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProduct _publicProduct;
        private readonly IManageProductService _manageProduct;

        public ProductController(IPublicProduct publicProduct, IManageProductService manageProduct)
        {
            _publicProduct = publicProduct;
            _manageProduct = manageProduct;
        }
        //http://localhostport/product
        [HttpGet("{languageId}")]
        public async Task<IActionResult> Get(string languageId)
        {
            var products = await _publicProduct.GetAll(languageId);
            return Ok(products);
        }
        //http://localhostport/product/public-paging
        [HttpGet("public-paging/{languageId}")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProduct.GetAllByCategoryId(request);
            return Ok(products);
        }
        //http://localhostport/product/id/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var products = await _manageProduct.GetById(productId, languageId);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _manageProduct.Create(request);
            if (productId == 0)
            {
                return BadRequest("Product not found");
            }
            var product = await _manageProduct.GetById(productId, request.LanguageId);
            return Created(nameof(GetById), product);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResult = await _manageProduct.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest("Product not found");
            }

            return Ok();
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProduct.Delete(productId);
            if (affectedResult == 0)
            {
                return BadRequest("Product not found");
            }

            return Ok();
        }
        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var result = await _manageProduct.UpdatePrice(id, newPrice);
            if (result == true)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
