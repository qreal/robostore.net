/*
Класс Робот
У Агеева Robot, RobotInfo и RobotWrapper - 3 разных класса, я же пока не вижу причин так делать
Todo: разобраться с использованием
Todo: статус - string стоит ли?
*/

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc;

namespace Roboto.Models.Entities
{
  public class Robot
  {
    [HiddenInput(DisplayValue = false)]
    public int RobotID { get; set; }
    [Required(ErrorMessage = "Please enter a name")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter a SSID")]
    public string SSID { get; set; }
    [Required(ErrorMessage = "Please enter an UserID")]
    public int UserID { get; set; }
    /*
    Хз зачем, пока эти 3 штуки
    */
    [Required(ErrorMessage = "Please enter a Model Configuration")]
    public string ModelConfig { get; set; }
    [Required(ErrorMessage = "Please enter a System Configuration")]
    public string SystemConfig { get; set; }
    [Required(ErrorMessage = "Please enter a Program")]
    public string Program { get; set; }
    [Required(ErrorMessage = "Please enter a Status")]
    public string Status { get; set; }
  }
}