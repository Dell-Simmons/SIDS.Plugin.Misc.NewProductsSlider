using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using SIDS.Plugin.Misc.NewProductsSlider.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIDS.Plugin.Misc.NewProductsSlider
{
    /// <summary>
    /// Rename this file and change to the correct type
    /// </summary>
    public class NewProductsSliderPlugin : BasePlugin, IWidgetPlugin
    {
        #region Constants and Fields
        private readonly IWebHelper _webHelper;
        #endregion

        #region Public static properties
        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
        #endregion

        #region Constructors
        public NewProductsSliderPlugin(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsNivoSlider/Configure";
        }
        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsNivoSliderViewComponent);
        }
        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the widget zones
        /// </returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBeforeNews });
        }
        /// <summary>
        /// Install plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }
        /// <summary>
        /// Uninstall plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
        #endregion
    }
}

