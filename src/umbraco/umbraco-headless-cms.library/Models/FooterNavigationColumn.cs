using System.Collections.Generic;

namespace umbraco_headless_cms.library.Models
{
    public class FooterNavigationColumn
    {
        public string Heading { get; set; }
        public IEnumerable<Link> FooterLinks { get; set; }
    }
}