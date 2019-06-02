using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class TermsLinksProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var termsLinks = Context.Content.GetPropertyValue<IEnumerable<RelatedLink>>("termsLinks");

            return termsLinks?.Select(item => item.ToLink());
        }
    }
}
