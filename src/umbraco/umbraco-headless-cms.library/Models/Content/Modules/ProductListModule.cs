using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class ProductListModule : BaseModule
    {
        public string HybrisProductCategory { get; set; }

        [SingleMultinodeTreepickerLinkProcessor]
        public string ProductDetailsPage { get; set; }
    }
}