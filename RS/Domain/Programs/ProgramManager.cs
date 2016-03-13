using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;

namespace Domain.Programs
{
  public class ProgramManager
  {
    private IData data;

    public ProgramManager(IData d)
    {
      data = d;
    }

    public Program GetProgramById(int id) =>
      data.Programs.First(x => x.ProgramID == id);

    public IEnumerable<ProgramRobot> GetProgramsIdsByRobotId(int robotId) => 
      data.ProgramRobots.
      Where(x => x.RobotID == robotId);

    public async Task AddProgramToRobot(int programId, int robotId)
    {
      var program = data.Programs.First(x => x.ProgramID == programId);
      var programRobot = new ProgramRobot
      {
        Program = program,
        CurrentVersion = program.ActualVersion,
        RobotID = robotId,
        Received = false
      };
      await data.AddAsync(programRobot);
    }
  }
}
