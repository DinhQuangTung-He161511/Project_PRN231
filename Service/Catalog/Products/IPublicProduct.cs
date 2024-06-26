using ViewModels.Catalog.Product.Public;
using ViewModels.Common;

namespace Service.Catalog.Products
{
    public interface IPublicProduct
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
