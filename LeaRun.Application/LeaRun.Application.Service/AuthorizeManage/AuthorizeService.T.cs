using LeaRun.Application.Code;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：刘晓雷
    /// 编辑人：LR01010
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeService<T> : RepositoryFactory<T>, IAuthorizeService<T> where T : class,new()
    {
        private IRepository db = new RepositoryFactory().BaseRepository();
        private AuthorizeService authorizeService = new AuthorizeService();
        #region 带权限的数据源查询
        /// <summary>
        /// IQueryable查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> IQueryable()
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().IQueryable();
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("F_CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().IQueryable(lambda);
            }
        }
        /// <summary>
        /// IQueryable查询
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("F_CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return db.IQueryable<T>(condition);
        }
        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList(Pagination pagination)
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().FindList(pagination);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("F_CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().FindList(lambda, pagination);
            }
        }
        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("F_CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return this.BaseRepository().FindList(condition, pagination);
        }
        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and F_CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql);
        }
        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and F_CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter);
        }
        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and F_CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, pagination);
        }
        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbParameter"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and F_CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter, pagination);
        }
        #endregion
        
        #region 取数据权限用户
        private string GetReadUserId()
        {
            return CurrentDataAuthorize().ReadAutorizeUserId;
        }
        private string GetReadSql()
        {
            return CurrentDataAuthorize().ReadAutorize;
        }

        private AuthorizeDataModel CurrentDataAuthorize()
        {
            try
            {
                AuthorizeDataModel currentDataAuthorize = OperatorProvider.Provider.CurrentDataAuthorize();
                if (currentDataAuthorize == null)
                {
                    currentDataAuthorize = new AuthorizeDataModel();
                    currentDataAuthorize.ReadAutorize = authorizeService.GetDataAuthor(OperatorProvider.Provider.Current());
                    currentDataAuthorize.ReadAutorizeUserId = authorizeService.GetDataAuthorUserId(OperatorProvider.Provider.Current());
                    currentDataAuthorize.WriteAutorize = authorizeService.GetDataAuthor(OperatorProvider.Provider.Current(), true);
                    currentDataAuthorize.WriteAutorizeUserId = authorizeService.GetDataAuthorUserId(OperatorProvider.Provider.Current(), true);

                    OperatorProvider.Provider.AddCurrentDataAuthorize(currentDataAuthorize, OperatorProvider.Provider.Current().UserId);
                }
                return currentDataAuthorize;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
