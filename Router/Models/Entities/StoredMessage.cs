using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Router.Models.Entities
{
  [Table("Messages")]
  public class StoredMessage
  {
    [Key]
    public int MessageID { get; set; }
    public string Text { get; set; }
    public Robot Robot { get; set; }
    public int RobotID { get; set; }
  }
}
