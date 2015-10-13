using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Robots.Domain.Abstract;
using Robots.Domain.Concrete;
using Robots.WebUI.Infrastructure.Abstract;
using Robots.WebUI.Infrastructure.Concrete;

// Здесь мы регистрируем наши прявязки

namespace Robots.WebUI.Infrastructure
{
  public class NinjectDependencyResolver : IDependencyResolver
  {
    private IKernel kernel;
    public NinjectDependencyResolver(IKernel kernelParam)
    {
      kernel = kernelParam;
      AddBindings();
    }
    public object GetService(Type serviceType)
    {
      return kernel.TryGet(serviceType);
    }
    public IEnumerable<object> GetServices(Type serviceType)
    {
      return kernel.GetAll(serviceType);
    } 
    private void AddBindings()
    {
      kernel.Bind<IUserRepository>().To<EFUserRepository>();
      kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
    }
  }
}
