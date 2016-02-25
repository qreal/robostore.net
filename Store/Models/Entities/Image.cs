using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models.Entities
{
  public class Image
  {
    public int ImageID { get; set; }
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
  }
}
