using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Models.Services;

namespace Store.ViewModels.Home
{
  public class ProgramsListViewModel
  {
    public IEnumerable<Models.Entities.Program>  Programs { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
