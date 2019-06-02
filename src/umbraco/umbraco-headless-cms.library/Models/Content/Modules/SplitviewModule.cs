using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class SplitviewModule : BaseModule
    {
        [ModulesProcessor]
        public IEnumerable<BaseModule> Modules { get; set; }
    }
}