using BGLB.MerberPoint.Entity.POCOModel;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace BGLB.MerberPoint.Business
{
    public class CallContextFactory
    {
        /// <summary>
        /// 从当前线程中取数据访问上下文，保证线程内唯一；
        /// </summary>
        /// <returns></returns>
        public static DbContext GetCurrentDbcontext()
        {
            if (!(CallContext.GetData("MerberPointModel") is DbContext db))
            {
                db = new MerberPointModel();
                CallContext.SetData("MerberPointModel", db);
            }
            return db;
        }

    }
}
