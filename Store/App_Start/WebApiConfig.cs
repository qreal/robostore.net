using System.Web.Http;
using Microsoft.Practices.Unity;
using Store.Models.Data;
using Store.Services;

namespace Store
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
        );

      //var container = new UnityContainer();
      //container.RegisterType<IData, Data>(new HierarchicalLifetimeManager());
      //config.DependencyResolver = new UnityResolver(container);

      config.Formatters.Remove(config.Formatters.XmlFormatter);
    }
  }
}
