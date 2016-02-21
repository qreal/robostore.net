using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Models.Managers;
using Tests.Data;

namespace Tests.Logic
{
  [TestClass]
  public class ProgramLogicTest : LogicTest
  {
    private readonly ProgramManager _manager;

    public ProgramLogicTest()
    {
      _manager = new ProgramManager(data);
    }

    [TestMethod]
    public void GetProgram()
    {
      var prg = data.Programs.First();
      var result = _manager.GetProgram(prg.ProgramID);
      Assert.AreNotEqual(null, result);
      Assert.AreEqual(result.Code, prg.Code);
      Assert.AreEqual(result.Name, prg.Name);
      Assert.AreEqual(result.ActualVersion, prg.ActualVersion);
    }
  }
}
