using Service.Catalog.Products.DTOs;
using Service.Catalog.Products.DTOs.Manage;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest editRequest);
        Task<int> Delete(int productid);
        Task<bool> UpdatePrice(int productid,decimal newPrice);
        Task<bool> UpdateStock(int productid,int addedQuantity);
        Task AddViewCount(int productid);
        Task<List<ProductViewModel>> GetAll();
        Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
    }
}
