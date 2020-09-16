using BGLB.MerberPoint.Business;
using BGLB.MerberPoint.Common;
using BGLB.MerberPoint.Entity.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace BGLB.MerberPoint.Web.Controllers
{
    public class MeberCardController : Controller
    {
        private CardLevelService _CardLevelService = new CardLevelService();
        private MeberCardService _MeberCardService = new MeberCardService();
        /// <summary>
        /// 加载会员列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //获取会员等级信息
            var cardLevels = _CardLevelService.GetList(e => true).Select(e => new SelectListItem() { Text = e.CL_LevelName, Value = e.CL_ID.ToString() }).ToList();
            cardLevels.Insert(0, new SelectListItem() { Text = "全部", Value = "0" });
            ViewBag.CardLevel = cardLevels;

            //获取状态信息
            var cardTypeList = EnumHelper.EnumListDic<CardStateTypeEnum>("全部", "0");
            var cardTypeSelectList = new SelectList(cardTypeList, "value", "key");
            ViewBag.CardTypeSelectList = cardTypeSelectList;
            return View();

        }


        public ActionResult GetPagedMeberCardList(GetPagedMeberCardListViewModel viewModel)
        {

            var result = _MeberCardService.GetPagedMeberCardList(viewModel);
            return Json(result.Data);
        }


        public ActionResult Edit(int id = 0)
        {
            var cardLevels = _CardLevelService.GetList(e => true).Select(e => new SelectListItem() { Text = e.CL_LevelName, Value = e.CL_ID.ToString() }).ToList();
            cardLevels.Insert(0, new SelectListItem() { Text = "全部", Value = "0" });
            ViewBag.CardLevel = cardLevels;
            if (id > 0)
            {
                var result = _MeberCardService.GetMerberCardInfo(id.ToString());
                if (result.IsSuccess)
                {
                    return View(result.Data);
                }
                else
                {
                    return Content(result.Msg);
                    //return Redirect()
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Create(MerberCardEditViewModel viewModel)
        {
            var result = _MeberCardService.Create(viewModel);
            return Json(result);
        }

        public ActionResult Update(MerberCardEditViewModel viewModel)
        {
            var result = _MeberCardService.Update(viewModel);
            return Json(result);
        }

        public ActionResult Delete(int id)
        {
            var result = _MeberCardService.Delete(id);
            return Json(result);
        }

        public ActionResult SearchWithCardId(string id)
        {
            var result = _MeberCardService.GetMerberCardInfo(id);

            if (result.IsSuccess)
            {
                var jsondata = result.Data as MerberCardTransferViewModel;

                return Json(jsondata);
            }
            else
            {
                return Content(result.Msg);
                //return Redirect()
            }
        }

        public ActionResult Transfer(int id)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                var result = new MerberCardTransferViewModel();
                var memberCardInfo = _MeberCardService.Find(e => e.MC_CardID == id.ToString());

                if (memberCardInfo == null)
                {
                    return Content("查询出错！");

                }
                if (string.IsNullOrWhiteSpace(result.FromMerberCardId))
                {
                    result.FromMerberCardId = memberCardInfo.MC_CardID;
                    result.FromName = memberCardInfo.MC_Name;
                    result.FromPoint = (int)memberCardInfo.MC_Point;
                    result.FromTotalMoney = (float)memberCardInfo.MC_TotalMoney;
                }
                else
                {
                    result.ToMerberCardId = memberCardInfo.MC_CardID;

                    result.ToName = memberCardInfo.MC_Name;
                    result.ToPoint = (int)memberCardInfo.MC_Point;
                    result.ToTotalMoney = (float)memberCardInfo.MC_TotalMoney;
                    
                }
                return View(result);

            }



        }

        public ActionResult TransferTo(MerberCardTransferViewModel viewModel)
        {
            return View();

        }


    }
}