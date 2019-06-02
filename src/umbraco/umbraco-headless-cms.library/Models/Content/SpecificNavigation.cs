using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class SpecificNavigation
    {
        [SingleMultinodeTreepickerLinkProcessor]
        public string NavigationRoot { get; set; }
        [ExtendedLinkProcessor]
        public IEnumerable<ExtendedLink> NavigationItems { get; set; }
    }
}