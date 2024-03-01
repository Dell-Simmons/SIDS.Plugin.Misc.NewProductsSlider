using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace SIDS.Plugin.Misc.NewProductsSlider.Models
{
    public record RecentArrivalsPublicInfoModel : BaseNopModel
    {
        public IList<SliderPicModel> SliderPics { get; set; }
    }
}