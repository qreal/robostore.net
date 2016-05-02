using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Store.Models.API;
using Store.Models.Command;
using Store.Models.Program;
using Tests.DB;

namespace Tests.API
{
  [TestClass]
  public class APIControllerTest : APITest
  {
    [TestMethod]
    public void RegistraterRobotApiTest()
    {
      Robot last = null;
      var amount = context.Robots.Count();
      try
      {
        // регистрация нового робота
        using (var wb = new WebClient())
        {
          wb.Encoding = Encoding.UTF8;
          var json = wb.DownloadString(serverUrl + "/api/robotRegistration");
          var resp = JsonConvert.DeserializeObject<RobotRegistrationOutput>(json);
          Assert.AreEqual(amount + 1, context.Robots.Count());
          last = context.Robots.ToList().Last();
          Assert.AreEqual(resp.ActivateCode, last.ActivationCode);
          Assert.AreEqual(resp.RobotId, last.RobotID);
        }
      }
      finally
      {
        context.Robots.Remove(last);
        context.SaveChanges();
        Assert.AreEqual(amount, context.Robots.Count());
      }
    }

    [TestMethod]
    public void GetProgramApiTest()
    {
      /*
      . создать программу
      . получить и проверить программу
      . удалить программу
      */

      var program = new Program
      {
        Code = MoqDataGenerator.GetRandomString(100),
        Name = MoqDataGenerator.GetRandomString(10)
      };
      context.Programs.Add(program);
      var amount = context.Programs.Count();
      context.SaveChanges();
      Assert.AreEqual(amount, context.Programs.Count() - 1);

      try
      {
        using (var wb = new WebClient())
        {
          wb.Encoding = Encoding.UTF8;
          var json = wb.DownloadString(serverUrl + "/api/GetProgram?programId=" + program.ProgramID);
          var resp = JsonConvert.DeserializeObject<ProgramExport>(json);
          Assert.AreEqual(resp.Name, program.Name);
          Assert.AreEqual(resp.Code, program.Code);
          Assert.AreEqual(resp.ProgramId, program.ProgramID);
        }
      }
      finally
      {
        context.Programs.Remove(program);
        context.SaveChanges();
        Assert.AreEqual(amount, context.Programs.Count());
      }
    }

    [TestMethod]
    public async Task ReportCommandExecutedApiTest()
    {
      /*
      - создать комманду и робота
      - проверить функцию
      - удалить робота, команда уйдет каскадно
      */
      var amountRobots = context.Robots.Count();
      var amountCommands = context.RobotCommands.Count();
      var robot = new Robot();
      var command = new RobotCommand
      {
        Robot = robot,
        Received = false
      };
      context.Robots.Add(robot);
      context.RobotCommands.Add(command);
      context.SaveChanges();
      var commandId = command.RobotCommandID;
      try
      {
        using (var client = new HttpClient())
        {
          var values = new Dictionary<string, string>
          {
            {"CommandId", "" + command.RobotCommandID}
          };
          var content = new FormUrlEncodedContent(values);
          await client.PostAsync(serverUrl + "/api/ReportCommandExecuted", content);

          context.RefreshModified();

          Assert.AreEqual(amountCommands, context.RobotCommands.Count());
          Assert.AreEqual(null, context.RobotCommands.FirstOrDefault(x => x.RobotCommandID == commandId));
        }
      }
      finally
      {
        context.Robots.Remove(robot);
        context.SaveChanges();
        Assert.AreEqual(amountRobots, context.Robots.Count());
        Assert.AreEqual(amountCommands, context.RobotCommands.Count());
      }
    }

    [TestMethod]
    public async Task ReportCommandGotApiTest()
    {
      /*
      - создать комманду и робота
      - проверить функцию
      - удалить робота, команда уйдет каскадно
      */

      var amountRobots = context.Robots.Count();
      var amountCommands = context.RobotCommands.Count();
      var robot = new Robot();
      var command = new RobotCommand
      {
        Robot = robot,
        Received = false
      };
      context.Robots.Add(robot);
      context.RobotCommands.Add(command);
      context.SaveChanges();
      try
      {
        using (var client = new HttpClient())
        {
          var values = new Dictionary<string, string>
          {
            {"CommandId", "" + command.RobotCommandID}
          };
          var content = new FormUrlEncodedContent(values);
          await client.PostAsync(serverUrl + "/api/ReportCommandGot", content);
          var result = (bool)context.Entry(command).GetDatabaseValues()["Received"];
          Assert.AreEqual(result, true);
        }
      }
      finally
      {
        context.Robots.Remove(robot);
        context.SaveChanges();
        Assert.AreEqual(amountRobots, context.Robots.Count());
        Assert.AreEqual(amountCommands, context.RobotCommands.Count());
      }

  }

    [TestMethod]
    public void GetCommandsApiTest()
    {
     
      /*
      . создать нового робота и комманду
      . получить эту комманду через апи
      . удалить нового робота и комманду
      */

      var amountRobots = context.Robots.Count();
      var amountCommands = context.RobotCommands.Count(); 
      var robot = new Robot();
      var command = new RobotCommand
      {
        Argument = MoqDataGenerator.GetRandomNumber(1, 10),
        Type = MoqDataGenerator.GetRandomNumber(1, 10),
        Robot = robot
      };
      context.Robots.Add(robot);
      context.RobotCommands.Add(command);
      context.SaveChanges();
      Assert.AreEqual(amountCommands, context.RobotCommands.Count() - 1);
      Assert.AreEqual(amountRobots, context.Robots.Count() - 1);

      try
      {
        using (var wb = new WebClient())
        {
          wb.Encoding = Encoding.UTF8;
          var json = wb.DownloadString(serverUrl + "/api/GetCommands?robotId=" + robot.RobotID);
          var resp = JsonConvert.DeserializeObject<IEnumerable<GetCommandsOutput>>(json);
          Assert.AreEqual(1, resp.Count());
          var comm = resp.First();
          Assert.AreEqual(comm.RobotCommandID, command.RobotCommandID);
          Assert.AreEqual(comm.Type, command.Type);
          Assert.AreEqual(comm.Argument, command.Argument);
        }
      }
      finally
      {
        context.Robots.Remove(robot);
        context.RobotCommands.Remove(command);
        context.SaveChanges();
        Assert.AreEqual(amountCommands, context.RobotCommands.Count());
        Assert.AreEqual(amountRobots, context.Robots.Count());
      }
    }
  }
}
