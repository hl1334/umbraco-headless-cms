using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Authorization;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.Content;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class OccasionsController : RenderMvcController
    {
        [BasicAuthorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        [HttpGet]
        public ActionResult OccasionsMap(RenderModel model)
        {
            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            var publishedContent = model.As<IPublishedContent>();

            var content = GetOccasionsMap(publishedContent);

            var result = JsonResultHelper.GetJsonResult(content);

            return result;
        }

        /// <summary>
        /// Gets the total map of all occasions below the "Occasions folder" node.
        /// </summary>
        /// <param name="publishedContent"></param>
        /// <returns></returns>
        private static IEnumerable<OccasionsMapItem> GetOccasionsMap(IPublishedContent publishedContent)
        {
            var childPages = publishedContent.Children;

            foreach (var page in childPages)
            {
                var item = page.As<OccasionsMapItem>();

                yield return item;
            }
        }
    }
}
