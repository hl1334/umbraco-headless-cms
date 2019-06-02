using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Pages
{
    public class ResetPasswordPage : BasePage
    {
        public string Heading { get; set; }

        public string Description { get; set; }

        [SingleMultinodeTreepickerLinkProcessor]
        public string Redirect { get; set; }
    }
}