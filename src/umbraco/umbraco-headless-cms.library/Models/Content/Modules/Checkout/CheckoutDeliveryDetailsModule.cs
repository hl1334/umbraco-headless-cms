using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules.Checkout
{
    public class CheckoutDeliveryDetailsModule : BaseCheckoutModule
    {
        [SingleMultinodeTreepickerLinkProcessor]
        public string ReceiverEditPage { get; set; }

        [SingleMultinodeTreepickerLinkProcessor]
        public string SenderEditPage { get; set; }
    }
}