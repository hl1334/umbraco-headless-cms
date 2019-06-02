using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class OccasionSliderModule : BaseModule
    {
        public string Title { get; set; }
   
        [SingleMultinodeTreepickerLinkProcessor]
        public string ShowAllButtonLink { get; set; }

        public string ShowAllButtonCaption { get; set; }
    }
}