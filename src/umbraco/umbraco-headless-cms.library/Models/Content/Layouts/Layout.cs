using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class Layout : BaseContent
    {
        [HeadersProcessor]
        public BaseHeader Header { get; set; }

        [FootersProcessor]
        public BaseFooter Footer { get; set; }
    }
}
