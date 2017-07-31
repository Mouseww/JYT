using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.18 11:02
    /// 描 述：数据库连接管理
    /// </summary>
    public class DataBaseLinkService : RepositoryFactory<DataBaseLinkEntity>, IDataBaseLinkService
    {
        #region 获取数据
        /// <summary>
        /// 库连接列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataBaseLinkEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.F_CreateDate).ToList();
        }
        /// <summary>
        /// 库连接实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataBaseLinkEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除库连接
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存库连接表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">库连接实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataBaseLinkEntity databaseLinkEntity)
        {
            #region 测试连接数据库
            DbConnection dbConnection = LeaRun.Data.DatabaseCommon.DbConnectionDataSource(databaseLinkEntity.F_DbType, databaseLinkEntity.F_DbConnection);
            databaseLinkEntity.F_ServerAddress = dbConnection.DataSource;
            dbConnection.Close();
            #endregion

            if (!string.IsNullOrEmpty(keyValue))
            {
                databaseLinkEntity.Modify(keyValue);
                this.BaseRepository().Update(databaseLinkEntity);
            }
            else
            {
                databaseLinkEntity.Create();
                this.BaseRepository().Insert(databaseLinkEntity);
            }
        }
        /// <summary>
        /// 测试连接字符串是否成功
        /// </summary>
        /// <param name="databaseLinkEntity"></param>
        public void TestConnection(DataBaseLinkEntity databaseLinkEntity)
        {
            try
            {
                DbConnection dbConnection = null;
                if (string.IsNullOrEmpty(databaseLinkEntity.F_DatabaseLinkId))
                {
                    dbConnection = LeaRun.Data.DatabaseCommon.DbConnectionDataSource(databaseLinkEntity.F_DbType, databaseLinkEntity.F_DbConnection);
                }
                else
                {
                    if (databaseLinkEntity.F_DbConnection == "******")
                    {
                        DataBaseLinkEntity entity = this.BaseRepository().FindEntity(databaseLinkEntity.F_DatabaseLinkId);
                        dbConnection = LeaRun.Data.DatabaseCommon.DbConnectionDataSource(databaseLinkEntity.F_DbType, entity.F_DbConnection);
                    }
                    else
                    {
                        dbConnection = LeaRun.Data.DatabaseCommon.DbConnectionDataSource(databaseLinkEntity.F_DbType, databaseLinkEntity.F_DbConnection);
                    }
                }
                dbConnection.Open();
                dbConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 扩展方法 
        /// <summary>
        /// 根据数据库ID执行sql语句
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="sql"></param>
        public void ExecuteBySql(string dbId, string sql)
        {
            try
            {
                DataBaseLinkEntity entity = this.BaseRepository().FindEntity(dbId);

                this.BaseRepository(entity.F_DbConnection, entity.F_DbType).ExecuteBySql(sql);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// 根据数据库ID执行sql语句
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="sql"></param>
        /// <param name="dbParameter"></param>
        public void ExecuteBySql(string dbId, string sql, params DbParameter[] dbParameter)
        {
            try
            {
                DataBaseLinkEntity entity = this.BaseRepository().FindEntity(dbId);

                this.BaseRepository(entity.F_DbConnection, entity.F_DbType).ExecuteBySql(sql, dbParameter);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 根据数据库ID执行sql语句,查询数据->datatable
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable FindTable(string dbId, string sql)
        {
            try
            {
                DataBaseLinkEntity entity = this.BaseRepository().FindEntity(dbId);
                return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 根据数据库ID执行sql语句,查询数据->datatable
        /// </summary>
        /// <param name="dbId"></param>
        /// <param name="sql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public DataTable FindTable(string dbId, string sql, params DbParameter[] dbParameter)
        {
            try
            {
                DataBaseLinkEntity entity = this.BaseRepository().FindEntity(dbId);
                return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, dbParameter);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
