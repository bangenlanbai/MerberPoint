using BGLB.MerberPoint.Common;
using BGLB.MerberPoint.Entity.POCOModel;
using BGLB.MerberPoint.Entity.ViewModel;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace BGLB.MerberPoint.Business
{
    public class MeberCardService : BaseService<MemCards>
    {


        public OperateResult GetPagedMeberCardList(GetPagedMeberCardListViewModel viewModel)
        {
            //总记录数
            int rowCount = 0;

            //当用户什么都没有输入的时候默认查询所有的
            var query = PredicateExtensions.True<MemCards>();
            //根据用户输入的参数信息实现多条件查询
            if (!string.IsNullOrWhiteSpace(viewModel.CardId))
            {
                query = query.And(e => e.MC_CardID == viewModel.CardId);
            }
            if (!string.IsNullOrWhiteSpace(viewModel.Name))
            {
                query = query.And(e => e.MC_Name.Contains(viewModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(viewModel.Mobile))
            {
                query = query.And(e => e.MC_Mobile == viewModel.Mobile);
            }
            if (viewModel.CardLevelId > 0)
            {
                query = query.And(e => e.CL_ID == viewModel.CardLevelId);
            }
            if (viewModel.State > 0)
            {
                query = query.And(e => e.MC_State == viewModel.State);
            }
            var pageData = GetList(viewModel.page, viewModel.rows, ref rowCount, query, e => e.MC_ID, false)
                .Select(e => new MeberCardListViewModel()
                {
                    CardId = e.MC_CardID,
                    CardLevelName = e.CardLevels.CL_LevelName,
                    CreateTime = (DateTime)e.MC_CreateTime,
                    Id = e.MC_ID,
                    Mobile = e.MC_Mobile,
                    Name = e.MC_Name,
                    Point = (int)e.MC_Point,
                    Sex = ((SexTypeEnum)e.MC_Sex).ToString(),
                    State = ((CardStateTypeEnum)e.MC_State).ToString(),
                    TotalMoney = (float)e.MC_TotalMoney
                }).AsQueryable().ToList();

            var dataGridViewModel = new DataGridViewModel() { total = rowCount, rows = pageData };

            return new OperateResult(true, "", dataGridViewModel);
        }

        /// <summary>
        /// 会员的添加
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public OperateResult Create(MerberCardEditViewModel viewModel)
        {
            var model = new MemCards
            {
                CL_ID = viewModel.CardLevelId,
                MC_BirthdayType = (byte)viewModel.BirthdayType,
                MC_Birthday_Day = viewModel.BirthdayDay,
                MC_Birthday_Month = viewModel.BirthdayMonth,
                MC_IsPast = Convert.ToByte(viewModel.IsPast),
                MC_IsPointAuto = Convert.ToByte(viewModel.IsPointAuto),
                MC_Point = viewModel.Point,
                MC_CardID = viewModel.CardId,
                MC_Mobile = viewModel.Mobile,
                MC_Money = (float)viewModel.Money,
                MC_Sex = (int)viewModel.Sex,
                MC_State = (int)viewModel.CardState,
                MC_RefererCard = viewModel.RefererCard,
                MC_CreateTime = DateTime.Now,
                MC_Password = viewModel.Password,
                MC_PastTime = viewModel.PastTime,
                MC_Name = viewModel.Name

            };
            var refererResult = GetRefererInfo(model.MC_RefererCard);
            if (!refererResult.IsSuccess)
            {
                return refererResult;
            }
            model.S_ID = 2;
            model.MC_TotalMoney = 0;
            model.MC_TotalCount = 0;
            if (Add(model))
            {
                return new OperateResult(true,"创建成功",null);
            }
            else
            {
                return new OperateResult(true, "网络异常", null);
            }

        }

       
        public OperateResult GetMerberCardInfo(string value,SearchEnum type=SearchEnum.MC_ID)
        {
            var model = Find(e => e.MC_ID == Convert.ToInt32(value));
   

            if (type == SearchEnum.MC_CardID)
            {
                model = Find(e => e.MC_CardID == value);
            }
            
            if (model == null)
            {

                return new OperateResult(false, "当前会员未找到", null);

            }
            var viewModel = new MerberCardEditViewModel()
            {
                Id = model.MC_ID,
                CardLevelId = (int)model.CL_ID,
                BirthdayType = (byte)model.MC_BirthdayType,
                BirthdayDay = (int)model.MC_Birthday_Day,
                BirthdayMonth = (int)model.MC_Birthday_Month,
                IsPast = Convert.ToBoolean(model.MC_IsPast),
                IsPointAuto = Convert.ToBoolean(model.MC_IsPointAuto),
                Point = (int)model.MC_Point,
                CardId = model.MC_CardID,
                Mobile = model.MC_Mobile,
                Money = (decimal)model.MC_Money,
                Sex = (SexTypeEnum)model.MC_Sex,
                CardState = (CardStateTypeEnum)model.MC_State,
                RefererCard = model.MC_RefererCard,
                Password = model.MC_Password,
                PastTime = (DateTime)model.MC_PastTime,
                Name = model.MC_Name
            };

            return new OperateResult(true,"",viewModel);

        }


        public OperateResult Update(MerberCardEditViewModel viewModel)
        {
            var model = Find(e => e.MC_ID == viewModel.Id);
            if (model == null)
            {
                return new OperateResult(false, "当前会员未找到");

            }

            model.CL_ID = viewModel.CardLevelId;
            model.MC_BirthdayType = (byte)viewModel.BirthdayType;
            model.MC_Birthday_Day = viewModel.BirthdayDay;
            model.MC_Birthday_Month = viewModel.BirthdayMonth;
            model.MC_IsPast = Convert.ToByte(viewModel.IsPast);
            model.MC_IsPointAuto = Convert.ToByte(viewModel.IsPointAuto);
            model.MC_CardID = viewModel.CardId;
            model.MC_Mobile = viewModel.Mobile;
            model.MC_Money = (float)viewModel.Money;
            model.MC_Name = viewModel.Name;
            if (!string.IsNullOrWhiteSpace(viewModel.Password)&&!string.IsNullOrWhiteSpace(viewModel.PasswordConfim))
            {
                if (viewModel.Password != viewModel.PasswordConfim)
                {
                    return new OperateResult(false, "密码输入不一致");
                }
                model.MC_Password = viewModel.Password;

            }
      

            model.MC_PastTime = Convert.ToDateTime(viewModel.PastTime);
            model.MC_Point = viewModel.Point;
            model.MC_Sex = (int)viewModel.Sex;
            model.MC_State = (int)viewModel.CardState;
            model.MC_RefererCard = viewModel.RefererCard;
            if (!string.IsNullOrWhiteSpace(model.MC_RefererCard))
            {
                var referer = Find(e => Convert.ToInt32(e.MC_CardID) == model.MC_RefererID);
                if (referer != null)
                {
                    model.MC_RefererName = referer.MC_RefererName;
                    model.MC_RefererID = referer.MC_ID;
                }
                else
                {
                    return new OperateResult(false, "您输入的推荐人不存在");
                }
            }

            if (Update(model))
            {
                return new OperateResult(true, "更新成功");
            }
            else
            {
                return new OperateResult(false, "网络异常");
            }

        }

        /// <summary>
        /// 查找推荐人
        /// </summary>
        /// <param name="CardId"></param>
        /// <returns></returns>
        private  OperateResult GetRefererInfo(string CardId)
        {
            if (!string.IsNullOrWhiteSpace(CardId))
            {
                var referer = Find(e => e.MC_CardID == CardId);
                if (referer != null)
                {
                    return new OperateResult(true, "",referer);
                }
                else
                {
                    return new OperateResult(false, "您输入的推荐人不存在");
                }
            }
            return new OperateResult(false, "不能为空");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperateResult Delete(int id)
        {
            var model = Find(e => e.MC_ID == id);
            if (model == null)
            {
                return new OperateResult(false, "该会员不存在");
            }
            if (Delete(model))
            {
                return new OperateResult(true, "删除成功");

            }
            else
            {
                return new OperateResult(false, "网络异常");
            }
        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OperateResult TransferMoney(TransferMoneyViewModel model)
        {
            SqlParameter[] pams =
            {
                new SqlParameter("@userId",model.UserId),
                new SqlParameter("@fromMerberCarId",model.FromMerberCardId),
                new SqlParameter("@toMerberCaId",model.ToMerberCardId),
                new SqlParameter("@transferMoney",model.TransferMoney),
                new SqlParameter("@remark",model.Remark),
                new SqlParameter("@errorMsg",System.Data.SqlDbType.NVarChar,128)
            };
            pams[5].Direction = System.Data.ParameterDirection.Output;
            try
            {
                db.Database.ExecuteSqlCommand("EXEC dbo.SP_TransferMoney @userId,@fromMerberCarId,@toMerberCaId,@transferMoney,@remark,@errorMsg output", pams);
                if (!string.IsNullOrWhiteSpace(pams[5].Value.ToString()))
                {
                    return new OperateResult(false,pams[5].Value.ToString());
                }
                else
                {
                    return new OperateResult(false, "转账成功");

                }

            }
            catch (Exception e)
            {

                return new OperateResult(false, e.Message);
            }
            
        }

        

    }
}