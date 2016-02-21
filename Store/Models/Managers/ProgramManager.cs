
using System.Linq;
using Store.Models.Data;
using Store.Models.Entities;
using Store.ViewModels.Program;

namespace Store.Models.Managers
{
  public class ProgramManager
  {
    private IData data;

    public ProgramManager(IData d)
    {
      data = d;
    }

    public ProgramExport GetProgram(int id) => data.Programs.
      Where(x => x.ProgramID == id).
      Select(x => new ProgramExport
    {
      ActualVersion = x.ActualVersion,
      Code = x.Code,
      Name = x.Name
    }).FirstOrDefault();
  }
}
