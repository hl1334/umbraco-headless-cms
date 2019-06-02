using Newtonsoft.Json;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class BaseHeader
    {
        [JsonProperty(PropertyName = "type")]
        public string DocumentTypeAlias { get; set; }
    }
}
