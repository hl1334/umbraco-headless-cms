using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class CardTextSuggestionsProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public CardTextSuggestionsProcessorAttribute()
        {            
        }

        public CardTextSuggestionsProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public CardTextSuggestionsProcessorAttribute(IProcessorAttributeHelper processorAttributeHelper)
        {
            _processorAttributeHelper = processorAttributeHelper;
        }
        public override object ProcessValue()
        {
            var content = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context);

            return content.Select(c => c.GetProperty("cardText")).Select(p => p.Value as string).ToList();
        }
    }
}