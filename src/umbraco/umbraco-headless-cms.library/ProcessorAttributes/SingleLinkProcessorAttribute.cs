using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class SingleLinkProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public SingleLinkProcessorAttribute()
        {            
        }

        public SingleLinkProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public SingleLinkProcessorAttribute(IProcessorAttributeHelper processorAttributeHelper)
        {
            _processorAttributeHelper = processorAttributeHelper;
        }

        public override object ProcessValue()
        {
            var content = _processorAttributeHelper.ResolveContent<IEnumerable<RelatedLink>>(Alias, Context);

            return content?.FirstOrDefault().ToLink();
        }
    }
}