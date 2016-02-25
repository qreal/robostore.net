using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.ViewModels.Program;

namespace Robot.Models
{
  class ProgramManager : Manager
  {
    public Task<IEnumerable<ProgramExport>> GetProgramAsync() => Task.Factory.StartNew(GetAllPrograms);

    private IEnumerable<ProgramExport> GetAllPrograms()
    {
      IEnumerable<ProgramIdExport> ids;
      using (var wb = new WebClient())
      {
        wb.Encoding = Encoding.UTF8;
        var json = wb.DownloadString(serverUrl + "/program/getLoadingProgramsIds");
        ids = JsonConvert.DeserializeObject<IEnumerable<ProgramIdExport>>(json);
      }
      return ids.Select(id => GetProgram(id.Id)).ToList();
    }

    private ProgramExport GetProgram(int id)
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
