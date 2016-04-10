using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;

namespace Domain.Robots
{
  public class RobotManager
  {
    private IData data;

    public RobotManager(IData d)
    {
      data = d;
    }

    /*
      Пока что код активации и есть Id робота в базе.
      Потом можно придумать что-то умнее
    */

    public Robot CreateRobot()
    {
      var robot = new Robot
      {
        ActivationCode = 0
      };
      data.Robots.Add(robot);
      robot.ActivationCode = robot.RobotID;
      data.Robots.Update(robot);
      return robot;
    }

    public Robot GetRobotById(int id)
      => data.Robots.Data.FirstOrDefault(x => x.RobotID == id);

    public Robot GetRobotByProgramRobotId(int id)
      => data.ProgramRobots.Data.FirstOrDefault(x => x.ProgramRobotID == id)?.Robot;

    public Robot GetRobotByActivationCode(int code)
      => data.Robots.Data.FirstOrDefault(x => x.ActivationCode == code);

    public void BindRobotToUser(Robot robot, User user)
    {
      robot.UserID = user.UserID;
      data.Robots.Update(robot);
    }

    public IEnumerable<Robot> GetMyRobots(User user)
      => data.Robots.Data.Where(x => x.UserID == user.UserID);
  }
}
