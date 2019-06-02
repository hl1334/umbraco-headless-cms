using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content
{
    public class CardTextSuggestionCategory
    {
        public string Name { get; set; }

        [CardTextSuggestionsProcessor]
        public IEnumerable<string> CardTextSuggestions { get; set; }
    }
}