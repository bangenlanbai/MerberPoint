using System.ComponentModel;

namespace BGLB.MerberPoint.Common
{
    public enum CardStateTypeEnum
    {
        [Description("正常")]
        正常 = 1,
        [Description("挂失")]
        挂失 = 2,
        [Description("锁定")]
        锁定 = 3
    }

    public enum SexTypeEnum
    {
        [Description("男")]
        男 = 1,
        [Description("女")]
        女 = 2,
    }

    public enum SearchEnum
    {
        [Description("会员卡编号")]
        MC_ID = 1,
        [Description("会员卡号")]
        MC_CardID = 2,
    }
}
