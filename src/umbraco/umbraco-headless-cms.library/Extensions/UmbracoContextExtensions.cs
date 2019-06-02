using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.Extensions
{
    public static class UmbracoContextExtensions
    {
        /// <summary>
        /// Gets content from the root node.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="nodeAlias"></param>
        /// <returns></returns>
        public static IEnumerable<IPublishedContent> GetContentFromRoot(this UmbracoContext context, string nodeAlias)
        {
            var content = context.ContentCache.GetAtRoot().DescendantsOrSelf(nodeAlias);
            return content;
        }
    }
}