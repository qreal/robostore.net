using System.Web.Http;
using Domain.Data;
using Microsoft.Practices.Unity;

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

      var container = new UnityContainer();
      container.RegisterType<IData, Data>(new InjectionFactory(x => Data.Instance));
      config.DependencyResolver = new UnityResolver(container);
      config.Formatters.Remove(config.Formatters.XmlFormatter);
    }
  }
}
