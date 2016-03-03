using System;
using System.Collections.Generic;
using Store.Models.Services;

namespace Store.ViewModels.Home
{
  public class ContentListViewModel <ContentType>
  {
    public IEnumerable<ContentType>  Content { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
