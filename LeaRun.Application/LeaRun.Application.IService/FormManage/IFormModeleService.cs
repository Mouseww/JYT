using LeaRun.Application.Entity.FormManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.11.29 14:50
    /// 描 述：表单模块表接口
    /// </summary>
    public interface IFormModuleService
    {
        #region 获取数据
        /// <summary>
        /// 获取表单列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<FormModuleEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取表单数据ALL(用于下拉框)
        /// </summary>
        /// <returns></returns>
        IEnumerable<FormModuleEntity> GetEntityList(string queryJson);
        /// <summary>
        /// 获取表单数据根据关联信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<FormModuleEntity> GetEntityListByRelation(string queryJson);
        /// <summary>
        /// 获取表单模块实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FormModuleEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除表单模块数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        int VirtualRemoveEntity(string keyValue);
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="entity">表单模板实体类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        int SaveEntity(string keyValue, FormModuleEntity entity);
        #endregion
    }
}
