using Umbraco.Core.Models;

namespace umbraco_headless_cms.library.Extensions.Wrappers
{
    public interface IGetPropertyValueWrapper
    {
        T GetPropertyValue<T>(IPublishedContent publishedContent, string propertyAlias);
    }
}