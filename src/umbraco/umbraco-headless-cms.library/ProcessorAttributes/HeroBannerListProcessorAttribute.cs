using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class HeroBannerListProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var list = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("list");

            return list?.Select(item => item.As<HeroBannerListItem>()).ToList();
        }
    }
}
