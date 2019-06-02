using umbraco_headless_cms.library.Models.Footer;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models
{
    public class FooterIconElement : FooterTextIconColumnElement
    {
        [LowercaseProcessor]
        public string IconId { get; set; }

        [FooterIconLinkProcessor]
        public Link Link { get; set; }
    }
}