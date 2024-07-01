using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;

namespace ViewModels.Catalog.Product
{
    public class GetPublicProductPagingRequest :PagingRequestBase
    {
        public int? CategoryId { get; set; }
        public string LanguageId {  get; set; } 
    }
}
