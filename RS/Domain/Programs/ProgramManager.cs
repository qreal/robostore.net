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

    public Program GetProgramByProgramRobotId(int id)
      => data.ProgramRobots.FirstOrDefault(x => x.ProgramRobotID == id)?.Program;

    public async Task CreateProgramRobotAsync(Robot robot, Program program)
    => 
      await data.AddAsync(new ProgramRobot
      {
        Program = program,
        Robot = robot,
        CurrentVersion = program.ActualVersion
      });

    public IEnumerable<ProgramRobot> GetRobotProgramsByRobotIdAsync(int id)
      => data.ProgramRobots.Where(x => x.RobotID == id);

    public async Task UpdateProgramRobotAsync(int programRobotId)
    {
      var programRobot = data.ProgramRobots.FirstOrDefault(x => x.ProgramRobotID == programRobotId);
      if (programRobot != null)
      {
        programRobot.CurrentVersion = programRobot.Program.ActualVersion;
        await data.UpdateAsync(programRobot);
      }
    }

    public async Task RemoveProgramRobotAsync(int programRobotId)
    {
      var programRobot = data.ProgramRobots.FirstOrDefault(x => x.ProgramRobotID == programRobotId);
      if (programRobot != null)
      {
        await data.RemoveAsync(programRobot);
      }
    }
  }
}
