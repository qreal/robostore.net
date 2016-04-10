using System.Collections.Generic;
using System.Linq;
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

    public void CreateProgramRobot(Robot robot, Program program)
    => 
      data.ProgramRobots.Add(new ProgramRobot
      {
        Program = program,
        Robot = robot,
        CurrentVersion = program.ActualVersion
      });

    public IEnumerable<ProgramRobot> GetRobotProgramsByRobotIdAsync(int id)
      => data.ProgramRobots.Data.Where(x => x.RobotID == id);

    public void UpdateProgramRobotAsync(int programRobotId)
    {
      var programRobot = data.ProgramRobots.Data.FirstOrDefault(x => x.ProgramRobotID == programRobotId);
      if (programRobot != null)
      {
        programRobot.CurrentVersion = programRobot.Program.ActualVersion;
        data.ProgramRobots.Update(programRobot);
      }
    }

    public void RemoveProgramRobotAsync(int programRobotId)
    {
      var programRobot = data.ProgramRobots.Data.FirstOrDefault(x => x.ProgramRobotID == programRobotId);
      if (programRobot != null)
      {
        data.ProgramRobots.Remove(programRobot);
      }
    }
  }
}
