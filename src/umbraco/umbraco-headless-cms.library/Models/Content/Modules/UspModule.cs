using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class UspModule : BaseModule
    {
        [ExtendedLinkProcessor("uspLinks")]
        public IEnumerable<ExtendedLink> UspLinks { get; set; }
    }
}