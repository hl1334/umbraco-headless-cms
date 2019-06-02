using System;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class NullableDateTimeProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public NullableDateTimeProcessorAttribute()
        {
        }

        public NullableDateTimeProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public override object ProcessValue()
        {
            if (Context.Content == null)
                return null;

            var dateTime = _processorAttributeHelper.ResolveContent<DateTime>(Alias, Context);

            if (dateTime == DateTime.MinValue)
            {
                return null;
            }
            return dateTime.Date;
        }
    }
}