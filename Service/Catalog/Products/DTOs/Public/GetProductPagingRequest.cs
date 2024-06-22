using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Catalog.Products.DTOs.Public
{
    public class GetProductPagingRequest :PagingRequestBase
    {
        public int CategoryId { get; set; }
    }
}
