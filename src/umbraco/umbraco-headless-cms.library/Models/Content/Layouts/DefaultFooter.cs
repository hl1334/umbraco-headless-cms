using System.Collections.Generic;
using umbraco_headless_cms.library.Models.Footer;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class DefaultFooter : BaseFooter
    {
        [ExtendedLinkProcessor("primaryUspLinks")]
        public IEnumerable<ExtendedLink> PrimaryUspLinks { get; set; }

        [ExtendedLinkProcessor("secondaryUspLinks")]
        public IEnumerable<ExtendedLink> SecondaryUspLinks { get; set; }

        [FooterNavigationProcessor]
        public IEnumerable<FooterNavigationColumn> Navigation { get; set; }

        [FooterTextIconColumnsProcessor]
        public IEnumerable<FooterTextIconColumn> TextIconColumns { get; set; }

        public string CopyrightText { get; set; }

        [TermsLinksProcessor]
        public IEnumerable<Link> TermsLinks { get; set; }
    }
}
