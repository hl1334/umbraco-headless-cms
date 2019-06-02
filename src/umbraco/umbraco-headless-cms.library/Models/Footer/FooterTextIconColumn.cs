using System.Collections.Generic;

namespace umbraco_headless_cms.library.Models.Footer
{
    public class FooterTextIconColumn
    {
        public string Headline { get; set; }
        public IEnumerable<FooterTextIconColumnElement> Elements { get; set; }
    }
}
