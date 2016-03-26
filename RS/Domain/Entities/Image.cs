namespace Domain.Entities
{
  public class Image
  {
    public int ImageID { get; set; }
    public byte[] ImageData { get; set; }
    public string ImageMimeType { get; set; }
    public string Name { get; set; }
  }
}
