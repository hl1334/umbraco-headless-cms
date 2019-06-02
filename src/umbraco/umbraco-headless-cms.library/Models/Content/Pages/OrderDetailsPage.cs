using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Pages
{
    public class OrderDetailsPage : BasePage
    {
        public string Heading { get; set; }

        [SpecificNavigationProcessor]
        public SpecificNavigation Navigation { get; set; }

        [ModulesProcessor]
        public IEnumerable<BaseModule> Modules { get; set; }
    }
}