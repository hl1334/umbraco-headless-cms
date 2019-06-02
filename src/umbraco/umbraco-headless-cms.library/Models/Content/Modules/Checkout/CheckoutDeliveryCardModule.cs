using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules.Checkout
{
    public class CheckoutDeliveryCardModule : BaseCheckoutModule
    {
        public string Heading { get; set; }

        [CardTextSuggestionCategoriesProcessor]
        public IEnumerable<CardTextSuggestionCategory> CardTextSuggestionCategories { get; set; }
    }
}