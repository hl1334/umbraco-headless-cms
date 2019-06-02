using System;
using System.Configuration;
using System.Globalization;
using System.Web.Routing;
using umbraco_headless_cms.library.Extensions;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Routing
{
    // TODO: There is some logic here w.r.t. preview content, so if possible we should unit test it.
    // Note: This would likely imply some refactoring of the preview logic code (move to some helper class, that can be tested).

    public class UmbracoVirtualNodeByIdRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        private readonly string _previewToken;

        public UmbracoVirtualNodeByIdRouteHandler() : this(ConfigurationManager.AppSettings["previewToken"])
        {            
        }

        public UmbracoVirtualNodeByIdRouteHandler(string previewToken)
        {
            if (previewToken == null) throw new ArgumentNullException(nameof(previewToken));
            _previewToken = previewToken;
        }

        protected sealed override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            var idValue = requestContext.RouteData.Values["id"].ToString();

            int id;
            var success = int.TryParse(idValue, out id);
            if (!success)
            {
                return null;
            }

            var previewQuery = requestContext.HttpContext.Request.QueryString["preview"];
            var dateQuery = requestContext.HttpContext.Request.QueryString["date"];
            var previewDate = DateTime.MinValue;
            var successPreviewDateParse = false;
            if (!dateQuery.IsNullOrWhiteSpace())
            {
                successPreviewDateParse = DateTime.TryParse(dateQuery, CultureInfo.InvariantCulture, DateTimeStyles.None, out previewDate);                
            }

            // If not preview query get only published content.
            if (previewQuery.IsNullOrWhiteSpace()) return umbracoContext.ContentCache.GetById(false, id);

            // Verify request preview token.
            var requestPreviewToken = requestContext.HttpContext.Request.QueryString["token"];
            if (requestPreviewToken.IsNullOrWhiteSpace() || requestPreviewToken != _previewToken)
            {
                return null;
            }

            // Get both published and saved content using contentservice.
            var icontent = ApplicationContext.Current.Services.ContentService.GetById(id);
            var previewContent = icontent.ToPublishedContent();

            // If there is a date query but we cannot parse the date successfully we just return preview content.
            if (dateQuery.IsNullOrWhiteSpace() || !successPreviewDateParse) return previewContent;

            // If preview and date query we must filter on ReleaseDate.
            // If content status is "unpublished", then there is no set releasedate, so we just return preview content.
            if (icontent.Status == ContentStatus.Unpublished)
            {
                return previewContent;
            }
            if (icontent.Status == ContentStatus.AwaitingRelease && icontent.ReleaseDate > previewDate)
            {
                // If we are not at release yet, then we should return the published (none-preview) content.
                return umbracoContext.ContentCache.GetById(false, id);
            }

            return previewContent;
        }
    }
}
