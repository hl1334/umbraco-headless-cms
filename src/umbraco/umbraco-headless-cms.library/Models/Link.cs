namespace umbraco_headless_cms.library.Models
{
    public class Link
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool NewWindow { get; set; }
        public bool IsInternal { get; set; }
    }
}
