using BGLB.MerberPoint.Common;
using BGLB.MerberPoint.Entity.DTOModel;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace BGLB.MerberPoint.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)

        {
            //通过sender 获取HttpApplication
            HttpApplication app = sender as HttpApplication;
            // 拿到HTTP上下文
            HttpContext context = app.Context;
            //根据FormsAuthentication.FormsCookieName从上下文请求中获取到cookie
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                //解密cookie.Value 获得票据
                if (!string.IsNullOrWhiteSpace(cookie.Value))
                {
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //判断票据的userdata,如果不为空则反序列化为实体
                    if (!string.IsNullOrWhiteSpace(ticket.UserData))
                    {
                        var dtoModel = ticket.UserData.ToObject<LoginUserDTOModel>();
                        //将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData
                        context.User = new MyFormsPrincipal<LoginUserDTOModel>(ticket, dtoModel);
                    }
                }
                

            }

        }
    }
}
