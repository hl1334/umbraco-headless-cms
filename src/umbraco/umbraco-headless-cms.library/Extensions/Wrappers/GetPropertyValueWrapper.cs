using System.Diagnostics.CodeAnalysis;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.Extensions.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class GetPropertyValueWrapper : IGetPropertyValueWrapper
    {
        public T GetPropertyValue<T>(IPublishedContent publishedContent, string propertyAlias)
        {
            return publishedContent.GetPropertyValue<T>(propertyAlias);
        }
    }
}