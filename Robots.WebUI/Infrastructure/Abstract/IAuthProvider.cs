using System.Collections;
using System.Collections.Generic;
using Robots.Domain.Entities;

namespace Robots.WebUI.Infrastructure.Abstract
{
  public interface IAuthProvider
  {
    bool Authenticate(string username, string password);
  }
}
