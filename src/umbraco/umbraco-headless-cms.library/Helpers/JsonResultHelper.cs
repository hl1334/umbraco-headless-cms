using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Helpers
{
    public class JsonResultHelper
    {
        public static JsonNetResult GetJsonResult(object content)
        {
            var result = new JsonNetResult
            {
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                Data = content,
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            return result;
        }
    }
}
