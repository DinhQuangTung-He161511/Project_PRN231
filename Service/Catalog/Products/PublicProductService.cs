﻿using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using ViewModels.Catalog.Product;
using ViewModels.Common;

namespace Service.Catalog.Products
{
    public class PublicProductService : IPublicProduct
    {
        private readonly EshopDBContext _context;

        public PublicProductService(EshopDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll(string languageId)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };

            var data = await query.Select(x => new ProductViewModel
            {
                Id = x.p.Id,
                Name = x.pt.Name,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                SeoAlias = x.pt.SeoAlias,
                SeoDescription = x.pt.SeoDescription,
                SeoTitle = x.pt.SeoTitle,
                Stock = x.p.Stock,
                ViewCount = x.p.ViewCount
            }).ToListAsync();

            return data;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId== request.LanguageId
                        select new { p, pt, pic };

            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new ProductViewModel
                                  {
                                      Id = x.p.Id,
                                      Name = x.pt.Name,
                                      Description = x.pt.Description,
                                      Details = x.pt.Details,
                                      LanguageId = x.pt.LanguageId,
                                      OriginalPrice = x.p.OriginalPrice,
                                      Price = x.p.Price,
                                      SeoAlias = x.pt.SeoAlias,
                                      SeoDescription = x.pt.SeoDescription,
                                      SeoTitle = x.pt.SeoTitle,
                                      Stock = x.p.Stock,
                                      ViewCount = x.p.ViewCount
                                  }).ToListAsync();

            var pageResult = new PageResult<ProductViewModel>
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pageResult;
        }
    }
}
