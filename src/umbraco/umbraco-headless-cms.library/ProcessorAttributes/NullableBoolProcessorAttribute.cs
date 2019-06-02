using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class NullableBoolProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public NullableBoolProcessorAttribute()
        {            
        }

        public NullableBoolProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public NullableBoolProcessorAttribute(IProcessorAttributeHelper processorAttributeHelper)
        {
            _processorAttributeHelper = processorAttributeHelper;
        }

        public override object ProcessValue()
        {
            var content = _processorAttributeHelper.ResolveContent<bool>(Alias, Context);

            if (content)
            {
                return true;
            }
            return null;
        }
    }
}