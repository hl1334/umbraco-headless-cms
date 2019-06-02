using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    // TODO: We have disabled all the event triggers below since we are having problems with node trashing in combination with cancelling the unpublish event; i.e. the content cache is not updated correctly when trashing.
    // TODO: Possibly the handling of events like this actually introduces more problems than it solves, so consider removing this.
    // TODO: Maybe we should prevent deletion of the top-folders and maybe actually some child node types.

    public class ContentServiceEvents : ApplicationEventHandler
    {
        private const string RootPageAlias = "home";
        private const string PagesFolderAlias = "pagesFolder";
        private const string ComponentsFolderAlias = "componentsFolder";
        private const string LayoutsFolderAlias = "layoutsFolder";
        private const string HeadersFolderAlias = "headersFolder";
        private const string FootersFolderAlias = "footersFolder";
        private const string ProductSliderTagsFolderAlias = "productSliderTagsFolder";
        private const string OccasionsFolderAlias = "occasionsFolder";
        private const string SettingsAlias = "settings";
        private const string GlobalContentFolderAlias = "globalContentFolder";
        private const string DeliveryCardSuggestionsFolderAlias = "deliveryCardSuggestionsFolder";
        private const string NavigationFolderAlias = "navigationFolder";
        
        private static IList<string> _nodesForcedPublished;
        private static IList<string> _childNodesForcedPublished;

        public ContentServiceEvents()
        {
            _nodesForcedPublished = new List<string>
            {
                RootPageAlias,
                PagesFolderAlias,
                ComponentsFolderAlias,
                LayoutsFolderAlias,
                HeadersFolderAlias,
                FootersFolderAlias,
                ProductSliderTagsFolderAlias,
                OccasionsFolderAlias,
                SettingsAlias,
                GlobalContentFolderAlias,
                DeliveryCardSuggestionsFolderAlias,
                NavigationFolderAlias
            };

            _childNodesForcedPublished = new List<string>
            {
                ComponentsFolderAlias,
                LayoutsFolderAlias,
                HeadersFolderAlias,
                FootersFolderAlias,
                ProductSliderTagsFolderAlias,
                OccasionsFolderAlias,
                GlobalContentFolderAlias,
                NavigationFolderAlias
            };
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //ContentService.Saved += ContentService_Saved;
            //ContentService.UnPublishing += ContentService_UnPublishing;
            //ContentService.Trashed += ContentService_Trashed;
        }

        private void ContentService_Trashed(IContentService sender, MoveEventArgs<IContent> e)
        {
            // Refresh xml cache when trashing specific node types.
            // Note: Since we are preventing unpublishing (but not trashing) of specific node types we need to make sure that 
            // the xml cache is refreshed

            foreach (var moveEventInfo in e.MoveInfoCollection)
            {
                // If this is one of the specified node types trash it and refresh the xml cache.
                // TODO: Problem is the node no longer has a parent node (parent now is the recycle bin), so we cannot use the current MustEnsurePublishedState method (since it checks for parent node).
                if (MustEnsurePublishedState(moveEventInfo.Entity))
                {
                    umbraco.library.RefreshContent();
                }
            }
        }

        private static void ContentService_UnPublishing(Umbraco.Core.Publishing.IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            // Prevent unpublish of specific node types.
            foreach (var node in e.PublishedEntities)
            {
                if (MustEnsurePublishedState(node))
                    e.Cancel = true;
            }
        }

        private static void ContentService_Saved(IContentService sender, SaveEventArgs<IContent> e)
        {
            // Ensure published state for specific node types.
            foreach (var node in e.SavedEntities)
            {
                if (node.Published) continue;

                if (MustEnsurePublishedState(node))
                {
                    sender.Publish(node);
                    e.Messages.Add(new EventMessage("Info", "The content was also published", EventMessageType.Info));
                }
            }
        }

        /// <summary>
        /// Returns either true/false depending on if the node NEEDS to have published state.
        /// </summary>
        /// <param name="node">
        /// The node to check if its allowed to be unpublished or saved.
        /// </param>
        private static bool MustEnsurePublishedState(IContent node)
        {
            // If the node matches one of the aliasses in the '_nodesForcedPublished' list, dont allow it to be unpublished or saved.
            if (_nodesForcedPublished.Contains(node.ContentType.Alias))
            {
                return true;
            }

            var nodeParent = node.Parent();
            if (nodeParent != null)
            {
                // If the node is a child to one of the aliasses in '_childNodesForcedPublished', don't allow it to be unpublished or saved.
                if (_childNodesForcedPublished.Contains(nodeParent.ContentType.Alias))
                {
                    return true;
                }
            }

            return false;
        }
    }
}