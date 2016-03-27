using System;
using System.Reflection;

/*
Взял отсюда:
https://ru.wikipedia.org/wiki/%D0%9E%D0%B4%D0%B8%D0%BD%D0%BE%D1%87%D0%BA%D0%B0_(%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD_%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D1%8F)#.D0.9F.D1.80.D0.B8.D0.BC.D0.B5.D1.80_.D0.BD.D0.B0_C.23
*/

namespace Domain.Services
{
  /// generic Singleton<T> (потокобезопасный с использованием generic-класса и с отложенной инициализацией)
  /// <typeparam name="T">Singleton class</typeparam>
  public class Singleton<T> where T : class
  {
    /// Защищённый конструктор необходим для того, чтобы предотвратить создание экземпляра класса Singleton. 
    /// Он будет вызван из закрытого конструктора наследственного класса.
    protected Singleton()
    {
    }

    /// Фабрика используется для отложенной инициализации экземпляра класса
    private sealed class SingletonCreator<S> where S : class
    {
      //Используется Reflection для создания экземпляра класса без публичного конструктора
      private static readonly S instance = (S) typeof (S).GetConstructor(
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new Type[0],
        new ParameterModifier[0]).Invoke(null);

      public static S CreatorInstance
      {
        get { return instance; }
      }
    }

    public static T Instance
    {
      get { return SingletonCreator<T>.CreatorInstance; }
    }
  }
}
