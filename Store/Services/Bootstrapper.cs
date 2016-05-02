using System.Web.Mvc;
using Domain.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace Store.Services
{
  /*
Нужен для DI в обычных контроллерах
*/
  public class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();
      DependencyResolver.SetResolver(new UnityDependencyResolver(container));
      return container;
    }
    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here  
      //This is the important line to edit  
      container.RegisterType<IData, Data>(new InjectionFactory(c => Data.Instance));

      RegisterTypes(container);
      return container;
    }
    public static void RegisterTypes(IUnityContainer container)
    {

    }
  }
}
