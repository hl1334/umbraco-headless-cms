using System;
using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class ExcludedDatesListProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public ExcludedDatesListProcessorAttribute()
        { }

        public ExcludedDatesListProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public override object ProcessValue()
        {
            if (Context.Content == null)
                return null;

            var content = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context);

            return content?.Select(nestedContentItem => nestedContentItem.GetPropertyValue<DateTime>("excludedDate")).ToList();
        }
    }
}
