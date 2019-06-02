using System;
using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class ProductSliderModule : BaseModule
    {
        [SingleMultinodeTreepickerLinkProcessor]
        public string ProductDetailsPageLink { get; set; }

        public string Heading { get; set; }

        [NullableDateTimeProcessor]
        public DateTime? CountdownStartDate { get; set; }

        [SingleMultinodeTreepickerLinkProcessor]
        public string ShowAllButtonLink { get; set; }

        public string ShowAllButtonCaption { get; set; }

        public IEnumerable<ProductSliderTag> Tags { get; set; }

        [ProductIdsProcessor]
        public List<string> ProductIds { get; set; }
    }
}
