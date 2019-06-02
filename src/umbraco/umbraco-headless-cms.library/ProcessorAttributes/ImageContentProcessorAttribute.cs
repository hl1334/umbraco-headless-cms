using ds.umbraco.imgix;
using ds.umbraco.imgix.Extensions.Images;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Helpers.Interfaces;
using umbraco_headless_cms.library.Models.Content;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class ImageContentProcessorAttribute : DittoProcessorAttribute
    {
        private readonly IProcessorAttributeHelper _processorAttributeHelper = new ProcessorAttributeHelper();

        public string Alias { get; }

        public ImageContentProcessorAttribute()
        { }

        public ImageContentProcessorAttribute(string alias)
        {
            Alias = alias;
        }

        public override object ProcessValue()
        {
            if (Context.Content == null)
                return null;

            var content = _processorAttributeHelper.ResolveContent<IPublishedContent>(Alias, Context);

            return content == null ? null : new ImageContent
            {
                Name = content.Name,
                Url = content.GetImgixImage(),
                FocalPointX = content.GetCropDataSet().FocalPoint?.Left,
                FocalPointY = content.GetCropDataSet().FocalPoint?.Top,
                AlternativeText = content.GetPropertyValue<string>("alternativeText")
            };
        }
    }
}
