using System.Text;
using System.Web.Mvc;
using BookShop.WebUI.Models;
using System;

namespace BookShop.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PaggingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                tag.InnerHtml = i.ToString();
                result.Append(tag.ToString());
                
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}