using BGLB.MerberPoint.Business;
using BGLB.MerberPoint.Common;
using BGLB.MerberPoint.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGLB.MerberPoint.Web.Controllers
{
    public class MeberCardController : Controller
    {
        private CardLevelService _CardLevelService = new CardLevelService();
         /// <summary>
         /// 加载会员列表
         /// </summary>
         /// <returns></returns>
        public ActionResult Index()
        {
            //获取会员等级信息
            var cardLevels = _CardLevelService.GetList(e => true).Select(e=>new SelectListItem() { Text = e.CL_LevelName,Value = e.CL_ID.ToString()}).ToList();
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


            return View();
        }

    }
}