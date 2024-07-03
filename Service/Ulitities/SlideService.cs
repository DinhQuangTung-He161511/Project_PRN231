using Data.EF;
using Microsoft.EntityFrameworkCore;
using ViewModels.Ulitilies.Slides;

namespace Service.Ulitities
{
    public class SlideService : ISlideService
    {
        private readonly EshopDBContext _context;

        public SlideService(EshopDBContext context)
        {
            _context = context;
        }

        public async Task<List<SlideVm>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                .Select(x => new SlideVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    Image = x.Image
                }).ToListAsync();

            return slides;
        }
    }
}
