using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace BGLB.MerberPoint.Entity.DTOModel
{
    public class MyFormsPrincipal<T> : IPrincipal where T : class, new()
    {
        public IIdentity Identity
        {
            get;
            private set;
        }
        public T UserData { get; private set; }
        public MyFormsPrincipal(FormsAuthenticationTicket ticket, T userData)
        {
            this.Identity = new FormsIdentity(ticket);
            this.UserData = userData;
        }
        public bool IsInRole(string role)
        {
            IPrincipal principal = UserData as IPrincipal;
            if (principal == null)
            {
                throw new ArgumentNullException("principal为空");
            }
            return principal.IsInRole(role);

        }
    }
}
