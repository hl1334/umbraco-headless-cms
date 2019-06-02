using System.Collections.Generic;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Models;
using umbraco_headless_cms.library.Models.Footer;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class FooterTextIconColumnsProcessor : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var columns = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("columns");

            if (columns == null)
            {
                return null;
            }

            var footerTextIconColumns = GetFooterTextIconColumns(columns);

            return footerTextIconColumns;
        }

        private IEnumerable<FooterTextIconColumn> GetFooterTextIconColumns(IEnumerable<IPublishedContent> columns)
        {
            var footerColumns = new List<FooterTextIconColumn>();

            foreach (var column in columns)
            {
                var elements = column.GetPropertyValue<IEnumerable<IPublishedContent>>("elements");

                var footerColumn = new FooterTextIconColumn
                {
                    Headline = column.GetPropertyValue<string>("headline"),
                };

                if (elements == null)
                {
                    footerColumns.Add(footerColumn);
                }
                else
                {
                    var footerColumnElements = GetFooterTextIconColumnElement(elements);

                    footerColumn.Elements = footerColumnElements;

                    footerColumns.Add(footerColumn);
                }
            }

            return footerColumns;
        }

        private IEnumerable<FooterTextIconColumnElement> GetFooterTextIconColumnElement(IEnumerable<IPublishedContent> elements)
        {
            var footerTextIconColumnElements = new List<FooterTextIconColumnElement>();

            foreach (var element in elements)
            {
                switch (element.DocumentTypeAlias)
                {
                    case "footerTextbox":
                        var textBox = element.As<FooterTextbox>();
                        footerTextIconColumnElements.Add(textBox);
                        break;
                    case "footerIconElement":
                        var icon = element.As<FooterIconElement>();
                        footerTextIconColumnElements.Add(icon);
                        break;
                }
            }

            return footerTextIconColumnElements;
        }
    }
}
