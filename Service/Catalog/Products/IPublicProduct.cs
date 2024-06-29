
using ViewModels.Catalog.Product;
using ViewModels.Common;

namespace Service.Catalog.Products
{
    public interface IPublicProduct
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();
    }
}
