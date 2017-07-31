using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-09-07 09:39
    /// 描 述：Base_DataSource
    /// </summary>
    public class DataSourceService : RepositoryFactory<DataSourceEntity>, IDataSourceService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DataSourceEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataSourceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 源数据管理列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<DataSourceEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Name.Contains(keyord));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataSourceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion

        #region 接口数据处理
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dbConnection"></param>
        /// <param name="dbType"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetData(string strSql, string dbConnection, string dbType, string queryJson = "{}")
        {
            try
            {
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                Regex myReg = new Regex("&.*\\b");
                var parameterNames = myReg.Matches(strSql);
                foreach (var item in parameterNames)
                {
                    string strItem = item.ToString().Replace("&","");
                    if (!queryParam[strItem].IsEmpty())
                    {
                        parameter.Add(DbParameters.CreateDbParameter("@" + strItem, queryParam[strItem].ToString()));
                    }
                }
                string resStrSql = strSql.Replace("&","@");
                return this.BaseRepository(dbConnection).FindTable(resStrSql, parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
