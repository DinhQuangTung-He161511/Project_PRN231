using Microsoft.AspNetCore.Http;
using ViewModels.Catalog.Product;
using ViewModels.Common;

namespace Service.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest editRequest);
        Task<int> Delete(int productid);
        Task<bool> UpdatePrice(int productid, decimal newPrice);
        Task<bool> UpdateStock(int productid, int addedQuantity);
        Task AddViewCount(int productid);
        Task<int> AddImage(int productId, List<IFormFile> files);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, string caption, bool IsDefault);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<ProductViewModel> GetById(int productId,string languaeId);
        Task<List<ProductImageViewModel>> GetListImages(int productId);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
    }
}
