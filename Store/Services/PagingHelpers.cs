using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Store.Models.Entities;
using Store.Models.Services;

/*
Класс генерирует строку из номеров страниц с выделением текущей страницы
*/

namespace Store.Services
{
  public static class PagingHelpers
  {
    public static MvcHtmlString FormPageLinks(this HtmlHelper html,
                                               PagingInfo pagingInfo,
                                               Func<int, string> pageUrl)
    {
      StringBuilder result = new StringBuilder();
      for (int i = 1; i <= pagingInfo.TotalPages; i++)
      {
        TagBuilder tag = new TagBuilder("a");
        tag.MergeAttribute("href", pageUrl(i));
        tag.InnerHtml = i.ToString();
        if (i == pagingInfo.CurrentPage)
        {
          tag.AddCssClass("selected");
          tag.AddCssClass("btn-primary");
        }
        tag.AddCssClass("btn btn-default");
        result.Append(tag);
      }
      return MvcHtmlString.Create(result.ToString());
    }

  }
}
