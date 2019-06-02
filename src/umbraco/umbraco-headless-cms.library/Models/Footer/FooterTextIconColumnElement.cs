using Newtonsoft.Json;

namespace umbraco_headless_cms.library.Models.Footer
{
    public class FooterTextIconColumnElement
    {
        [JsonProperty(PropertyName = "type")]
        public string DocumentTypeAlias { get; set; }
    }
}
