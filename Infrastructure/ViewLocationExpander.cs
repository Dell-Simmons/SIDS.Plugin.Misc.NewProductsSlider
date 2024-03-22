using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace SIDS.Plugin.Misc.NewProductsSlider.Infrastructure
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        #region Methods
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.AreaName == "Admin")
            {
                viewLocations = new[] { $"/Plugins/SIDS.Plugin.Misc.NewProductsSlider/Areas/Admin/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/SIDS.Plugin.Misc.NewProductsSlider/Views/{context.ControllerName}/{context.ViewName}.cshtml" }.Concat(viewLocations);
            }

            return viewLocations;
        }
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
        #endregion
    }
}
