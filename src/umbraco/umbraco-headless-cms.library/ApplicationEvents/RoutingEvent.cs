using System.Web.Mvc;
using System.Web.Routing;
using umbraco_headless_cms.library.Routing;
using Umbraco.Core;
using Umbraco.Web;

namespace umbraco_headless_cms.library.ApplicationEvents
{
    public class RoutingEvent : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            /* 
             * To create a new version of a controller, change the v1 to v?.
             * Change "typeof(controller).Namespace" to the new controller. 
             */

            #region Version 1

            RouteTable.Routes.MapUmbracoRoute("v1_pages_map", "v1/pages",
                new
                {
                    controller = "Pages",
                    action = "PagesMap"
                },
                new UmbracoVirtualNodeByXPathRouteHandler("/root/pagesFolder"),
                null,
                new[]
                {
                    typeof(Controllers.v1.PagesController).Namespace
                }
            );

            RouteTable.Routes.MapUmbracoRoute("v1_pages", "v1/pages/{id}",
                new
                {
                    controller = "Pages",
                    action = "Index"
                },
                new UmbracoVirtualNodeByIdRouteHandler(),
                null,
                new[]
                {
                    typeof(Controllers.v1.PagesController).Namespace
                }
            );

            // TODO: Remove this route at some point; we don't want to have this option, since it has issues and does not provide anything useful.
            RouteTable.Routes.MapUmbracoRoute("v1_pages_by_name", "namedpages/{pagename}",
                new
                {
                    controller = "Pages",
                    action = "Index"
                },
                new UmbracoVirtualNodeByXPathRouteHandler("/root/home//*[@urlName=$pagename]"),
                null,
                new[]
                {
                    typeof(Controllers.v1.PagesController).Namespace
                }
            );

            RouteTable.Routes.MapUmbracoRoute("v1_components", "v1/components/{id}",
                new
                {
                    controller = "Components",
                    action = "Index"
                },
                new UmbracoVirtualNodeByIdRouteHandler(),
                null,
                new[]
                {
                    typeof(Controllers.v1.ComponentsController).Namespace
                }
            );

            RouteTable.Routes.MapUmbracoRoute("v1_layouts_map", "v1/layouts",
                new
                {
                    controller = "Layouts",
                    action = "LayoutsMap"
                },
                new UmbracoVirtualNodeByXPathRouteHandler("/root/layoutsFolder"),
                null,
                new[]
                {
                    typeof(Controllers.v1.LayoutsController).Namespace
                }
            );

            RouteTable.Routes.MapUmbracoRoute("v1_layouts", "v1/layouts/{id}",
                new
                {
                    controller = "Layouts",
                    action = "Index"
                },
                new UmbracoVirtualNodeByIdRouteHandler(),
                null,
                new[]
                {
                    typeof(Controllers.v1.LayoutsController).Namespace
                }
            );

            RouteTable.Routes.MapUmbracoRoute("v1_occasions_map", "v1/occasions",
                new
                {
                    controller = "Occasions",
                    action = "OccasionsMap"
                },
                new UmbracoVirtualNodeByXPathRouteHandler("/root/occasionsFolder"),
                null,
                new[]
                {
                    typeof(Controllers.v1.OccasionsController).Namespace
                }
            );

            RouteTable.Routes.MapUmbracoRoute("v1_settings_map", "v1/settings",
                new
                {
                    controller = "Settings",
                    action = "SettingsMap"
                },
                new UmbracoVirtualNodeByXPathRouteHandler("/root/settings"),
                null,
                new[]
                {
                    typeof(Controllers.v1.SettingsController).Namespace
                }
            );

            RouteTable.Routes.MapRoute("v1_dictionary", "v1/dictionary/{rootKey}/{languageIsoCode}",
                new
                {
                    controller = "Dictionary",
                    action = "All",
                    languageIsoCode = "da-DK"
                },
                new[]
                {
                    typeof(Controllers.v1.DictionaryController).Namespace
                }
            );

            RouteTable.Routes.MapRoute("v1_customerrors_404", "v1/404",
                new
                {
                    controller = "CustomErrors",
                    action = "NotFound"
                },
                new[]
                {
                    typeof(Controllers.v1.CustomErrorsController).Namespace
                }
            );

            #endregion

            base.ApplicationStarted(umbracoApplication, applicationContext);

        }
    }
}
