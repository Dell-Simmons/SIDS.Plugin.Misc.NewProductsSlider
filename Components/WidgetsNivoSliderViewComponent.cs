using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Seo;
using Nop.Web.Framework.Components;
using SIDS.Plugin.Misc.NewProductsSlider.Infrastructure.Cache;
using SIDS.Plugin.Misc.NewProductsSlider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIDS.Plugin.Misc.NewProductsSlider.Components;

[ViewComponent(Name = "WidgetsNivoSlider")]
public class WidgetsNivoSliderViewComponent : NopViewComponent
{
    #region Constants and Fields
    private readonly ILogger _logger;
    private readonly INopFileProvider _nopFileProvider;
    private readonly IPictureService _pictureService;
    private readonly IProductService _productService;
    private readonly ISettingService _settingService;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IStoreContext _storeContext;
    private readonly IUrlRecordService _urlRecordService;
    private readonly IWebHelper _webHelper;
    #endregion

    #region Constructors
    public WidgetsNivoSliderViewComponent(IStoreContext storeContext,
        IStaticCacheManager staticCacheManager,
        ISettingService settingService,
        IPictureService pictureService,
        IWebHelper webHelper,
        IProductService productService,
        IUrlRecordService urlRecordService,
        INopFileProvider nopFileProvider,
        ILogger logger)
    {
        _storeContext = storeContext;
        _staticCacheManager = staticCacheManager;
        _settingService = settingService;
        _pictureService = pictureService;
        _webHelper = webHelper;
        _productService = productService;
        _urlRecordService = urlRecordService;
        _nopFileProvider = nopFileProvider;
        _logger = logger;
    }
    #endregion

    #region Methods
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {

        var nivoSliderProducts = (await _productService.GetProductsMarkedAsNewAsync()).ToList();
        if (nivoSliderProducts == null)
        {
            return Content(string.Empty);
        }
      //  nivoSliderProducts = LimitCatalogNumberRepitition(nivoSliderProducts);

        var model = new RecentArrivalsPublicInfoModel();
        model.SliderPics = new List<SliderPicModel>();
        await _logger.InformationAsync(string.Format("in SIDS.NewProductsSlider.  There are {0} Slider Pics to show", nivoSliderProducts.Count));
       
        foreach (var p in nivoSliderProducts.Take(5))
        {
            SliderPicModel sliderPicModel = new SliderPicModel();

            List<Picture> productPictures = (List<Picture>)await _pictureService.GetPicturesByProductIdAsync(p.Id, 1);
            if (productPictures == null)
            {
                continue;
            }
            if (!productPictures.Any())
            {
                continue;
            }

            Picture defaultPic = productPictures.FirstOrDefault();
            sliderPicModel.PictureUrl = await GetPictureUrlAsync(defaultPic.Id);

            var seName = await _urlRecordService.GetSeNameAsync(p, 0, true, false);
            sliderPicModel.Link = _webHelper.GetStoreLocation() + seName;

            sliderPicModel.Text = p.Name;
            sliderPicModel.AltText = defaultPic.TitleAttribute;

            model.SliderPics.Add(sliderPicModel);
        }
        var goodPics = from m in model.SliderPics where (m.PictureUrl != String.Empty) select m;
        if (!goodPics.Any())
        {
            return Content(string.Empty);
        }

        return View("~/Plugins/SIDS.NewProductsSlider/Views/RecentArrivalsPublicInfo.cshtml", model);

    }

    private IList<Product> LimitCatalogNumberRepitition(IList<Product> nivoSliderProducts)
    {
        return nivoSliderProducts;
        // check if the new configuration model (not written yet) says to limit the numb of each cat num displayed
        // if (configmodel.MaxNumOfSameCatalogNumber => 0) for example
        // { remove all but the MaxNumOfSameCatalogNumber one of each catalog number }

        // try this:
        //List<Booking> yetAnotherList =
        //     list.GroupBy(row => row.TourOperator)
        //         .SelectMany(g => g.OrderBy(row => row.DepDate).Take(2))
        //         .ToList();
    }

    /// <returns>A task that represents the asynchronous operation</returns>
    protected async Task<string> GetPictureUrlAsync(int pictureId)
    {
        var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY,
            pictureId, _webHelper.IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            //little hack here. nulls aren't cacheable so set it to ""
            var url = await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? string.Empty;
            return url;
        });
    }
    #endregion
}