using System.Data.Entity.Infrastructure;
using Domain.Entities;
using Domain.Services;

/*
Используется паттерн singeton
*/

namespace Domain.Data
{
    public class Data : Singleton<Data>, IData
    {
        private DataContext _context = new DataContext();

        public IRepository<Configuration> Configurations { get; private set; }
        public IRepository<Robot> Robots { get; private set; }
        public IRepository<Program> Programs { get; private set; }
        public IRepository<ProgramRobot> ProgramRobots { get; private set; }
        public IRepository<User> Users { get; private set; }
        public IRepository<RobotCommand> RobotCommands { get; private set; }
        public IRepository<Image> Images { get; private set; }

        /*
         * Иногда нужно обновить данные руками
         * */
        public void Reload()
        {
            _context.Dispose();
            _context = new DataContext();
            Configurations = new EFRepository<Configuration>(_context);
            Robots = new EFRepository<Robot>(_context);
            Programs = new EFRepository<Program>(_context);
            ProgramRobots = new EFRepository<ProgramRobot>(_context);
            Users = new EFRepository<User>(_context);
            RobotCommands = new EFRepository<RobotCommand>(_context);
            Images = new EFRepository<Image>(_context);
        }

        private Data()
        {
            Configurations = new EFRepository<Configuration>(_context);
            Robots = new EFRepository<Robot>(_context);
            Programs = new EFRepository<Program>(_context);
            ProgramRobots = new EFRepository<ProgramRobot>(_context);
            Users = new EFRepository<User>(_context);
            RobotCommands = new EFRepository<RobotCommand>(_context);
            Images = new EFRepository<Image>(_context);
        }
    }
}
