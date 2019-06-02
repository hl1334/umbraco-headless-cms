using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class SpecificNavigationProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public SpecificNavigationProcessorAttribute()
        {
        }

        public SpecificNavigationProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public override object ProcessValue()
        {
            if (Context.Content == null)
                return null;

            var nodes = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context).ToList();
            return nodes.FirstOrDefault();
        }
    }
}
