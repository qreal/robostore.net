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
      => data.Programs.Data.FirstOrDefault(x => x.ProgramID == id);

    public Program GetProgramByProgramRobotId(int id)
      => data.ProgramRobots.Data.FirstOrDefault(x => x.ProgramRobotID == id)?.Program;

    public async Task CreateProgramRobotAsync(Robot robot, Program program)
    => 
      await data.ProgramRobots.AddAsync(new ProgramRobot
      {
        Program = program,
        Robot = robot,
        CurrentVersion = program.ActualVersion
      });

    public IEnumerable<ProgramRobot> GetRobotProgramsByRobotIdAsync(int id)
      => data.ProgramRobots.Data.Where(x => x.RobotID == id);

    public async Task UpdateProgramRobotAsync(int programRobotId)
    {
      var programRobot = data.ProgramRobots.Data.FirstOrDefault(x => x.ProgramRobotID == programRobotId);
      if (programRobot != null)
      {
        programRobot.CurrentVersion = programRobot.Program.ActualVersion;
        await data.ProgramRobots.UpdateAsync(programRobot);
      }
    }

    public async Task RemoveProgramRobotAsync(int programRobotId)
    {
      var programRobot = data.ProgramRobots.Data.FirstOrDefault(x => x.ProgramRobotID == programRobotId);
      if (programRobot != null)
      {
        await data.ProgramRobots.RemoveAsync(programRobot);
      }
    }
  }
}
