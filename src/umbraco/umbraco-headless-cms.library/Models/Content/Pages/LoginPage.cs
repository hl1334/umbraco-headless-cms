﻿using System.Collections.Generic;
using umbraco_headless_cms.library.ProcessorAttributes;

namespace umbraco_headless_cms.library.Models.Content.Pages
{
    public class LoginPage : BasePage
    {
        [SingleMultinodeTreepickerLinkProcessor]
        public string DefaultRedirect { get; set; }

        [ModulesProcessor]
        public IEnumerable<BaseModule> Modules { get; set; }
    }
}