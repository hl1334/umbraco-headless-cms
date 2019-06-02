using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using umbraco_headless_cms.library.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class FooterNavigationProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var navigation = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("navigation");

            return navigation?.Select(item => new FooterNavigationColumn
            {
                Heading = item.GetPropertyValue<string>("heading"),
                FooterLinks = item.GetPropertyValue<IEnumerable<RelatedLink>>("footerLinks")?.Select(relatedLink => relatedLink.ToLink()).ToList()
            }).ToList();
        }
    }
}
