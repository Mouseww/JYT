using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using System.Collections.Generic;
using System;
using System.Data;
using LeaRun.Util.WebControl;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-09-07 09:39
    /// 描 述：Base_DataSource
    /// </summary>
    public class DataSourceBLL
    {
        private IDataSourceService service = new DataSourceService();
        private IDataBaseLinkService dataBaseLinkService = new DataBaseLinkService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DataSourceEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataSourceEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 源数据管理列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataSourceEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 接口数据处理
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetData(DataSourceEntity entity , string queryJson)
        {
            try
            {
                DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(entity.F_DbId);
                if (dataBaseLinkEntity != null)
                {
                    return service.GetData(entity.F_Strsql, dataBaseLinkEntity.F_DbConnection, queryJson);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取数据表的数据
        /// </summary>
        /// <param name="dbLinkId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableData(string dbLinkId, string tableName)
        {
            try
            {
                DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(dbLinkId);
                if (dataBaseLinkEntity != null)
                {
                    string strSQL = "select * from " + tableName;
                    return service.GetData(strSQL, dataBaseLinkEntity.F_DbConnection, dataBaseLinkEntity.F_DbType);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}
