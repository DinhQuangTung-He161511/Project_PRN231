using Service.Catalog.Products.DTOs;
using Service.Catalog.Products.DTOs.Public;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Catalog.Products
{
    public interface IPublicProduct
    {
         PageResult<ProductViewModel> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
