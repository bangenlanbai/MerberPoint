using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BGLB.MerberPoint.Business
{
    public class BaseService<T> where T : class, new()
    {
        protected DbContext db = CallContextFactory.GetCurrentDbcontext();

        //添加
        public bool Add(T entity)
        {
            //把实体标记为添加
            db.Entry<T>(entity).State = EntityState.Added;
            return db.SaveChanges() > 0;
        }

        //删除
        public bool Delete(T entity)
        {
            //把实体附加到EF容器内
            db.Set<T>().Attach(entity);
            //把实体标记为删除
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        //修改
        public bool Update(T entity)
        {
            //把实体附加到EF容器内
            db.Set<T>().Attach(entity);
            //把实体标记为修改
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        //查询
        public T GetEntity(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().FirstOrDefault(whereLambda);
        }

        public T Find(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().FirstOrDefault(whereLambda);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
                return db.Set<T>().Where(whereLambda);
        }
        /// <summary>
        /// 用于排序查询的虚方法 子类 里面可以重写
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderBy">排序关键字</param>
        /// <param name="isAsc">默认正序排列 false 倒序</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true)
        {
            if (isAsc)
            {
                return db.Set<T>().Where(whereLambda).OrderBy(orderBy);
            }
            else
            {
                return db.Set<T>().Where(whereLambda).OrderByDescending(orderBy);
            }
        }
        /// <summary>
        /// 分页查询的虚方法 带排序 子类 里面可以重写
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderBy">排序关键字</param>
        /// <param name="isAsc">默认正序排列 false 倒序</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList<TKey>(int pageIndex,int pageSize,ref int rowCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy, bool isAsc = true)
        {
            rowCount = db.Set<T>().Count(whereLambda);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            if (isAsc)
            {
                return db.Set<T>().Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return db.Set<T>().Where(whereLambda).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        

    }
}
