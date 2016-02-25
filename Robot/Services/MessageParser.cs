using System;
using System.Threading.Tasks;
using Robot.Models;

namespace Robot.Services
{
  public class MessageParser
  {
    private Form1 form;
    private enum OperationCategory { None, Program }
    private enum OperationType { None, GetAll }

    private ProgramManager programManager = new ProgramManager();

    public MessageParser(Form1 f)
    {
      form = f;
    }

    public async Task ParseCommand (string input)
    {
      /*
      Комманда состоит из 2х чисел, подробнее здесь:
      https://github.com/qreal/robostore.net/wiki/%D0%9F%D1%80%D0%BE%D1%82%D0%BE%D0%BA%D0%BE%D0%BB-%D0%A1%D0%B5%D1%80%D0%B2%D0%B5%D1%80-%D0%A0%D0%BE%D0%B1%D0%BE%D1%82
      */
      var category = (OperationCategory) int.Parse(input.Substring(0,1));
      var command = (OperationType)int.Parse(input.Substring(1, 1));

      switch (category)
      {
          case OperationCategory.Program:
          switch (command)
          {
              case OperationType.GetAll:
              form.FormWriteLine("Parser: we need to get some programms");
              form.OutputProgram(await programManager.GetProgramAsync());
              break;
            case OperationType.None:
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
          break;
        case OperationCategory.None:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
