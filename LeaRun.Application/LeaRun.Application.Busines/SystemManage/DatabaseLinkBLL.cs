using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.18 11:02
    /// 描 述：数据库连接管理
    /// </summary>
    public class DataBaseLinkBLL
    {
        private IDataBaseLinkService service = new DataBaseLinkService();

        #region 获取数据
        /// <summary>
        /// 库连接列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataBaseLinkEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 库连接实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataBaseLinkEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除库连接
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存库连接表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">库连接实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataBaseLinkEntity databaseLinkEntity)
        {
            try
            {
                service.SaveForm(keyValue, databaseLinkEntity);
            }
            catch (Exception)
            {
                throw;
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
                service.TestConnection(databaseLinkEntity);
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
                service.ExecuteBySql(dbId, sql);
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
                service.ExecuteBySql(dbId, sql, dbParameter);
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
                return service.FindTable(dbId, sql);
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
                return service.FindTable(dbId, sql, dbParameter);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
