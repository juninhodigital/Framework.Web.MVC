using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Framework.Web.MVC
{
    public class RequiresSSLFilter : ActionFilterAttribute
    {
        #region| Events |

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oRequest  = filterContext.HttpContext.Request;
            var oResponse = filterContext.HttpContext.Response;

            //Check if we're secure or not and if we're on the local box
            if (!oRequest.IsSecureConnection && !oRequest.IsLocal)
            {
                var builder = new UriBuilder(oRequest.Url)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = 443
                };

                oResponse.Redirect(builder.Uri.ToString());
            }

            base.OnActionExecuting(filterContext);
        } 

        #endregion
    }
}
