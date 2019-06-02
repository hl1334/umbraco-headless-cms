using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class QuantityListProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public QuantityListProcessorAttribute()
        {            
        }

        public QuantityListProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public QuantityListProcessorAttribute(IProcessorAttributeHelper processorAttributeHelper)
        {
            _processorAttributeHelper = processorAttributeHelper;
        }

        public override object ProcessValue()
        {
            var content = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context);

            return content?.Select(i => i.GetProperty("quantity")).Select(q => q.Value as int?).ToList() ?? new List<int?>();
        }
    }
}