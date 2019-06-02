using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Pages
{
    public class CheckoutPage : BasePage
    {
        public bool IsReceiptPage { get; set; }
        public bool IsLoginPage { get; set; }

        [ModulesProcessor]
        public IEnumerable<BaseModule> Modules { get; set; }
    }
}