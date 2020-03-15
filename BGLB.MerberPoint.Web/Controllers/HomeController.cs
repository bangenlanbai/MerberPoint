using BGLB.MerberPoint.Business;
using BGLB.MerberPoint.Entity.ViewModel;
using System.Web.Mvc;

namespace BGLB.MerberPoint.Web.Controllers
{
    public class HomeController : Controller
    {
        private UsersServive _UsersServive = new UsersServive();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _UsersServive.Login(viewModel);
                if (result.IsSuccess)
                {
                    var returnUrl = Request["ReturnUrl"];
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "网络繁忙");
                }
                return View();
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误");
                return View();
            }

        }

        public ActionResult Logout()
        {
            _UsersServive.Logout();
            return RedirectToAction("Login");
        }

    }
}