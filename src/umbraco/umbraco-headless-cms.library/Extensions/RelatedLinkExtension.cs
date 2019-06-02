using umbraco_headless_cms.library.Models;
using Umbraco.Web.Models;

namespace umbraco_headless_cms.library.Extensions
{
    public static class RelatedLinkExtension
    {
        public static Link ToLink(this RelatedLink content)
        {
            return new Link()
            {
                Title = content.Caption,
                Url = content.Link == string.Empty || content.Link.ToLowerInvariant() == "http://" ? null : content.Link,
                NewWindow = content.NewWindow,
                IsInternal = content.IsInternal
            };
        }
    }
}
