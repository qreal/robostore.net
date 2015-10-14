using System.Collections.Generic;
using Robots.Domain.Entities;

/*
Используем паттерн репозиторий, чтобы скрыть как мы храним и откуда берем данные.
Реализация интерфейса будет получена от Ninject
Один  интерфейс репозитори для всех сущностей из БД.
*/

namespace Robots.Domain.Abstract
{
  public interface ICommonRepository<T>
  {
    IEnumerable<T> Data { get; }
    void SaveData(T data);
    T DeleteData(int ID);
  }
}
