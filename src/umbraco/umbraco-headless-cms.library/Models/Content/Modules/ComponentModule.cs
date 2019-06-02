using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class ComponentModule : BaseModule
    {
        [ComponentProcessor]
        public BaseContent Component { get; set; }
    }
}