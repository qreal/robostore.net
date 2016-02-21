using System;
using System.Text;

namespace Tests.Services
{
  /*
  Класс используется для генерации тестовых данных
  */

  public static class MoqDataGenerator
  {
    // генератор рандомных чисел
    private static Random random = new Random((int)DateTime.Now.Ticks);

    // генератор рандомного числа
    public static int GetRandomNumber(int min, int max) => random.Next(min, max);

    // Генератор рандомной строки из заглавных латинских букв
    public static string GetRandomString(int size)
    {
      StringBuilder builder = new StringBuilder();
      char ch;
      for (int i = 0; i < size; i++)
      {
        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        builder.Append(ch);
      }
      return builder.ToString();
    }

  }
}
