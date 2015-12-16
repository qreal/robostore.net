using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
  [Table("Messages")]
  public class StoredMessageE
  {
    [Key]
    public int MessageID { get; set; }
    public string Text { get; set; }
    public RobotE Robot { get; set; }
    public int RobotID { get; set; }
  }
}
