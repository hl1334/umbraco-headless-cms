using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class FooterIconsProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var icons = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("icons");

            return icons?.Select(item => new FooterIcons
            {
                Heading = item.GetPropertyValue<string>("heading"),
                Elements = item.GetPropertyValue<IEnumerable<IPublishedContent>>("elements")?.As<FooterIconElement>().ToList()
            }).ToList();
        }
    }
}
