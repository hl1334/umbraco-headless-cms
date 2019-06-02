using Umbraco.Core;
using Umbraco.Web.Editors;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    /// <inheritdoc />
    /// <summary>
    /// Disables the preview button in back-office for all content nodes.
    /// </summary>
    public class DisablePreviewEvent : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            EditorModelEventManager.SendingContentModel += (sender, e) =>
            {
                var contentModel = e.Model;
                if (contentModel != null)
                {
                    contentModel.AllowPreview = false;
                }
            };
        }
    }
}