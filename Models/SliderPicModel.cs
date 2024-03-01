using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace SIDS.Plugin.Misc.NewProductsSlider.Models
{
    public record SliderPicModel : BaseNopModel
    {
        public string PictureUrl { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string AltText { get; set; }
        public string DataTransition { get; set; }
    }
}
