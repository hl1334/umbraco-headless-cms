using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules.Checkout
{
    public class CheckoutProductVariantModule : BaseCheckoutModule
    {
        [SingleLinkProcessor]
        public Link ProductListingPageLink { get; set; }
    }
}