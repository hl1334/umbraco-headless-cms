using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class ImageboxModule : BaseModule
    {
        [ImageContentProcessor("image")]
        public ImageContent Image { get; set; }
        public string Caption { get; set; }
    }
}