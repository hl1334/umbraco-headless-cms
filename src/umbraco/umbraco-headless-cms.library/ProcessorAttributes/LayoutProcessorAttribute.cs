using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models.Content;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class LayoutProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var layout = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("layout").As<BaseContent>().FirstOrDefault();

            return layout;
        }
    }
}