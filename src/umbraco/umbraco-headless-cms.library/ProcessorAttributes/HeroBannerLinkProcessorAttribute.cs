using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class HeroBannerLinkProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var link = Context.Content.GetPropertyValue<IEnumerable<RelatedLink>>("bannerlink");

            return link?.FirstOrDefault().ToLink();
        }
    }
}
