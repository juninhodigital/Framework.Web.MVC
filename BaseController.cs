using System;
using System.Web;
using System.Web.Mvc;

using Framework.Core;

namespace Framework.Web.MVC
{
    public abstract class BaseController: Controller
    {
        #region| Properties |

        protected HttpContext Web
        {
            get
            {
                return System.Web.HttpContext.Current;
            }
            set
            {
                System.Web.HttpContext.Current = value;
            }
        }


        #endregion

        #region| Constructor |

        public BaseController()
        {

        }

        #endregion

        #region| Methods |

        public string Encode(string @string)
        {
            return Server.UrlEncode(@string);
        }

        public string Decode(string @string)
        {
            return Server.UrlDecode(@string);
        }

        protected string GetAppSettings(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[name];
        }

        protected HttpCookie GetCookie(string name)
        {
            var oCookie = Request.Cookies[name];

            return oCookie;
        }

        protected bool HasCookie(string name)
        {
            var oCookie = Request.Cookies[name];

            if (oCookie.IsNull())
            {
                return false;
            }
            else
            {
                oCookie = null;
                return true;
            }
        }

        protected void ClearCookie(string name)
        {
            var oCookie = Web.Request.Cookies[name];

            if (oCookie.IsNotNull())
            {
                oCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(oCookie);
            }

            oCookie = null;

        }

        public bool IsCookieEnabled()
        {
            var Name = "FrameworkSupportCookies";
            try
            {
                var oCookie = new HttpCookie(Name, "true");
                oCookie.Save();

                oCookie = null;
            }
            catch
            {
                return false;
            }

            if (HasCookie(Name) || Web.Request.Browser.Cookies)
            {
                ClearCookie(Name);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
