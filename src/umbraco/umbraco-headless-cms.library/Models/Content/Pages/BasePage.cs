using System;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Pages
{
    public class BasePage : BaseContent
    {
        public string Url { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [LayoutProcessor]
        public BaseContent Layout { get; set; }
    }
}