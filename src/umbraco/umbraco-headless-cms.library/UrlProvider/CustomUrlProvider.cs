using System;
using Umbraco.Core.Configuration;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace umbraco_headless_cms.library.UrlProvider
{
    public class CustomUrlProvider : DefaultUrlProvider
    {
        public CustomUrlProvider() : base(UmbracoConfig.For.UmbracoSettings().RequestHandler)
        { }

        public override string GetUrl(UmbracoContext umbracoContext, int id, Uri current, UrlProviderMode mode)
        {
            var node = umbracoContext.ContentCache.GetById(id);

            if (node == null)
            {
                return string.Empty;
            }

            return node.DocumentTypeAlias == "home" ? "/" : base.GetUrl(umbracoContext, id, current, mode);
        }
    }
}