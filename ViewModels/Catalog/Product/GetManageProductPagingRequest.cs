﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Common;

namespace ViewModels.Catalog.Product
{
    public class GetManageProductPagingRequest :PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId {  get; set; }  
    }
}