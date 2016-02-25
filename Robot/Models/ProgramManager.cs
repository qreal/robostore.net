using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.ViewModels.Configuration;
using Store.ViewModels.Program;

namespace Robot.Models
{
  class ProgramManager : Manager
  {
    public Task<ProgramExport> GetProgramAsync(int id = 5) => Task.Factory.StartNew(() => GetProgram(id));

    public ProgramExport GetProgram(int id = 5)
    {
      ProgramExport response;
      using (var wb = new WebClient())
      {
        wb.Encoding = Encoding.UTF8;
        var json = wb.DownloadString(serverUrl + "/program/getProgramById?id=" + id);
        response = JsonConvert.DeserializeObject<ProgramExport>(json);
      }
      return response;
    }
  }
}
