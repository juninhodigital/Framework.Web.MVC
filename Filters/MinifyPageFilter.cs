using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Framework.Web.MVC
{
    public class MinifyPageAttribute : ActionFilterAttribute
    {
        #region| Events |

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request  = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            response.Filter = new WhiteSpaceResponseFilter(response.Filter, s =>
            {
                //s = Regex.Replace(s, @"\s+", " "); // Se descomentar esta linha, os javascripts da página devem estar em arquivos externos e não inline
                s = Regex.Replace(s, @"\s*\n\s*", "\n");
                s = Regex.Replace(s, @"\s*\>\s*\<\s*", "><");
                s = Regex.Replace(s, @"<!--(.*?)-->", ""); // Remove comments
                s = Regex.Replace(s, @"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}", "");

                // single-line doctype must be preserved 
                var firstEndBracketPosition = s.IndexOf(">");
                if (firstEndBracketPosition >= 0)
                {
                    s = s.Remove(firstEndBracketPosition, 1);
                    s = s.Insert(firstEndBracketPosition, ">");
                }
                return s;
            });
        } 

        #endregion
    }
}
