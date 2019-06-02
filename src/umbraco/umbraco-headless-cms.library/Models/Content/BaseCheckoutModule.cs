using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class BaseCheckoutModule : BaseModule
    {
        [NullableBoolProcessor]
        public bool? RightColumnPlacement { get; set; }
    }
}