using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions;
using Umbraco.Core;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class GlobalContentSingleObjectProcessorAttribute : DittoProcessorAttribute 
    {
        public string GlobalContentAlias { get; }

        public GlobalContentSingleObjectProcessorAttribute()
        {            
        }

        public GlobalContentSingleObjectProcessorAttribute(string globalContentalias)
        {
            GlobalContentAlias = globalContentalias;
        }

        public override object ProcessValue()
        {
            var alias = GlobalContentAlias.IsNullOrWhiteSpace() ? Context.PropertyDescriptor.Name.ToSafeAlias(true) : GlobalContentAlias;
            var type = Context.PropertyDescriptor.PropertyType;
            var content = UmbracoContext.Current.GetContentFromRoot(alias).FirstOrDefault();

            return content.As(type);
        }
    }
}