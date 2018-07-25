using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using SearcherApplication.Web.App_Start;
using System.Web.Routing;
using System.Web.Optimization;

[assembly: OwinStartup(typeof(SearcherApplication.Web.SearcherStartup))]

namespace SearcherApplication.Web
{
    public class SearcherStartup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyResolver.SetResolver(AutofacConfig.GetResolver());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}