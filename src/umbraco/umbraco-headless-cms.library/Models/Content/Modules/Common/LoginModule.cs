using umbraco_headless_cms.library.Models.Content.GlobalContent;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules.Common
{
    public class LoginModule : BaseModule
    {
        [GlobalContentSingleObjectProcessor]
        public LoginContent LoginContent { get; set; }
    }
}