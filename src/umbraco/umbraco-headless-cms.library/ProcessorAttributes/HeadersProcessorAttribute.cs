using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models.Content.Layouts;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class HeadersProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var headers = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("header");
            var first = headers?.FirstOrDefault();

            switch (first?.DocumentTypeAlias)
            {
                case "defaultHeader":
                    return first.As<DefaultHeader>();

                case "checkoutHeader":
                    return first.As<CheckoutHeader>();

                default:
                    return null;
            }
        }
    }
}
