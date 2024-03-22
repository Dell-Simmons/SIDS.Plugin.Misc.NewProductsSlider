using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using SIDS.Plugin.Misc.NewProductsSlider.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SIDS.Plugin.Misc.NewProductsSlider.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsNivoSliderController : BasePluginController
    {
        #region Constants and Fields
        private readonly IPermissionService _permissionService;
        #endregion

        #region Constructors
        public WidgetsNivoSliderController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            {
                return AccessDeniedView();
            }


            return View("~/Plugins/SIDS.NewProductsSlider/Views/Configure.cshtml");
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