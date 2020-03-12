using BGLB.MerberPoint.Business;
using System.Web.Mvc;

namespace BGLB.MerberPoint.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UsersServive _AirportServive = new UsersServive();

        public ActionResult Index()
        {
            var userModel = _AirportServive.GetEntity(e => e.U_LoginName == "BGLB");

            return Json(userModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}