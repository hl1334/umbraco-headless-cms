using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules.MyAccount
{
    public class MyAccountOrdersModule : BaseModule
    {
        [SingleMultinodeTreepickerLinkProcessor]
        public string OrderDetailsPage { get; set; }
    }
}