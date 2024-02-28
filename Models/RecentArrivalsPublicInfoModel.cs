using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.SIDS.NivoSlider.Models
{
    public record RecentArrivalsPublicInfoModel : BaseNopModel
    {
        public IList<SliderPicModel> SliderPics { get; set; }
    }
}