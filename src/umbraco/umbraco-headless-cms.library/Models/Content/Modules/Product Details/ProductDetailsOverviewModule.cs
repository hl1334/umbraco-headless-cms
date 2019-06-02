using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules.Product_Details
{
    public class ProductDetailsOverviewModule : BaseModule
    {
        public string ImageText { get; set; }
        public string LinkToSpecificationText { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string BuyNowButtonLink { get; set; }
        public string DeliveryInformation { get; set; }
        public string ProductDeliveryTimeBeforeSpecificTime { get; set; }
        public string ProductDeliveryTimeAfterSpecificTime { get; set; }
        public string ProductDeliveryTimeFutureProduct { get; set; }
        public string ProductAvailabilityTextAvailableProduct { get; set; }
        public string ProductAvailabilityTextFutureProduct { get; set; }
        [QuantityListProcessor]
        public List<int?> QuantityList { get; set; }
    }
}
