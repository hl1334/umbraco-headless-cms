using System.Collections.Generic;

namespace umbraco_headless_cms.library.Models
{
    public class FooterIcons
    {
        public string Heading { get; set; }
        public IEnumerable<FooterIconElement> Elements { get; set; }
    }
}