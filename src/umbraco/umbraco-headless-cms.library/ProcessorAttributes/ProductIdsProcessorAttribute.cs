using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class ProductIdsProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public ProductIdsProcessorAttribute()
        {            
        }

        public ProductIdsProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public ProductIdsProcessorAttribute(IProcessorAttributeHelper processorAttributeHelper)
        {
            _processorAttributeHelper = processorAttributeHelper;
        }

        public override object ProcessValue()
        {
            var content = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context);

            return content?.Select(i => i.GetProperty("productId")).Select(p => p.Value as string).ToList() ?? new List<string>();
        }
    }
}