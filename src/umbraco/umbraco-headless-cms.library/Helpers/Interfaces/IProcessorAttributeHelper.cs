using Our.Umbraco.Ditto;

namespace umbraco_headless_cms.library.Helpers.Interfaces
{
    public interface IProcessorAttributeHelper
    {
        T ResolveContent<T>(string alias, DittoProcessorContext context);
    }
}
