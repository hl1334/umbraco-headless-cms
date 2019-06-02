using System;
using System.Configuration;
using Umbraco.Core;
using Umbraco.Web.Trees;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    public class RegisterEvents : ApplicationEventHandler
    {
        private readonly string _websiteRootUrl;
        private readonly string _previewToken;

        public RegisterEvents() : this(
            ConfigurationManager.AppSettings["previewWebsiteRootUrl"],
            ConfigurationManager.AppSettings["previewToken"])
        {           
        }

        public RegisterEvents(string websiteRootUrl, string previewToken)
        {
            if (websiteRootUrl == null) throw new ArgumentNullException(nameof(websiteRootUrl));
            if (previewToken == null) throw new ArgumentNullException(nameof(previewToken));
            _websiteRootUrl = websiteRootUrl;
            _previewToken = previewToken;
        }

        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            // Register custom menu item in the media tree
            TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
        }

        private void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            if (sender.TreeAlias != "content") return;

            var nodeId = e.NodeId.ParseInto<int>();
            var contentItem = ApplicationContext.Current.Services.ContentService.GetById(nodeId);
            if (contentItem == null || (contentItem.ContentType.Alias != "home" &&
                                        contentItem.ContentType.Alias != "contentPage" &&
                                        contentItem.ContentType.Alias != "productDetailsPage")) return;

            var previewMenuItem =
                new Umbraco.Web.Models.Trees.MenuItem("previewPageContent", "Preview")
                {
                    Icon = "display",
                    SeperatorBefore = true
                };
                    
            previewMenuItem.AdditionalData.Add("actionView", "/App_Plugins/PreviewContextAction/previewcontextaction.html");
            previewMenuItem.AdditionalData.Add("pageId", contentItem.Id);
            previewMenuItem.AdditionalData.Add("websiteRootUrl", _websiteRootUrl);
            previewMenuItem.AdditionalData.Add("previewToken", _previewToken);
            e.Menu.Items.Insert(3, previewMenuItem);
        }
    }
}