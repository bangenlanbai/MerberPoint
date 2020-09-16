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
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public OperateResult(bool isSuccess,string msg,object data)
        {
            this.IsSuccess = isSuccess;
            this.Msg = msg;
            this.Data = data;
        }
        public OperateResult()
        {

        }


    }
}
