using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models
{
    public class ExtendedLink
    {
        public Link Link { get; set; }
        public string Subtitle { get; set; }

        [LowercaseProcessor]
        public string IconId { get; set; }
    }
}
