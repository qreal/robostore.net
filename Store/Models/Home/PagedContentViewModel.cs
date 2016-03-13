using System.Collections.Generic;
using Domain.Pagination;

namespace Store.Models.Home
{
  public class PagedContentViewModel<ContentType>
  {
    public IEnumerable<ContentType>  PageContent { get; set; }
    public PagingInfo PagingInfo { get; set; }
  }
}
