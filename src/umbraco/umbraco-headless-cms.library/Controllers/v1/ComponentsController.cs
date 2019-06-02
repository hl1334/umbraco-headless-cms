using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Authorization;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.Content;
using umbraco_headless_cms.library.Models.Content.Modules;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class ComponentsController : RenderMvcController
    {
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

            var content = ResolveComponentContent(publishedContent);

            if (content == null)
            {
                return new HttpNotFoundResult();
            }

            var result = JsonResultHelper.GetJsonResult(content);

            return result;
        }

        private static BaseModule ResolveComponentContent(IPublishedContent publishedContent)
        {
            switch (publishedContent.DocumentTypeAlias)
            {
                case "productSliderModule":
                    return publishedContent.As<ProductSliderModule>();

                case "occasionSliderModule":
                    return publishedContent.As<OccasionSliderModule>();

                case "uspModule":
                    return publishedContent.As<UspModule>();

                case "textboxModule":
                    return publishedContent.As<TextboxModule>();

                case "imageboxModule":
                    return publishedContent.As<ImageboxModule>();

                case "heroBannerModule":
                    return publishedContent.As<HeroBannerModule>();

                case "videoModule":
                    return publishedContent.As<VideoModule>();

                default:
                    return null;
            }
        }
    }
}