using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;

namespace umbraco_headless_cms.library.Authorization
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _enabled;
        private readonly string _userName;
        private readonly string _userPassword;

        public BasicAuthorizeAttribute() : this(
            Convert.ToBoolean(ConfigurationManager.AppSettings["authEnabled"]),
            ConfigurationManager.AppSettings["authUsername"],
            ConfigurationManager.AppSettings["authUserpassword"])
        {
        }

        public BasicAuthorizeAttribute(bool enabled, string userName, string userPassword)
        {
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            if (userPassword == null) throw new ArgumentNullException(nameof(userPassword));
            _enabled = enabled;
            _userName = userName;
            _userPassword = userPassword;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!_enabled)
            {
                return;
            }

            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            var auth = filterContext.HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(auth))
            {
                var encodedDataAsBytes = Convert.FromBase64String(auth.Replace("Basic ", ""));
                var value = Encoding.ASCII.GetString(encodedDataAsBytes);
                var username = value.Substring(0, value.IndexOf(':'));
                var password = value.Substring(value.IndexOf(':') + 1);

                if (ValidateUser(username, password))
                {
                    filterContext.HttpContext.User = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    UnautorizedResponse(filterContext, "401, wrong username/password");
                }
            }
            else
            {
                UnautorizedResponse(filterContext, "401, please authenticate");
            }
        }

        private bool ValidateUser(string username, string userpassword)
        {
            return username == _userName && userpassword == _userPassword;
        }

        private static void UnautorizedResponse(AuthorizationContext authorizationContext, string message)
        {
            authorizationContext.HttpContext.Response.Clear();
            authorizationContext.HttpContext.Response.StatusDescription = "Unauthorized";
            // TODO: Should we actually set WWW-Authenticate header?
            authorizationContext.HttpContext.Response.AddHeader("WWW-Authenticate", "Basic realm=\"Secure Area\"");
            authorizationContext.HttpContext.Response.Write(message);
            authorizationContext.HttpContext.Response.StatusCode = 401;
            authorizationContext.Result = new EmptyResult();
            authorizationContext.HttpContext.Response.End();
        }
    }
}