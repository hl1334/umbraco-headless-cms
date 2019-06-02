using Our.Umbraco.Ditto;
using System.Collections.Generic;
using System.Linq;
using umbraco_headless_cms.library.Extensions;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class RightNavigationProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var rightNavigation = Context.Content.GetPropertyValue<IEnumerable<RelatedLink>>("rightNavigation");

            return rightNavigation?.Select(relatedLink => relatedLink.ToLink()).ToList();
        }
    }
}
