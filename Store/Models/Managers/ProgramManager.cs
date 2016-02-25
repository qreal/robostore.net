
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models.Data;
using Store.Models.Entities;
using Store.Models.Services;
using Store.Services;
using Store.ViewModels.Home;
using Store.ViewModels.Program;

namespace Store.Models.Managers
{
  public class ProgramManager
  {
    private IData data;
    private IRobotConnector robotConnector;

    public ProgramManager(IData d, IRobotConnector r)
    {
      data = d;
      robotConnector = r;
    }

    public ProgramExport GetProgramById(int id) => 
      data.Programs.
      Where(x => x.ProgramID == id).
      Select(x => new ProgramExport
    {
      ActualVersion = x.ActualVersion,
      Code = x.Code,
      Name = x.Name
    }).FirstOrDefault();

    public IEnumerable<ProgramIdExport> GetProgramsIds(int robotId) =>  data.ProgramRobots.
                                                                        Where(x => x.RobotID == robotId).
                                                                        Select(x => new ProgramIdExport {Id = x.ProgramID});

    public ProgramsListViewModel FormProgramList(int pageSize, int page) =>
      new ProgramsListViewModel
      {
        Programs = data.Programs.OrderBy(x => x.Name).
                                 Skip((page - 1) * pageSize).
                                 Take(pageSize),
        PagingInfo = new PagingInfo
        {
          CurrentPage = page,
          ItemsPerPage = pageSize,
          TotalItems = data.Programs.Count()
        }
      };

    public async Task AddProgramToRobot(int programId, int robotId)
    {
      // нужно привять экземпляр программы к роботу, по умолчанию к первому, ибо я пока не могу диалоги на выбор робота выдать
      var program = data.Programs.First(x => x.ProgramID == programId);
      var programRobot = new ProgramRobot
      {
        Program = program,
        CurrentVersion = program.ActualVersion,
        RobotID = robotId,
        Received = false
      };
      await data.AddAsync(programRobot);

      // и потом сказать роботу, что ему пора проверить, есть ли для него программы
      robotConnector.SendMessageToRobot((int)RobotConnector.OperationCategory.Program + ""
                                      + (int)RobotConnector.OperationType.GetAll);
    }
      
  }
}
