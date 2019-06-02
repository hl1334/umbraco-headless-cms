using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models
{
    public class HeroBannerListItem
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        [LowercaseProcessor]
        public string IconId { get; set; }
    }
}
