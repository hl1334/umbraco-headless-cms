using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models.Content.Layouts;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class FootersProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var footers = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("footer");
            var first = footers?.FirstOrDefault();

            switch (first?.DocumentTypeAlias)
            {
                case "defaultFooter":
                    return first.As<DefaultFooter>();

                case "checkoutFooter":
                    return first.As<CheckoutFooter>();

                default:
                    return null;
            }
        }
    }
}
