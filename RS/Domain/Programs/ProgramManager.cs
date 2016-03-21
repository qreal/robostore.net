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

    public Program GetProgramById(int id)
      => data.Programs.FirstOrDefault(x => x.ProgramID == id);

    public async Task CreateProgramRobot(Robot robot, Program program)
    => 
      await data.AddAsync(new ProgramRobot
      {
        Program = program,
        Robot = robot,
        CurrentVersion = program.ActualVersion
      });

    public IEnumerable<ProgramRobot> GetRobotProgramsByRobotId(int id)
      => data.ProgramRobots.Where(x => x.RobotID == id);
  }
}
