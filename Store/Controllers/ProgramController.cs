using System.Web.Http;
using Store.Models.Data;
using Store.Models.Managers;
using Store.ViewModels.Program;

namespace Store.Controllers
{
  public class ProgramController : ApiController
  {
    private ProgramManager _manager;

    public ProgramController(IData data)
    {
      _manager = new ProgramManager(data);
    }

    [Route("api/program/get")]
    [HttpGet]
    public ProgramExport GetProgram(int id) => _manager.GetProgram(id);
  }
}
