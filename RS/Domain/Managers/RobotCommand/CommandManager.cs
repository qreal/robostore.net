using System.Collections.Generic;
using System.Linq;
using Domain.Data;
using Domain.Entities;

namespace Domain.Managers.RobotCommand
{
    public class CommandManager
    {
        private IData data;

        public CommandManager(IData d)
        {
            data = d;
        }

        public void CreateRobotCommand(Robot robot, Program program, RobotCommandTypes commandType)
            => data.RobotCommands.Add(new Entities.RobotCommand
            {
                Argument = program.ProgramID,
                Robot = robot,
                Type = (int) commandType
            });

        public IEnumerable<Entities.RobotCommand> GetNotReceivedCommandsByRobotId(int robotId)
            => data.RobotCommands.Data.Where(x => x.RobotID == robotId && x.Received == false);

        public void SetCommandReceived(int commandId)
        {
            var command = GetCommandById(commandId);
            command.Received = true;
            data.RobotCommands.Update(command);
        }

        public void RemoveExecutedProgram(int commandId)
            => data.RobotCommands.Remove(GetCommandById(commandId));

        private Entities.RobotCommand GetCommandById(int commandId)
            => data.RobotCommands.Data.First(x => x.RobotCommandID == commandId);
    }
}
