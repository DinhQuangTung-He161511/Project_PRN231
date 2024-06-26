using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using ViewModels.Catalog.Product;
using ViewModels.Catalog.Product.Manage;

using Utilities.Exceptions;
using ViewModels.Common;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.VariantTypes;
using System.Net.Http.Headers;
using Service.Common;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;

namespace Service.Catalog.Products
{
    public class ManagerProductService : IManageProductService
    {
        private readonly EshopDBContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ManagerProductService(EshopDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
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
            //Save Image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>
                {
                    new ProductImage()
                    {
                        Caption="Thumbail image",
                        DateCreated= DateTime.Now,
                        FileSize=request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault =true,
                        SortOrder =1

                    }
                };
            }
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
            var images =  _context.ProductImages.Where(x=> x.ProductId == productid);
            foreach (var image in images)
            {
               await _storageService.DeleteFileAsync(image.ImagePath);
            }
          
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }


        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //1.Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //2.Filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            if (request.CategoryId.Count > 0)
            {
                query = query.Where(p => request.CategoryId.Contains(p.pic.CategoryId));
            }
            //3.Paging
            int TotalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).
                Take(request.PageSize)
                .Select(x => new ProductViewModel()
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
            // 4.Select
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = TotalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest editRequest)
        {
            var product = await _context.Products.FindAsync(editRequest.Id);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == editRequest.Id
            && x.LanguageId == editRequest.LanguageId);
            if (productTranslation == null || product == null)
            {
                throw new EshopExceptions($"Cannot find a product with id :{editRequest.Id} ");
            }
            productTranslation.Name = editRequest.Name;
            productTranslation.SeoAlias = editRequest.SeoAlias;
            productTranslation.SeoDescription = editRequest.SeoDescription;
            productTranslation.SeoTitle = editRequest.SeoTitle;
            productTranslation.Description = editRequest.Description;
            productTranslation.Details = editRequest.Details;
            //Save Image
            if (editRequest.ThumbnailImage != null)
            {
                var thumbailImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true
                && x.ProductId == editRequest.Id);
                if (thumbailImage != null)
                {
                    thumbailImage.FileSize = editRequest.ThumbnailImage.Length;
                    thumbailImage.ImagePath = await this.SaveFile(editRequest.ThumbnailImage);
                    _context.ProductImages.Update(thumbailImage);
                }
            }
            return await _context.SaveChangesAsync();

        }
        public async Task<bool> UpdatePrice(int productid, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productid);
            if (product == null)
            {
                throw new EshopExceptions($"Cannot find a product with id :{productid} ");
            }
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productid, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productid);
            if (product == null)
            {
                throw new EshopExceptions($"Cannot find a product with id :{productid} ");
            }
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        //public async Task<int> Delete(Product productid)
        //{
        //    var product = await _context.Products.FindAsync(productid);
        //    if (product == null)
        //    {
        //        throw new EshopExceptions($"Cannot find a product with id :{productid} ");
        //    }
        //    _context.Products.Remove(productid);
        //    return await _context.SaveChangesAsync();
        //}
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public Task<int> AddImage(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImage(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int imageId, string caption, bool IsDefault)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImageViewModel> GetImageById(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
