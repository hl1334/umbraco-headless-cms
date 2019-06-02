using Umbraco.Core;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.PropertyEditors.ValueConverters;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    public class RteMacroRenderingValueConverterEvent : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Remove the built-in RTE value converter since we have a custom one to handle responsive images.
            PropertyValueConvertersResolver.Current.RemoveType<RteMacroRenderingValueConverter>();
        }
    }
}