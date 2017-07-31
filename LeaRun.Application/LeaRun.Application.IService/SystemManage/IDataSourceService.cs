using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-09-07 09:39
    /// 描 述：Base_DataSource
    /// </summary>
    public interface IDataSourceService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<DataSourceEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DataSourceEntity GetEntity(string keyValue);
          /// <summary>
        /// 源数据管理列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, DataSourceEntity entity);
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
        DataTable GetData(string strSql, string dbConnection, string dbType, string queryJson = "{}");
        #endregion
    }
}
