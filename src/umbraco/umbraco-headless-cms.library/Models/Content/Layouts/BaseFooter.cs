using Newtonsoft.Json;

namespace umbraco_headless_cms.library.Models.Content.Layouts
{
    public class BaseFooter
    {
        [JsonProperty(PropertyName = "type")]
        public string DocumentTypeAlias { get; set; }
    }
}
