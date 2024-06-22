using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Catalog.Products.DTOs;
using Service.Catalog.Products.DTOs.Manage;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace Service.Catalog.Products
{
    public class ManagerProductService : IManageProductService
    {
        private readonly EshopDBContext _context;
        public ManagerProductService(EshopDBContext context)
        {
            _context = context;
        }

        public async Task AddViewCount(int productid)
        {
            var product = await _context.Products.FindAsync(productid);
            product.ViewCount += 1;
           await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {

            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,  
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,    
                        Description = request.Description,  
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,    
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productid)
        {
            var product = await _context.Products.FindAsync(productid);
            if (productid == null)
            {
                throw new EshopExceptions("Cannot find product");
            }
           
           _context.Products.Remove(product);
           return await _context.SaveChangesAsync();
        }


        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            /*//1.Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new {p,pt,pic};
            //2.Filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            if(request.CategoryId.Count > 0)
            {
                query = query.Where(p => request.CategoryId.Contains(p.pic.CategoryId));
            }
           //3.Paging
           int TotalRow = await query.CountAsync();
            var data =await query.Skip((request.PageIndex-1)*request.PageSize).
                Take(request.PageSize)
                .Select(x=>new ProductViewModel()
                {
                    Id=x.p.Id,
                    Name=x.pt.Name,
                    Description=x.pt.Description,
                    Details=x.pt.Details,
                    LanguageId=x.pt.LanguageId,
                    OriginalPrice=x.p.OriginalPrice,
                    Price=x.p.Price,
                    SeoAlias=x.pt.SeoAlias,
                    SeoDescription=x.pt.SeoDescription,
                    SeoTitle=x.pt.SeoTitle,
                    Stock=x.p.Stock,
                    ViewCount=x.p.ViewCount

                }).ToListAsync();
            // 4.Select
            var pageResult = new PageResult<ProductViewModel>()
            {
                //TotalRecord = TotalRow,
                //Items = data
            };*/
            throw new Exception();
        }

        public async Task<int> Update(ProductUpdateRequest editRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePrice(int productid, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStock(int productid, int addedQuantity)
        {
            throw new NotImplementedException();
        }

        Task<int> IManageProductService.Delete(int productid)
        {
            throw new NotImplementedException();
        }

        Task<List<ProductViewModel>> IManageProductService.GetAll()
        {
            throw new NotImplementedException();
        }
        Task<int> IManageProductService.Update(ProductUpdateRequest editRequest)
        {
            throw new NotImplementedException();
        }
    }
}
