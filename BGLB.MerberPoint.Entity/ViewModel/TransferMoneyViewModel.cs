using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGLB.MerberPoint.Entity.ViewModel
{
    public  class TransferMoneyViewModel
    {
        // 操作人Id
        public string UserId { get; set; }

        // 转账人卡号
        public string FromMerberCardId { get; set; }

        // 收款人卡号
        public string ToMerberCardId { get; set; }

        // 钱的数量
        public decimal TransferMoney { get; set; }

        // 转账备注
        public string Remark { get; set; }
    }
}
