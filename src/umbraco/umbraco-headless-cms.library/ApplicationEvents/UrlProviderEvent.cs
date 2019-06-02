using umbraco_headless_cms.library.UrlProvider;
using Umbraco.Core;
using Umbraco.Web.Routing;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    public class UrlProviderEvent : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UrlProviderResolver.Current.InsertTypeBefore<DefaultUrlProvider, CustomUrlProvider>();
        }
    }
}