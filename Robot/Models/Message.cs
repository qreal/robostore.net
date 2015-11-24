using System.Collections.Generic;

namespace Robot
{
  public class Message
  {
    public string Server { get; set; }
    public Configuration Robot { get; set; }
    public List<string> Commands { get; set; }

    public string Text { get; set; }
  }
}
