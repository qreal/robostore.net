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
  }
}
