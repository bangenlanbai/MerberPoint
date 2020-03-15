using BGLB.MerberPoint.Common;
using BGLB.MerberPoint.Entity.POCOModel;
using BGLB.MerberPoint.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLB.MerberPoint.Business
{
    public class MeberCardService:BaseService<MemCards>
    {
        public OperateResult GetPagedMeberCardList(GetPagedMeberCardListViewModel viewModel)
        {
            //当用户什么都没有输入的时候默认查询所有的
            var query = PredicateExtensions.True<MemCards>();
            //根据用户输入的参数信息实现多条件查询
            if (!string.IsNullOrWhiteSpace(viewModel.Name))
            {
                query = query.And(e => e.MC_Name.Contains(viewModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(viewModel.Mobile))
            {
                query = query.And(e => e.MC_Mobile==viewModel.Mobile);
            }
            if (viewModel.CardLevelId>0)
            {
                query = query.And(e => e.CL_ID==viewModel.CardLevelId);
            }
            if (viewModel.State>0)
            {
                query = query.And(e => e.MC_State==viewModel.State);
            }
            GetPagedMeberCardList()

        }

    }
}
