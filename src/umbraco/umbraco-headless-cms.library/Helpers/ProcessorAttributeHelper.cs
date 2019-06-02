using System.Diagnostics.CodeAnalysis;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers.Interfaces;
using Umbraco.Web;

namespace umbraco_headless_cms.library.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ProcessorAttributeHelper : IProcessorAttributeHelper
    {
        public T ResolveContent<T>(string alias, DittoProcessorContext context)
        {
            var umbracoAlias = alias ?? context.PropertyDescriptor.Name;

            return context.Content.GetPropertyValue<T>(umbracoAlias);
        }
    }
}
