using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Framework.Web.MVC
{
    public static class Extensions
    {
        #region| Methods |

        public static IHtmlString ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            var link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions).ToHtmlString();
            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }

        public static string Truncate(this HtmlHelper helper, string @string, int length)
        {
            if (@string.Length <= length)
            {
                return @string;
            }
            else
            {
                return @string.Substring(0, length) + "...";
            }
        }

        public static void Save(this HttpCookie @HttpCookie)
        {
            HttpContext.Current.Response.Cookies.Add(@HttpCookie);
        }

        #endregion
    }
}
