using System.Collections.Generic;

namespace umbraco_headless_cms.library.Models.Content.Modules
{
    public class ImagesModule : BaseModule
    {
        public IEnumerable<ImageItem> Images { get; set; }
        public string Caption { get; set; }
        public bool FullWidth { get; set; }
    }
}