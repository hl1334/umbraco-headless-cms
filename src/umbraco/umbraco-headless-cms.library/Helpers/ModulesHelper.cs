using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Extensions.Wrappers;
using umbraco_headless_cms.library.Models.Content;
using umbraco_headless_cms.library.Models.Content.Modules;
using umbraco_headless_cms.library.Models.Content.Modules.Checkout;
using umbraco_headless_cms.library.Models.Content.Modules.Common;
using umbraco_headless_cms.library.Models.Content.Modules.MyAccount;
using umbraco_headless_cms.library.Models.Content.Modules.Product_Details;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.Helpers
{
    public class ModulesHelper
    {
        private readonly bool _resolveComponentModules;
        private readonly IGetPropertyValueWrapper _getPropertyValueWrapper;
        private readonly bool _preview;
        private readonly string _previewDateString;

        public ModulesHelper() : this(
            Convert.ToBoolean(ConfigurationManager.AppSettings["resolveComponentModules"]),
            new GetPropertyValueWrapper(),
            !UmbracoContext.Current.HttpContext.Request.QueryString["preview"].IsNullOrWhiteSpace(),
            UmbracoContext.Current.HttpContext.Request.QueryString["date"])
        {            
        }

        public ModulesHelper(bool resolveComponentModules, IGetPropertyValueWrapper getPropertyValueWrapper,
            bool preview, string previewDateString)
        {
            _resolveComponentModules = resolveComponentModules;
            _getPropertyValueWrapper = getPropertyValueWrapper;
            _preview = preview;
            _previewDateString = previewDateString;
        }

        public IEnumerable<BaseModule> ResolveModules(IEnumerable<IPublishedContent> modules)
        {
            if (modules == null)
            {
                yield break;
            }

            foreach (var module in modules)
            {
                var publishDate = _getPropertyValueWrapper.GetPropertyValue<DateTime>(module, "publishDate");
                var unpublishDate = _getPropertyValueWrapper.GetPropertyValue<DateTime>(module, "unpublishDate");

                // If module has a set publishDate and/or unpublishDate, we need to check if module should be published using a date filter.
                // Date filter is either current system datetime or if preview and a specific preview date has been requested then we use the preview date.
                // If preview has been requested without a preview date we just return the module regardless.
                DateTime filterDate;
                if ((publishDate != DateTime.MinValue || unpublishDate != DateTime.MinValue) && _preview && !_previewDateString.IsNullOrWhiteSpace())
                {
                    var successPreviewDateParse = DateTime.TryParse(_previewDateString, CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out filterDate);
                    // If preview date cannot be parsed we fall back to current system datetime.
                    if (!successPreviewDateParse)
                    {
                        filterDate = DateTime.Now;
                    }
                }
                else if ((publishDate != DateTime.MinValue || unpublishDate != DateTime.MinValue) && _preview &&
                    _previewDateString.IsNullOrWhiteSpace())
                {
                    // Preview has been requested without a preview date, so we set the filterdate to MAX-value to make sure module gets through.
                    filterDate = DateTime.MaxValue;
                }
                else
                {
                    filterDate = DateTime.Now;
                }

                if (publishDate != DateTime.MinValue && publishDate >= filterDate || unpublishDate != DateTime.MinValue && unpublishDate <= filterDate)
                {
                    continue;
                }

                switch (module.DocumentTypeAlias)
                {
                    case "componentModule":
                        if (_resolveComponentModules)
                        {
                            var component =
                                _getPropertyValueWrapper.GetPropertyValue<IEnumerable<IPublishedContent>>(module, "component");
                            var resolvedModule = ResolveModules(component).FirstOrDefault();

                            if (resolvedModule != null)
                            {
                                yield return resolvedModule;
                            }
                        }
                        else
                        {
                            var component = module.As<ComponentModule>();
                            yield return component;
                        }
                        break;

                    case "textboxModule":
                        var moduleTextbox = module.As<TextboxModule>();
                        yield return moduleTextbox;
                        break;

                    case "imageboxModule":
                        var imageBox = module.As<ImageboxModule>();
                        yield return imageBox;
                        break;

                    case "imagesModule":
                        var image = module.As<ImagesModule>();
                        yield return image;
                        break;

                    case "heroBannerModule":
                        var heroBanner = module.As<HeroBannerModule>();
                        yield return heroBanner;
                        break;

                    case "uspModule":
                        var usp = module.As<UspModule>();
                        yield return usp;
                        break;

                    case "productSliderModule":
                        var productSlider = module.As<ProductSliderModule>();
                        yield return productSlider;
                        break;

                    case "occasionSliderModule":
                        var occasionSlider = module.As<OccasionSliderModule>();
                        yield return occasionSlider;
                        break;

                    case "videoModule":
                        var video = module.As<VideoModule>();
                        yield return video;
                        break;

                    case "splitviewModule":
                        var splitview = module.As<SplitviewModule>();
                        yield return splitview;
                        break;

                    case "ctaButtonModule":
                        var buttonModule = module.As<ButtonModule>();
                        yield return buttonModule;
                        break;

                    case "productListModule":
                        var productListModule = module.As<ProductListModule>();
                        yield return productListModule;
                        break;

                    case "loginModule":
                        var loginModule = module.As<LoginModule>();
                        yield return loginModule;
                        break;

                    case "checkoutLoginModule":
                        var checkoutLoginModule = module.As<CheckoutLoginModule>();
                        yield return checkoutLoginModule;
                        break;

                    case "checkoutReceiverModule":
                        var checkoutReceiver = module.As<CheckoutReceiverModule>();
                        yield return checkoutReceiver;
                        break;

                    case "checkoutDeliveryTimeModule":
                        var checkoutDeliveryTime = module.As<CheckoutDeliveryTimeModule>();
                        yield return checkoutDeliveryTime;
                        break;

                    case "checkoutDeliveryCardModule":
                        var checkoutDeliveryCard = module.As<CheckoutDeliveryCardModule>();
                        yield return checkoutDeliveryCard;
                        break;

                    case "checkoutSenderModule":
                        var checkoutSender = module.As<CheckoutSenderModule>();
                        yield return checkoutSender;
                        break;

                    case "checkoutOrderConfirmationModule":
                        var checkoutOrderConfirmation = module.As<CheckoutOrderConfirmationModule>();
                        yield return checkoutOrderConfirmation;
                        break;

                    case "checkoutDeliveryDetailsModule":
                        var checkoutDeliveryDetails = module.As<CheckoutDeliveryDetailsModule>();
                        yield return checkoutDeliveryDetails;
                        break;

                    case "checkoutDiscountOrGiftCardModule":
                        var checkoutDiscountOrGiftCard = module.As<CheckoutDiscountOrGiftcardModule>();
                        yield return checkoutDiscountOrGiftCard;
                        break;

                    case "checkoutPaymentMethodModule":
                        var checkoutPaymentMethod = module.As<CheckoutPaymentMethodModule>();
                        yield return checkoutPaymentMethod;
                        break;

                    case "checkoutProductVariantModule":
                        var checkoutProductVariant = module.As<CheckoutProductVariantModule>();
                        yield return checkoutProductVariant;
                        break;

                    case "checkoutReceiptModule":
                        var checkoutReceipt = module.As<CheckoutReceiptModule>();
                        yield return checkoutReceipt;
                        break;

                    case "checkoutOrderOverviewModule":
                        var checkoutOrderOverview = module.As<CheckoutOrderOverviewModule>();
                        yield return checkoutOrderOverview;
                        break;

                    case "checkoutPaymentDetailsModule":
                        var checkoutPaymentDetails = module.As<CheckoutPaymentDetailsModule>();
                        yield return checkoutPaymentDetails;
                        break;

                    case "checkoutNextStepModule":
                        var checkoutNextStep = module.As<CheckoutNextStepModule>();
                        yield return checkoutNextStep;
                        break;

                    case "checkoutTermsAcceptModule":
                        var checkoutTermsAccept = module.As<CheckoutTermsAcceptModule>();
                        yield return checkoutTermsAccept;
                        break;

                    case "productDetailsOverviewModule":
                        var productDetailsOverview = module.As<ProductDetailsOverviewModule>();
                        yield return productDetailsOverview;
                        break;

                    case "productDetailsSpecificationModule":
                        var productDetailsSpecification = module.As<ProductDetailsSpecificationModule>();
                        yield return productDetailsSpecification;
                        break;

                    case "myAccountOrdersModule":
                        var myAccountOrders = module.As<MyAccountOrdersModule>();
                        yield return myAccountOrders;
                        break;

                    case "myAccountUserInformationModule":
                        var myAccountUserInformation = module.As<MyAccountUserInformationModule>();
                        yield return myAccountUserInformation;
                        break;

                    case "myAccountChangePasswordModule":
                        var myAccountChangePassword = module.As<MyAccountChangePasswordModule>();
                        yield return myAccountChangePassword;
                        break;

                }
            }
        }
    }
}
