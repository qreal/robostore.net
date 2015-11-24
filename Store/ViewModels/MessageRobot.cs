using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ViewModels
{
  public class MessageRobot
  {
    [Required]
    public string Text { get; set; }
    [Required]
    public int RobotID { get; set; }
  }
}
