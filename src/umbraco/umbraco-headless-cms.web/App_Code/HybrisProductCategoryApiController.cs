using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using umbraco_headless_cms.library.Helpers;
using umbraco_headless_cms.library.Models.CustomEditors;
using umbraco_headless_cms.library.Models.CustomEditors.Hybris;
using Umbraco.Core.Logging;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using System.Linq;

namespace umbraco_headless_cms.web
{
    [PluginController("CustomEditors")]
    public class HybrisProductCategoryApiController : UmbracoAuthorizedJsonController
    {
        private readonly string _hybrisOccBaseUrl;
        private readonly string _hybrisOccProductCategoriesUrl;
        private readonly bool _hybrisOccIgnoreSslCertificate;

        public HybrisProductCategoryApiController() : this(
            ConfigurationManager.AppSettings["hybrisOccBaseUrl"],
            ConfigurationManager.AppSettings["hybrisOccProductCategoriesUrl"],
            Convert.ToBoolean(ConfigurationManager.AppSettings["hybrisOccIgnoreSslCertificate"]))
        {
        }

        public HybrisProductCategoryApiController(string hybrisOccBaseUrl, string hybrisOccProductCategoriesUrl, bool hybrisOccIgnoreSslCertificate)
        {
            if (hybrisOccBaseUrl == null) throw new ArgumentNullException(nameof(hybrisOccBaseUrl));
            if (hybrisOccProductCategoriesUrl == null) throw new ArgumentNullException(nameof(hybrisOccProductCategoriesUrl));

            _hybrisOccBaseUrl = hybrisOccBaseUrl;
            _hybrisOccProductCategoriesUrl = hybrisOccProductCategoriesUrl;
            _hybrisOccIgnoreSslCertificate = hybrisOccIgnoreSslCertificate;
        }

        public async Task<JsonNetResult> GetAll()
        {
            var response = await GetProductCategoriesFromHybrisOcc<HybrisOccProductCategoryApiResponse>();

            if (response == null || response.Error != null)
            {
                if (response != null)
                {
                    LogHybrisOccError("Hybris/OCC error getting product categories", response.Error);
                }

                return JsonResultHelper.GetJsonResult(GetCustomErrorResponse());
            }

            if (response.Result == null && response.Error == null)
            {
                return JsonResultHelper.GetJsonResult(GetCustomErrorResponse());
            }

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                var list = response.Result?.Facets?.FirstOrDefault(facet => facet.Category == true).Values.Select(productCategory => productCategory.Name).ToList();

                return JsonResultHelper.GetJsonResult(list);
            }
            catch (ArgumentNullException exception)
            {
                LogHelper.Error<HybrisProductCategoryApiController>("The response from hybris was not as expected.", exception);
                return JsonResultHelper.GetJsonResult(GetCustomErrorResponse());
            }
        }

        /* TODO: At some later point we should maybe use the NuGet packet 'ds-dotnet-occclient' instead? */
        private async Task<HybrisOccApiResponse<T>> GetProductCategoriesFromHybrisOcc<T>()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_hybrisOccBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (_hybrisOccIgnoreSslCertificate)
                {
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                }

                try
                {
                    var response = await client.GetAsync(_hybrisOccProductCategoriesUrl);

                    return response.IsSuccessStatusCode ?
                        new HybrisOccApiResponse<T>(await response.Content.ReadAsAsync<T>(), response.StatusCode) :
                        new HybrisOccApiResponse<T>(await GetErrorResponse(response), response.StatusCode);
                }
                catch (HttpRequestException exception)
                {
                    LogHelper.Error<HybrisProductCategoryApiController>("Could not get any response from hybris", exception);
                    return null;
                }
            }
        }

        private static async Task<HybrisOccApiError> GetErrorResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadAsAsync<HybrisOccApiError>();
        }

        private static CustomEditorErrorResponse GetCustomErrorResponse()
        {
            var errorResponse = new CustomEditorErrorResponse
            {
                Error = true,
                ErrorMessage = "The response from hybris was not as expected, please try again later."
            };
            return errorResponse;
        }

        private static void LogHybrisOccError(string errorCause, HybrisOccApiError hybrisOccApiError)
        {
            var errorSpecification = hybrisOccApiError != null && hybrisOccApiError.Errors?.Count > 0 ?
                $"Hybris/OCC error specifiction - Type: {hybrisOccApiError.Errors[0].Type}, Message: {hybrisOccApiError.Errors[0].Message}" : "Hybris/OCC error not specified";
            LogHelper.Error<HybrisProductCategoryApiController>(errorCause, new Exception(errorSpecification));
        }
    }
}