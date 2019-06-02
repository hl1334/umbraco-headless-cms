using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class ImageItem
    {
        [ImageContentProcessor]
        public ImageContent Image { get; set; }

        [SingleLinkProcessor]
        public Link Link { get; set; }
    }
}