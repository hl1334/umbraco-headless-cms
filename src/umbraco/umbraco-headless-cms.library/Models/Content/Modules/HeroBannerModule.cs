using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class HeroBannerModule : BaseModule
    {
        [ImageContentProcessor("image")]
        public ImageContent Image { get; set; }

        public string Heading { get; set; }

        public string Paragraph { get; set; }

        [ExtendedLinkProcessor("button")]
        public ExtendedLink Button { get; set; }

        [HeroBannerListProcessor]
        public IEnumerable<HeroBannerListItem> List { get; set; }

        public bool InverseClass { get; set; }

        [HeroBannerContentAlignmentProcessor]
        public string ContentAlignment { get; set; }

        [HeroBannerLinkProcessor]
        public Link BannerLink { get; set; }
    }
}
