using System.Collections.Generic;
using System.Web.Http;
using Store.Models.Data;
using Store.Models.Managers;
using Store.Services;
using Store.ViewModels.Program;

namespace Store.Controllers
{
  public class ProgramController : ApiController
  {
    private readonly ProgramManager _manager;
    private const int TestRobotId = 4;

    public ProgramController(IData data, IRobotConnector r)
    {
      _manager = new ProgramManager(data, r);
    }

    [Route("api/program/getProgramById")]
    [HttpGet]
    // todo как мы узнаем, кто получатель, то ему Received = true поставим
    public ProgramExport GetProgram(int id) => _manager.GetProgramById(id);

    [Route("api/program/getLoadingProgramsIds")]
    [HttpGet]
    public IEnumerable<ProgramIdExport> GetLoadingProgramsIds() => _manager.GetProgramsIds(TestRobotId);

  }
}
