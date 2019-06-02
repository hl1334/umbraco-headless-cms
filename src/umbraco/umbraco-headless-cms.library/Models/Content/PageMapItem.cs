using System;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class PageMapItem : BaseContent
    {
        public string Url { get; set; }
	    public string UrlName { get; set; }
        [NullableBoolProcessor]
        public bool? HideFromSitemap { get; set; }
        [NullableBoolProcessor]
        public bool? IsReceiptPage { get; set; }
        [NullableBoolProcessor]
        public bool? IsLoginPage { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}