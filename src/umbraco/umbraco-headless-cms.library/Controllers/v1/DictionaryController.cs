using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using umbraco_headless_cms.library.Authorization;
using umbraco_headless_cms.library.Helpers;
using Umbraco.Web.Mvc;

namespace umbraco_headless_cms.library.Controllers.v1
{
    public class DictionaryController : UmbracoController
    {
        [BasicAuthorize]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        [HttpGet]
        public ActionResult All(string rootKey, string languageIsoCode)
        {
            var dictionaryRoot = ApplicationContext.Services.LocalizationService.GetRootDictionaryItems()
                .FirstOrDefault(d => string.Equals(d.ItemKey, rootKey, StringComparison.InvariantCultureIgnoreCase));

            if (dictionaryRoot == null)
            {
                return new HttpNotFoundResult();
            }

            var dictionaryItems =
                ApplicationContext.Services.LocalizationService.GetDictionaryItemChildren(dictionaryRoot.Key);

            var dictionary = dictionaryItems.ToDictionary(
                item => item.ItemKey.Substring(0, item.ItemKey.IndexOf("-", StringComparison.Ordinal)), 
                item => item.Translations.Where(x => x.Language.IsoCode == languageIsoCode).Select(x => x.Value).FirstOrDefault()
            );

            return JsonResultHelper.GetJsonResult(dictionary);
        }
    }
}