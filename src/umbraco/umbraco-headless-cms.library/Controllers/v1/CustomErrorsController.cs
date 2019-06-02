using System.Net;
using System.Web.Mvc;
using umbraco_headless_cms.library.Helpers;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class CustomErrorsController : UmbracoController
    {
        /// <summary>
        /// A generic NotFound error response.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            var response = new ErrorResponse
            {
                StatusCode = HttpStatusCode.NotFound,
                ErrorMessage = "Not able to find content"
            };

            return JsonResultHelper.GetJsonResult(response);
        }
    }

    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}