using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Authorization;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.Content;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class SettingsController : RenderMvcController
    {
        [BasicAuthorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        [HttpGet]
        public ActionResult SettingsMap(RenderModel model)
        {
            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            var publishedContent = model.As<SettingsMapItem>();

            var result = JsonResultHelper.GetJsonResult(publishedContent);

            return result;
        }
    }
}
