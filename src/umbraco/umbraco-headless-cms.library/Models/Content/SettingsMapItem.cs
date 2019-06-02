using System;
using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class SettingsMapItem
    {
        public string DeliveryDeadline { get; set; }
        [ZipCodeExcludesProcessor]
        public IEnumerable<string> ExcludedZipCodes { get; set; }
        [ExcludedDatesListProcessor]
        public IEnumerable<DateTime> NonOrderProcessingDates { get; set; }
        [ExcludedDatesListProcessor]
        public IEnumerable<DateTime> WhitelistedOrderProcessingDates { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string ReceiptPage { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string CancelPage { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string NotfoundPage { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string LoginPage { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string CheckoutLoginPage { get; set; }
        [SingleMultinodeTreepickerLinkProcessor]
        public string ProductDetailsPage { get; set; }

        public string DefaultTitle { get; set; }
        public string DefaultDescription { get; set; }
        public string TitleTemplate { get; set; }

        public bool CloseShop { get; set; }
        public bool ShowWarning { get; set; }
        public string WarningTitle { get; set; }
        public string WarningText { get; set; }
    }
}
