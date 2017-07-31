using LeaRun.Application.Entity.AppManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-07-18 11:52
    /// 描 述：App_PageTemplates
    /// </summary>
    public interface AppTemplatesIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<AppTemplatesEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        AppTemplatesEntity GetEntity(string keyValue);
          /// <summary>
        /// 获取实体列表根据项目Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        IEnumerable<AppTemplatesEntity> GetEntityByProjectId(string projectId);
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
        void SaveForm(string keyValue, AppTemplatesEntity entity);
        #endregion
    }
}