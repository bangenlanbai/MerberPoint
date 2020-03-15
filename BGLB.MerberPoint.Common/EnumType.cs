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
}
