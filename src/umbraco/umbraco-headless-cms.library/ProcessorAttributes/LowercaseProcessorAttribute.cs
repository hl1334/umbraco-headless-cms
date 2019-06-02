using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    /// <summary>
    /// Ensures lowercase string value.
    /// </summary>
    public class LowercaseProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public LowercaseProcessorAttribute()
        {            
        }

        public LowercaseProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public LowercaseProcessorAttribute(IProcessorAttributeHelper processorAttributeHelper)
        {
            _processorAttributeHelper = processorAttributeHelper;
        }

        public override object ProcessValue()
        {
            var content = _processorAttributeHelper.ResolveContent<string>(Alias, Context);

            return content.ToLowerInvariant();
        }
    }
}