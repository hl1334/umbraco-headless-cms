using Newtonsoft.Json;

namespace umbraco_headless_cms.library.Models.Content
{
    public class BaseContent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string DocumentTypeAlias { get; set; }

        public string Link { get; set; }
    }
}
