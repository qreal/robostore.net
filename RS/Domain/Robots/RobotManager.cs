using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;

namespace Domain.Robots
{
  public class RobotManager
  {
    private IData data;
    public static int ActivationCodeMin { get; } = 0;
    public static int ActivationCodeMax { get; } = 9999;

    public RobotManager(IData d)
    {
      data = d;
    }

    public async Task<Robot> CreateRobot()
      => await data.AddAsync(new Robot
      {
        ActivationCode = MoqDataGenerator.GetRandomNumber(ActivationCodeMin, ActivationCodeMax)
      }) as Robot;
  }
}
