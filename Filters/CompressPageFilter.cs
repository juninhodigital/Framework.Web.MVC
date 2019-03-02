using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.Web.MVC
{
   public class CompressPageFilter: ActionFilterAttribute
    {
        #region| Events |

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var oResponse = filterContext.HttpContext.Response;

            oResponse.Filter = new GZipStream(oResponse.Filter, CompressionMode.Compress);

            oResponse.AppendHeader("Content-encoding", "gzip");
            oResponse.Cache.VaryByHeaders["Accept-encoding"] = true;
        } 

        #endregion
    }
}