using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using SIDS.Plugin.Misc.NewProductsSlider.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIDS.Plugin.Misc.NewProductsSlider.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsNivoSliderController : BasePluginController
    {
        #region Constants and Fields
        private readonly IPermissionService _permissionService;
        private readonly IProductService _productService;
        #endregion

        #region Constructors
        public WidgetsNivoSliderController(IPermissionService permissionService, IProductService productService)
        {
            _permissionService = permissionService;
            _productService = productService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            {
                return AccessDeniedView();
            }
          

            var nivoSliderProducts = await _productService.GetProductsMarkedAsNewAsync();
            NewProductsSliderConfigurationModel model = new();
            model.Products = new List<NewProductModel>();
                  
            foreach (var p in nivoSliderProducts)
            {
                NewProductModel newProduct = new()
                {
                    Sku = p.Sku,
                    ProductName = p.Name,
                    IsNew = p.MarkAsNew,
                    StartDate = p.MarkAsNewStartDateTimeUtc ?? default,
                    EndDate = p.MarkAsNewEndDateTimeUtc ?? default
                };
                model.Products.Add(newProduct);
            }

                return View("~/Plugins/SIDS.NewProductsSlider/Views/Configure.cshtml",model);
        }
        //[HttpPost]
        //public async Task<IActionResult> Configure()
        //{
        //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
        //    {
        //        return AccessDeniedView();
        //    }

        
        //    return await Configure();
        //}
        #endregion
    }
}