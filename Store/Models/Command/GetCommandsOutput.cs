namespace Store.Models.Command
{
  public class GetCommandsOutput
  {
    public int RobotCommandID { get; set; }
    public int Type { get; set; }
    public int Argument { get; set; }
  }
}
