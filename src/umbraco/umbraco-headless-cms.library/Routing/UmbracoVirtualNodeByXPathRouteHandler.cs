using System;
using System.Linq;
using System.Web.Routing;
using Umbraco.Core.Models;
using Umbraco.Core.Xml;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Routing
{
    public class UmbracoVirtualNodeByXPathRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        private readonly string _xpath;

        public UmbracoVirtualNodeByXPathRouteHandler(string xpath)
        {
            if (xpath == null) throw new ArgumentNullException(nameof(xpath));

            _xpath = xpath;
        }

        protected sealed override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {

            var xpathVariables = from routeValue in requestContext.RouteData.Values
                                 select new XPathVariable(routeValue.Key, routeValue.Value.ToString());

            var content = umbracoContext.ContentCache.GetSingleByXPath(false, _xpath, xpathVariables.ToArray());

            return content;
        }
    }
}
