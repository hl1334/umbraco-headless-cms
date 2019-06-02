using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class SubNavigationProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var subNavigation = Context.Content.GetPropertyValue<IEnumerable<RelatedLink>>("subNavigation");

            return subNavigation?.Select(relatedLink => relatedLink.ToLink()).ToList();
        }
    }
}
