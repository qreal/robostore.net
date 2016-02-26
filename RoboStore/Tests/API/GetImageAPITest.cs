using System;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DB;

namespace Tests.API
{
  [TestClass]
  public class GetImageAPITest : APITest
  {
    private Image image = new Image();

    [TestMethod]
    public void GetImageApiTest()
    {
      // загрузиили тестовую картинку в БД
      LoadNewImageToDB();

      try
      {
        // попробуем скачать ее
        DownloadTestImage(image);

        // проверим, что скачалась она
        CheckDownloadedImage();
      }
      finally
      {
        RemoveTestData();
      }
      

    }

    private void RemoveTestData()
    {
      context.Images.Remove(image);
      context.SaveChanges();

      // если винда не даст удалить, нам все равно, пусть висит, потом перезапишем просто
      try
      {
        File.Delete("image_from_server.png");
      }
      catch (Exception e)
      {
        // ignored
      }
    }

    private void CheckDownloadedImage()
    {
      // получим размер и тип второй картинки
      FileInfo fileInfo1 = new FileInfo("program.png");
      FileInfo fileInfo2 = new FileInfo("image_from_server.png");
      Assert.AreEqual(fileInfo1.Length, fileInfo2.Length);
      Assert.AreEqual(fileInfo1.Extension, fileInfo2.Extension);
    }

    private void LoadNewImageToDB()
    { 

      using (FileStream fsSource = new FileStream("program.png",
        FileMode.Open, FileAccess.Read))
      {
        byte[] bytes = new byte[fsSource.Length];
        int numBytesToRead = (int)fsSource.Length;
        int numBytesRead = 0;
        while (numBytesToRead > 0)
        {
          // Read may return anything from 0 to numBytesToRead.
          int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

          // Break when the end of the file is reached.
          if (n == 0)
            break;

          numBytesRead += n;
          numBytesToRead -= n;
        }

        // сохраняем картинку в БД
        image.ImageData = bytes;
        // получаем MimeType файла
        image.ImageMimeType = MimeMapping.GetMimeMapping("program.png");
        context.Images.Add(image);
        context.SaveChanges();
      }
    }

    private void DownloadTestImage(Image image)
    {
      using (WebClient webClient = new WebClient())
      {
        webClient.DownloadFile("http://robstore.azurewebsites.net/Home/GetImage?programId=" +
                                image.ImageID, "image_from_server.png");
      }
    }


  }
}
