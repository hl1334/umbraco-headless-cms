using System.Collections.Generic;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Core.Models;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class SingleMultinodeTreepickerLinkProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public SingleMultinodeTreepickerLinkProcessorAttribute()
        {
        }

        public SingleMultinodeTreepickerLinkProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public override object ProcessValue()
        {
            if (Context.Content == null)
                return null;

            // Since it's a Multinode Treepicker it returns a IEnumerable even though it's only possible to pick one.
            var nodesInMultiTreePicker = _processorAttributeHelper.ResolveContent<IEnumerable<IPublishedContent>>(Alias, Context).ToList();
            var node = nodesInMultiTreePicker.FirstOrDefault();

            return node?.Url;
        }
    }
}
