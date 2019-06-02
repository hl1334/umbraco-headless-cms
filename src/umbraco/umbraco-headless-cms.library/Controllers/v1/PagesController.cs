using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Authorization;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.Content;
using umbraco_headless_cms.library.Models.Content.Pages;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class PagesController : RenderMvcController
    {
        private readonly string[] _productDetailsPageDynamicParams;
        private readonly string _cmsServiceVersion;

        public PagesController()
        {
            _productDetailsPageDynamicParams = ConfigurationManager.AppSettings["productDetailsPageDynamicParams"].Split(',');
            _cmsServiceVersion = ConfigurationManager.AppSettings["cmsServiceVersion"];
        }

        [BasicAuthorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        [HttpGet]
        public override ActionResult Index(RenderModel model)
        {
            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            var publishedContent = model.As<IPublishedContent>();

            var content = GetPageContent(publishedContent);

            if (content == null)
            {
                return new HttpNotFoundResult();
            }

            var result = JsonResultHelper.GetJsonResult(content);

            return result;
        }

        [BasicAuthorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        [HttpGet]
        public ActionResult PagesMap(RenderModel model)
        {
            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            var publishedContent = model.As<IPublishedContent>();

            var checkoutPages = !string.IsNullOrWhiteSpace(Request.QueryString["checkout"]);

            var content = GetPagesMap(publishedContent, checkoutPages);

            var result = JsonResultHelper.GetJsonResult(content);

            return result;
        }

        private static BasePage GetPageContent(IPublishedContent publishedContent)
        {
            switch (publishedContent.DocumentTypeAlias)
            {
                case "home":
                    return publishedContent.As<ContentPage>();

                case "contentPage":
                    return publishedContent.As<ContentPage>();

                case "checkoutPage":
                    return publishedContent.As<CheckoutPage>();

                case "productDetailsPage":
	                return publishedContent.As<ProductDetailsPage>();

                case "productListPage":
                    return publishedContent.As<ProductListPage>();

                case "loginPage":
                    return publishedContent.As<LoginPage>();

                case "notFoundErrorPage":
                    return publishedContent.As<NotFoundErrorPage>();

                case "myAccountPage":
                    return publishedContent.As<MyAccountPage>();

                case "orderDetailsPage":
                    return publishedContent.As<OrderDetailsPage>();

                case "resetPasswordPage":
                    return publishedContent.As<ResetPasswordPage>();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the total map of all pages below the "Pages folder" node.
        /// </summary>
        /// <param name="publishedContent"></param>
        /// <param name="checkoutPagesOnly">If true then only return checkout page types</param>
        /// <returns></returns>
        private IEnumerable<PageMapItem> GetPagesMap(IPublishedContent publishedContent, bool checkoutPagesOnly)
        {
            var childPages = publishedContent.Children;

            foreach (var page in childPages)
            {
                if (checkoutPagesOnly && page.DocumentTypeAlias != "checkoutFolder" && page.DocumentTypeAlias != "checkoutPage")
                {
                    continue;
                }

                var item = page.As<PageMapItem>();
                item.UrlName = page.Name.ToUrlSegment();

                // Special handling of specific page types (ie. dynamic pages).
                switch (page.DocumentTypeAlias)
                {
                    case "productDetailsPage":
                        foreach (var param in _productDetailsPageDynamicParams)
                        {
                            item.Url += $":{param}/";
                        }
                        break;
                    case "orderDetailsPage":
                        // TODO: Variable parameter 'id' is hardcoded for now.
                        item.Url += ":id/";
                        break;
                }

                item.Link = $"/{_cmsServiceVersion}/pages/{page.Id}";

                yield return item;

                // Resolve sub pages to the list of pages.
                var subItems = GetPagesMap(page, checkoutPagesOnly);
                foreach (var subPageItem in subItems)
                {
                    yield return subPageItem;
                }
            }
        }

    }
}