using System.Collections.Generic;
using umbraco_headless_cms.library.Models.Footer;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class CheckoutFooter : BaseFooter
    {
        public IEnumerable<FooterUspIconText> UspStatements { get; set; }

        [FooterTextIconColumnsProcessor]
        public IEnumerable<FooterTextIconColumn> TextIconColumns { get; set; }

        public string CopyrightText { get; set; }

        [TermsLinksProcessor]
        public IEnumerable<Link> TermsLinks { get; set; }
    }
}
