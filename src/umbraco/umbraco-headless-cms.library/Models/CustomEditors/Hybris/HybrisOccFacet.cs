using System.Collections.Generic;

namespace umbraco_headless_cms.library.Models.CustomEditors.Hybris
{
    public class HybrisOccFacet
    {
        public bool Category { get; set; }
        public List<HybrisOccProductCategory> Values { get; set; }
    }
}
