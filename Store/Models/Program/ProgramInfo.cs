/*
Этот класс используется для передачи программы во View()
И пересылки данных о программе - id Робота и Программы обработчику View() 
*/

namespace Store.Models.Program
{
  public class ProgramInfo
  {
    public Domain.Entities.Program Program { get; set; }
    public int  SelectedRobot { get; set; }
    public int ProgramId { get; set; }
  }
}
