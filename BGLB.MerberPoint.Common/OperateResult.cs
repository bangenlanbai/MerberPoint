namespace BGLB.MerberPoint.Common
{
    /// <summary>
    /// 公用的处理方法
    /// </summary>
    public class OperateResult
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        ///初始化
        /// </summary>
        /// <param name="isSuccess">操作结果</param>
        /// <param name="msg">提示信息</param>
        /// <param name="data">数据信息</param>
        public OperateResult(bool isSuccess, string msg, object data)
        {
            this.IsSuccess = isSuccess;
            this.Msg = msg;
            this.Data = data;
        }

        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="data">数据信息</param>
        public OperateResult(object data)
        {
            this.IsSuccess = true;
            this.Msg = "";
            this.Data = data;
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg">提示信息</param>
        public OperateResult(string msg)
        {
            this.IsSuccess = false;
            this.Msg = msg;
            this.Data = null;

        }

        /// <summary>
        /// 不反回数据
        /// </summary>
        /// <param name="msg">提示信息</param>
        public OperateResult(bool isSuccess,string msg)
        {
            this.IsSuccess = isSuccess;
            this.Msg = msg;
            this.Data = null;

        }
    }
}
