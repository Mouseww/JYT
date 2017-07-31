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
    /// 描 述：表单模块实体表接口
    /// </summary>
    public interface IFormModuleInstanceService
    {
        #region 获取数据
        /// <summary>
        /// 根据表单Id获取表单实例列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<FormModuleInstanceEntity> GetInstancePageList(Pagination pagination, string Id);
        /// <summary>
        /// 根据表单Id获取表单实例列表数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<FormModuleInstanceEntity> GetInstanceList(string processId);
        /// <summary>
        /// 获取表单模块实例数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        FormModuleInstanceEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取表单模块实例数据
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="frmContentId"></param>
        /// <returns></returns>
        FormModuleInstanceEntity GetEntityByProcessId(string processId, string frmContentId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单模块实例数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="contentEntity"></param>
        /// <returns></returns>
        string SaveEntity(string keyValue, FormModuleInstanceEntity entity, Form_ModuleContentEntity contentEntity);

        /// <summary>
        /// 保存表单模块实例数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contentEntity"></param>
        /// <returns></returns>
        string SaveEntity(FormModuleInstanceEntity entity, Form_ModuleContentEntity contentEntity);


        /// <summary>
        /// 移除自定义表单实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int RemoveEntity(string keyValue, Form_ModuleContentEntity contentEntity);
        #endregion
    }
}
