using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Ulitilies.Slides;

namespace Service.Ulitities
{
    public interface ISlideService
    {
        Task<List<SlideVm>> GetAll();
    }
}
