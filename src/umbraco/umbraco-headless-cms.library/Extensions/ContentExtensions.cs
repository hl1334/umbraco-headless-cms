using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Core.Strings;
using Umbraco.Web.Models;

// We exclude this from coverage report for now, since we probably need to refactor greatly to make the code testable.

namespace umbraco_headless_cms.library.Extensions
{


    /// <summary>
    /// Content extensions.
    /// ref: https://gist.github.com/jbreuer/dde3605035179c34b7287850c45cb8c9
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ContentExtensions
    {
        /// <summary>
        /// Convert an IContent to an IPublishedContent.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="isPreview">
        /// The is preview.
        /// </param>
        /// <returns>
        /// The <see cref="IPublishedContent"/>.
        /// </returns>
        public static IPublishedContent ToPublishedContent(this IContent content, bool isPreview = false)
        {
            return new PublishedContent(content, isPreview);
        }
    }

    /// <summary>
    /// The published content.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PublishedContent : PublishedContentWithKeyBase
    {
        private static ServiceContext Services => ApplicationContext.Current.Services;

        private readonly PublishedContentType _contentType;

        private readonly IContent _inner;

        private readonly bool _isPreviewing;

        private readonly Lazy<string> _lazyCreatorName;

        private readonly Lazy<string> _lazyUrlName;

        private readonly Lazy<string> _lazyWriterName;

        private readonly IPublishedProperty[] _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedContent"/> class.
        /// </summary>
        /// <param name="inner">
        /// The inner.
        /// </param>
        /// <param name="isPreviewing">
        /// The is previewing.
        /// </param>
        public PublishedContent(IContent inner, bool isPreviewing)
        {
            if (inner == null) throw new NullReferenceException("inner");

            _inner = inner;
            _isPreviewing = isPreviewing;

            _lazyUrlName = new Lazy<string>(() => _inner.GetUrlSegment().ToLower());
            _lazyCreatorName = new Lazy<string>(() => _inner.GetCreatorProfile(Services.UserService).Name);
            _lazyWriterName = new Lazy<string>(() => _inner.GetWriterProfile(Services.UserService).Name);

            _contentType = PublishedContentType.Get(PublishedItemType.Content, _inner.ContentType.Alias);

            _properties =
                MapProperties(
                    _contentType.PropertyTypes,
                    _inner.Properties,
                    (t, v) => new PublishedProperty(t, v, _isPreviewing)).ToArray();
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public override int Id => _inner.Id;

        /// <summary>
        /// Gets the key.
        /// </summary>
        public override Guid Key => _inner.Key;

        /// <summary>
        /// Gets the document type id.
        /// </summary>
        public override int DocumentTypeId => _inner.ContentTypeId;

        /// <summary>
        /// Gets the document type alias.
        /// </summary>
        public override string DocumentTypeAlias => _inner.ContentType.Alias;

        /// <summary>
        /// Gets the item type.
        /// </summary>
        public override PublishedItemType ItemType => PublishedItemType.Content;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name => _inner.Name;

        /// <summary>
        /// Gets the level.
        /// </summary>
        public override int Level => _inner.Level;

        /// <summary>
        /// Gets the path.
        /// </summary>
        public override string Path => _inner.Path;

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        public override int SortOrder => _inner.SortOrder;

        /// <summary>
        /// Gets the version.
        /// </summary>
        public override Guid Version => _inner.Version;

        /// <summary>
        /// Gets the template id.
        /// </summary>
        public override int TemplateId => _inner.Template?.Id ?? 0;

        /// <summary>
        /// Gets the url name.
        /// </summary>
        public override string UrlName => _lazyUrlName.Value;

        /// <summary>
        /// Gets the create date.
        /// </summary>
        public override DateTime CreateDate => _inner.CreateDate;

        /// <summary>
        /// Gets the update date.
        /// </summary>
        public override DateTime UpdateDate => _inner.UpdateDate;

        /// <summary>
        /// Gets the creator id.
        /// </summary>
        public override int CreatorId => _inner.CreatorId;

        /// <summary>
        /// Gets the creator name.
        /// </summary>
        public override string CreatorName => _lazyCreatorName.Value;

        /// <summary>
        /// Gets the writer id.
        /// </summary>
        public override int WriterId => _inner.WriterId;

        /// <summary>
        /// Gets the writer name.
        /// </summary>
        public override string WriterName => _lazyWriterName.Value;

        /// <summary>
        /// Gets a value indicating whether is draft.
        /// </summary>
        public override bool IsDraft => _inner.Published == false;

        /// <summary>
        /// Gets the parent.
        /// </summary>
        public override IPublishedContent Parent
        {
            get
            {
                var parent = _inner.Parent();
                return parent.ToPublishedContent(_isPreviewing);
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public override IEnumerable<IPublishedContent> Children
        {
            get
            {
                var children = _inner.Children().ToList();

                return
                    children.Select(x => x.ToPublishedContent(_isPreviewing))
                        .Where(x => x != null)
                        .OrderBy(x => x.SortOrder);
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public override ICollection<IPublishedProperty> Properties => _properties;

        /// <summary>
        /// Gets the content type.
        /// </summary>
        public override PublishedContentType ContentType => _contentType;

        /// <summary>
        /// The get property.
        /// </summary>
        /// <param name="alias">
        /// The alias.
        /// </param>
        /// <returns>
        /// The <see cref="IPublishedProperty"/>.
        /// </returns>
        public override IPublishedProperty GetProperty(string alias)
        {
            return _properties.FirstOrDefault(x => x.PropertyTypeAlias.InvariantEquals(alias));
        }

        /// <summary>
        /// The map properties.
        /// </summary>
        /// <param name="propertyTypes">
        /// The property types.
        /// </param>
        /// <param name="properties">
        /// The properties.
        /// </param>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        internal static IEnumerable<IPublishedProperty> MapProperties(
            IEnumerable<PublishedPropertyType> propertyTypes,
            IEnumerable<Property> properties,
            Func<PublishedPropertyType, object, IPublishedProperty> map)
        {
            var propertyEditorResolver = PropertyEditorResolver.Current;
            var dataTypeService = ApplicationContext.Current.Services.DataTypeService;

            return propertyTypes.Select(
                x =>
                {
                    var p = properties.SingleOrDefault(xx => xx.Alias == x.PropertyTypeAlias);
                    var v = p?.Value;
                    if (v == null)
                        return map(x, v);

                    var e = propertyEditorResolver.GetByAlias(x.PropertyEditorAlias);

                    // We are converting to string, even for database values which are integer or
                    // DateTime, which is not optimum. Doing differently would require that we have a way to tell
                    // whether the conversion to XML string changes something or not... which we don't, and we
                    // don't want to implement it as PropertyValueEditor.ConvertDbToXml/String should die anyway.

                    // Don't think about improving the situation here: this is a corner case and the real
                    // thing to do is to get rig of PropertyValueEditor.ConvertDbToXml/String.

                    // Use ConvertDbToString to keep it simple, although everywhere we use ConvertDbToXml and
                    // nothing ensures that the two methods are consistent.
                    if (e != null)
                    {
                        v = e.ValueEditor.ConvertDbToString(p, p.PropertyType, dataTypeService);
                    }

                    return map(x, v);
                });
        }
    }

    [ExcludeFromCodeCoverage]
    internal static class ContentBaseExtensions
    {
        /// <summary>
        /// Gets the url segment providers.
        /// </summary>
        /// <remarks>This is so that unit tests that do not initialize the resolver do not
        /// fail and fall back to defaults. When running the whole Umbraco, CoreBootManager
        /// does initialise the resolver.</remarks>
        private static IEnumerable<IUrlSegmentProvider> UrlSegmentProviders => UrlSegmentProviderResolver.HasCurrent
            ? UrlSegmentProviderResolver.Current.Providers
            : new IUrlSegmentProvider[] { new DefaultUrlSegmentProvider() };

        /// <summary>
        /// Gets the default url segment for a specified content.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The url segment.
        /// </returns>
        public static string GetUrlSegment(this IContentBase content)
        {
            var url = UrlSegmentProviders.Select(p => p.GetUrlSegment(content)).First(u => u != null);
            url = url ?? new DefaultUrlSegmentProvider().GetUrlSegment(content); // be safe
            return url;
        }

        /// <summary>
        /// Gets the url segment for a specified content and culture.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The url segment.
        /// </returns>
        public static string GetUrlSegment(this IContentBase content, CultureInfo culture)
        {
            var url = UrlSegmentProviders.Select(p => p.GetUrlSegment(content, culture)).First(u => u != null);
            url = url ?? new DefaultUrlSegmentProvider().GetUrlSegment(content, culture); // be safe
            return url;
        }
    }

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(Namespace = "http://umbraco.org/webservices/")]
    internal class PublishedProperty : PublishedPropertyBase
    {
        private readonly object _dataValue;

        private readonly bool _isPreviewing;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedProperty"/> class.
        /// </summary>
        /// <param name="propertyType">
        /// The property type.
        /// </param>
        /// <param name="dataValue">
        /// The data value.
        /// </param>
        /// <param name="isPreviewing">
        /// The is previewing.
        /// </param>
        public PublishedProperty(PublishedPropertyType propertyType, object dataValue, bool isPreviewing)
            : base(propertyType)
        {
            _dataValue = dataValue;
            _isPreviewing = isPreviewing;
        }

        /// <summary>
        /// Gets a value indicating whether has value.
        /// </summary>
        public override bool HasValue => _dataValue != null
                                         && (_dataValue is string == false
                                             || string.IsNullOrWhiteSpace((string) _dataValue) == false);

        /// <summary>
        /// Gets the data value.
        /// </summary>
        public override object DataValue => _dataValue;

        /// <summary>
        /// Gets the value.
        /// </summary>
        public override object Value
        {
            get
            {
                if (_dataValue == null)
                {
                    return null;
                }
                var source = PropertyType.ConvertDataToSource(_dataValue, _isPreviewing);
                return PropertyType.ConvertSourceToObject(source, _isPreviewing);
            }
        }

        /// <summary>
        /// Gets the x path value.
        /// </summary>
        public override object XPathValue
        {
            get
            {
                if (_dataValue == null)
                {
                    return null;
                }
                var source = PropertyType.ConvertDataToSource(_dataValue, _isPreviewing);
                return PropertyType.ConvertSourceToXPath(source, _isPreviewing);
            }
        }
    }

    [ExcludeFromCodeCoverage]
    internal abstract class PublishedPropertyBase : IPublishedProperty
    {
        /// <summary>
        /// The property type.
        /// </summary>
        public readonly PublishedPropertyType PropertyType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedPropertyBase"/> class.
        /// </summary>
        /// <param name="propertyType">
        /// The property type.
        /// </param>
        protected PublishedPropertyBase(PublishedPropertyType propertyType)
        {
            if (propertyType == null) throw new ArgumentNullException(nameof(propertyType));

            PropertyType = propertyType;
        }

        /// <summary>
        /// Gets the property type alias.
        /// </summary>
        public string PropertyTypeAlias => PropertyType.PropertyTypeAlias;

        /// <summary>
        /// Gets a value indicating whether has value.
        /// </summary>
        public abstract bool HasValue { get; }

        /// <summary>
        /// Gets the data value.
        /// </summary>
        public abstract object DataValue { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public abstract object Value { get; }

        /// <summary>
        /// Gets the x path value.
        /// </summary>
        public abstract object XPathValue { get; }
    }
}
