using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Authorization;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.Content;
using umbraco_headless_cms.library.Models.Content.Layouts;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class LayoutsController : RenderMvcController
    {
        private readonly string _cmsServiceVersion;

        public LayoutsController()
        {
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

            var content = model.As<Layout>();

            if (content.DocumentTypeAlias != "layout")
            {
                return new HttpNotFoundResult();
            }

            var result = JsonResultHelper.GetJsonResult(content);

            return result;
        }

        [BasicAuthorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        [HttpGet]
        public ActionResult LayoutsMap(RenderModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var publishedContent = model.As<IPublishedContent>();

            var content = GetLayoutsMap(publishedContent);

            var result = JsonResultHelper.GetJsonResult(content);

            return result;
        }

        /// <summary>
        /// Gets the total map of all layouts below the "Layouts folder" node.
        /// </summary>
        /// <param name="publishedContent"></param>
        /// <returns></returns>
        private IEnumerable<LayoutMapItem> GetLayoutsMap(IPublishedContent publishedContent)
        {
            var childLayouts = publishedContent.Children;

            foreach (var layout in childLayouts)
            {
                var item = layout.As<LayoutMapItem>();

                item.Link = $"/{_cmsServiceVersion}/layouts/{layout.Id}";

                yield return item;
            }
        }

    }
}
