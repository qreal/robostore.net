using System.Web.Mvc;
using System.Web.Routing;

namespace Store
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
                name: null,
                url: "",
                defaults: new
                {
                  controller = "Home",
                  action = "ShowPrograms",
                  category = (string)null,
                  page = 1
                }
            );

      routes.MapRoute(
                null,
                "Page{page}",
                new { controller = "Home", action = "ShowPrograms" },
                new { page = @"\d+" }
            );

      routes.MapRoute(null, "{controller}/{action}");
    }
  }
}
