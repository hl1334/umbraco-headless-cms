using System;
using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class DefaultHeader : BaseHeader
    {
        [NullableDateTimeProcessor]
        public DateTime? CtaCountdownStartDate { get; set; }

        public string CtaCountdownText { get; set; }

        [SingleLinkProcessor]
        public Link CtaLink { get; set; }

        [RightNavigationProcessor]
        public IEnumerable<Link> RightNavigation { get; set; }

        [ExtendedLinkProcessor("mainNavigation")]
        public IEnumerable<ExtendedLink> MainNavigation { get; set; }

        [SubNavigationProcessor]
        public IEnumerable<Link> SubNavigation { get; set; }
    }
}
