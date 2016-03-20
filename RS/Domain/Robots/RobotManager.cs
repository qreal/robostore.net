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

    public async Task<Robot> CreateRobot()
    {
      var robot = await data.AddAsync(new Robot
      {
        ActivationCode = 0
      }) as Robot;
      robot.ActivationCode = robot.RobotID;
      await data.UpdateAsync(robot);
      return robot;
    }

    public Robot GetRobotById(int id)
      => data.Robots.FirstOrDefault(x => x.RobotID == id);

    public Robot GetRobotByActivationCode(int code)
      => data.Robots.FirstOrDefault(x => x.ActivationCode == code);

    public async Task BindRobotToUser(Robot robot, User user)
    {
      robot.UserID = user.UserID;
      await data.UpdateAsync(robot);
    }

    public IEnumerable<Robot> GetMyRobots(User user)
      => data.Robots.Where(x => x.UserID == user.UserID);
  }
}
