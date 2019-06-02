using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class CheckoutHeader : BaseHeader
    {
        public string Title { get; set; }

        [LowercaseProcessor]
        public string IconId { get; set; }

        public string Text { get; set; }
    }
}
