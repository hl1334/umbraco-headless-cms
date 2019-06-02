namespace umbraco_headless_cms.library.Models.Content
{
    public class ImageContent
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public decimal? FocalPointX { get; set; }
        public decimal? FocalPointY { get; set; }
        public string AlternativeText { get; set; }
    }
}