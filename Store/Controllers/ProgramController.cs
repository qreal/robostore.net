using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Domain.Data;
using Domain.Programs;
using Store.Models.Program;

namespace Store.Controllers
{
  public class ProgramController : ApiController
  {
    private readonly ProgramManager _manager;
    private const int TestRobotId = 4;

    public ProgramController(IData data)
    {
      _manager = new ProgramManager(data);
    }

    [Route("api/program/getProgramById")]
    [HttpGet]
    public ProgramExport GetProgram(int id)
    {
      var program = _manager.GetProgramById(id);
      return new ProgramExport
      {
        ActualVersion = program.ActualVersion,
        Code = program.Code,
        Name = program.Name
      };
    }

    [Route("api/program/getLoadingProgramsIds")]
    [HttpGet]
    public IEnumerable<ProgramIdExport> GetLoadingProgramsIds() => 
      _manager.GetProgramsIdsByRobotId(TestRobotId).
      Select(x => new ProgramIdExport { Id = x.ProgramID });

  }
}
