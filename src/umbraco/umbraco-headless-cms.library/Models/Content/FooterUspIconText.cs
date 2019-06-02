using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class FooterUspIconText
    {
        [LowercaseProcessor]
        public string IconId { get; set; }

        public string Text { get; set; }
    }
}
