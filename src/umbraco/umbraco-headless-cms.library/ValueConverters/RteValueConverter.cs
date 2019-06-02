using System.Web;
using ds.umbraco.imgix;
using HtmlAgilityPack;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;
using Umbraco.Web.PropertyEditors.ValueConverters;

namespace umbraco_headless_cms.library.ValueConverters
{
    [PropertyValueType(typeof(IHtmlString))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
    public class RteValueConverter : RteMacroRenderingValueConverter
    {
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            var doc = new HtmlDocument { OptionOutputOriginalCase = true };
            doc.LoadHtml(source.ToString());

            // Get all img tags to replace the "src" url with Imgix url and cleanup the html.
            var imgNodes = doc.DocumentNode.SelectNodes("//img");
            if (imgNodes != null)
            {
                foreach (var imgNode in imgNodes)
                {
                    var src = imgNode.GetAttributeValue("src", null);
                    if (src == null) continue;

                    var udiAttribute = imgNode.GetAttributeValue("data-udi", null);

                    if (udiAttribute.IsNullOrWhiteSpace()) continue;

                    var udi = Udi.Parse(udiAttribute);

                    var guidUdi = GuidUdi.Parse(udi.ToString());
                    var helper = new UmbracoHelper(UmbracoContext.Current);
                    var image = helper.TypedMedia(guidUdi.Guid);
                    if (image != null)
                    {
                        var imgixImageUrl = image.GetImgixImage().Url;

                        // Try get the potential image width and height attributes
                        var width = imgNode.GetAttributeValue("width", null);
                        var height = imgNode.GetAttributeValue("height", null);
                        if (!width.IsNullOrWhiteSpace() && !height.IsNullOrWhiteSpace())
                        {
                            imgixImageUrl += $"?w={width}&h={height}";
                            // Clean up
                            imgNode.Attributes["width"].Remove();
                            imgNode.Attributes["height"].Remove();
                        }

                        // Replace the src url with Imgix url
                        imgNode.SetAttributeValue("src", imgixImageUrl);
                    }

                    // Set alternative text
                    var alt = imgNode.GetAttributeValue("alt", null);
                    if (!alt.IsNullOrWhiteSpace()) continue;

                    var imageAltText = image.GetPropertyValue<string>("alternativeText");
                    imgNode.SetAttributeValue("alt", imageAltText);

                    // Clean up
                    imgNode.Attributes["data-udi"].Remove();
                    if (!imgNode.GetAttributeValue("id", null).IsNullOrWhiteSpace())
                    {
                        imgNode.Attributes["id"].Remove();
                    }
                }
            }

            // Get all anchor tags to cleanup the html.
            var anchorNodes = doc.DocumentNode.SelectNodes("//a");
            if (anchorNodes != null)
            {
                foreach (var anchorNode in anchorNodes)
                {
                    // Clean up
                    if (!anchorNode.GetAttributeValue("data-udi", null).IsNullOrWhiteSpace())
                    {
                        anchorNode.Attributes["data-udi"].Remove();
                    }
                }
            }

            if (imgNodes != null || anchorNodes != null)
            {
                return base.ConvertDataToSource(propertyType, doc.DocumentNode.OuterHtml, preview);
            }

            return base.ConvertDataToSource(propertyType, source, preview);
        }
    }
}