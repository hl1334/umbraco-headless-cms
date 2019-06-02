using System.Collections.Generic;
using Our.Umbraco.Ditto;
using umbraco_headless_cms.library.Helpers;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ProcessorAttributes
{
    public class ModulesProcessorAttribute : DittoProcessorAttribute
    {
        public override object ProcessValue()
        {
            var modules = Context.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("modules");

            var modulesHelper = new ModulesHelper();
            var resolvedModules = modulesHelper.ResolveModules(modules);

            return resolvedModules;
        }
    }
}
