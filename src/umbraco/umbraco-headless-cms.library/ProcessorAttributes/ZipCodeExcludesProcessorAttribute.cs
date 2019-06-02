using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class ZipCodeExcludesProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var nestedContentExcludedZipCodes = Context.Content?.GetPropertyValue<IEnumerable<IPublishedContent>>("excludedZipCodes");

            return nestedContentExcludedZipCodes?.Select(nestedContentItem => nestedContentItem.GetPropertyValue<string>("excludedZipCode")).ToList();
        }
    }
}
