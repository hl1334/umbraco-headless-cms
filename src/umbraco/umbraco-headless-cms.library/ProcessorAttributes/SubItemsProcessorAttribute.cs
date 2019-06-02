using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models.Content;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    /// <summary>
    /// Resolves subitems (children) as BaseContent.
    /// This processor is strictly for use for page document-types.
    /// </summary>
    public class SubItemsProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var subItems = Context.Content.Children;

            return subItems?.Select(item => new BaseContent
            {
                Id = item.Id,
                Name = item.Name,
                DocumentTypeAlias = item.DocumentTypeAlias
            });
        }
    }
}