using System;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class OccasionsMapItem : BaseContent
    {
        public string HybrisProductCategory { get; set; }

        [LowercaseProcessor]
        public string Icon { get; set; }

        [NullableDateTimeProcessor]
        public DateTime? Date { get; set; }

        [SingleMultinodeTreepickerLinkProcessor]
        public string ProductListPage { get; set; }
    }
}
