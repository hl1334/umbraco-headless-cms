using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class SeoProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var seo = new Seo
            {
                Title =
                    Context.Content.HasValue("seoTitle") ? Context.Content.GetPropertyValue<string>("seoTitle") : null,
                Description =
                    Context.Content.HasValue("seoDescription") ? Context.Content.GetPropertyValue<string>("seoDescription") : null
            };

            return seo;
        }
    }
}