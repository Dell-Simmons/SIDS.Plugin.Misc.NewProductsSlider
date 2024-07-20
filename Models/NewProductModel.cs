using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace SIDS.Plugin.Misc.NewProductsSlider.Models;
public record NewProductModel: BaseNopModel
{
    public string Sku { get; set; }
    public bool IsNew { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
