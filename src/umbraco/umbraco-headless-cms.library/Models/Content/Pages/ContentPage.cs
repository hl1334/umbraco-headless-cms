using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Pages
{
    public class ContentPage : BasePage
    {
        public string Heading { get; set; }

        [ImageContentProcessor]
        public ImageContent TopBannerImage { get; set; }

        [SeoProcessor]
        public Seo Seo { get; set; }

        public bool HideFromSitemap { get; set; }

        [ModulesProcessor]
        public IEnumerable<BaseModule> Modules { get; set; }

        [SubItemsProcessor]
        public IEnumerable<BaseContent> SubItems { get; set; }

        public string SeoText { get; set; }
    }
}
