using BGLB.MerberPoint.Common;
using BGLB.MerberPoint.Entity.DTOModel;
using BGLB.MerberPoint.Entity.POCOModel;
using BGLB.MerberPoint.Entity.ViewModel;
using System;
using System.Web;
using System.Web.Security;

namespace BGLB.MerberPoint.Business
{
    public class UsersServive : BaseService<Users>
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public OperateResult Login(LoginViewModel viewModel)
        {
            var model = Find(e => e.U_LoginName == viewModel.UserName && e.U_Password == viewModel.Password);
            if (model == null)
            {
                return new OperateResult() { IsSuccess = false,Msg="用户名和密码不匹配"};
            }
            else
            {
                //登陆成功 将用户信息写入Cookie中
                var dtoModel = new LoginUserDTOModel() {
                    Id = model.U_ID,
                    UserName = model.U_LoginName,
                    RealName = model.U_RealName
                };
                SetUserDate(dtoModel);
                return new OperateResult() { IsSuccess = true, Msg = "登陆成功" };
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public void Logout()
        {
            //删除票据
            FormsAuthentication.SignOut();

            //清除cookie
            HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }

        //生成cookie
        private void SetUserDate(LoginUserDTOModel loginUserDTOModel)
        {
            //把用户信息转为json字符串
            var userData = loginUserDTOModel.ToJson();//扩展方法自己封装的时候 参数传一个this;
            // 创建票据FormsAuthenticationTicket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,"loginUser",DateTime.Now, DateTime.Now.AddDays(1),false,userData);
            // 创建Cookie HttpCookie FormsAuthentication
            var ticketEncrypt =  FormsAuthentication.Encrypt(ticket);
            //创建Cookie 根据web.config里面的authentication 节点进行cookie 创建
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypt)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
};
            // 获取http请求上下文
            HttpContext context = HttpContext.Current;
            if (context == null)
            {
                throw new ArgumentNullException("context为空");//抛出异常
            }
            context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);            //先remove在写入
            context.Response.Cookies.Add(cookie);            //写入cookie
         }

    }
}
