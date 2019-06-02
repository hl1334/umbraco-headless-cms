using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class ButtonModule : BaseModule
    {
        [SingleLinkProcessor]
        public Link Button { get; set; }
    }
}
