using Domain.Entities;

namespace Domain.Data
{
    public interface IData
    {
        IRepository<Configuration> Configurations { get; }
        IRepository<Robot> Robots { get; }
        IRepository<Program> Programs { get; }
        IRepository<ProgramRobot> ProgramRobots { get; }
        IRepository<User> Users { get; }
        IRepository<RobotCommand> RobotCommands { get; }
        IRepository<Image> Images { get; }
        void Reload();

    }
}
