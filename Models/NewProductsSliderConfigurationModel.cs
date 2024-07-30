using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace SIDS.Plugin.Misc.NewProductsSlider.Models;
public record NewProductsSliderConfigurationModel: BaseNopModel
{
    public IList<NewProductModel> Products { get; set; }
    public int MaxNumberOfSameCatalogNumber { get; set; }
}
