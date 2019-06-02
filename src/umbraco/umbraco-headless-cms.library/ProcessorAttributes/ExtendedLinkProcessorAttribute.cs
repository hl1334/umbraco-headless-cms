using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using umbraco_headless_cms.library.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class ExtendedLinkProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public ExtendedLinkProcessorAttribute()
        { }

        public ExtendedLinkProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public override object ProcessValue()
        {
            if (Context.Content == null)
                return null;

            var content = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context);

            return content?.Select(item => new ExtendedLink
            {
                Link = item.GetPropertyValue<IEnumerable<RelatedLink>>("links")?.FirstOrDefault().ToLink(),
                Subtitle = item.GetPropertyValue<string>("subtitle"),
                IconId = item.GetPropertyValue<string>("iconId").ToLowerInvariant()
            }).ToList();
        }
    }
}
