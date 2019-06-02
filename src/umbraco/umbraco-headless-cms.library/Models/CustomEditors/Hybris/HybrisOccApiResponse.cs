using System.Net;

namespace umbraco_headless_cms.library.Models.CustomEditors.Hybris
{
    public class HybrisOccApiResponse<T>
    {
        public HybrisOccApiResponse(T result, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Result = result;
        }

        public HybrisOccApiResponse(HybrisOccApiError error, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Error = error;
        }
        public T Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public HybrisOccApiError Error { get; set; }
    }
}
