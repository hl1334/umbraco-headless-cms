using Our.Umbraco.Ditto;
using Umbraco.Core;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class HeroBannerContentAlignmentProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var contentAlignment = Context.Content.GetPropertyValue<string>("contentAlignment");

            return contentAlignment.IsNullOrWhiteSpace() ? "Left" : contentAlignment;
        }
    }
}
