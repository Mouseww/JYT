using LeaRun.Application.Entity.FlowManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.02.23 10:14
    /// 描 述：工作流模板信息表操作接口（支持：SqlServer）
    /// </summary>
    public interface IWFSchemeInfoService
    {
        #region 获取数据
        /// <summary>
        /// 获取流程列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取流程列表数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetList(string queryJson);
        /// <summary>
        /// 获取一个流程模板实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        WFSchemeInfoEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据一个功能模块Id获取一个流程模板
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        WFSchemeInfoEntity GetEntityByModuleId(string moduleId);
        /// <summary>
        /// 获取权限列表数据
        /// </summary>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        IEnumerable<WFSchemeInfoAuthorizeEntity> GetAuthorizeEntityList(string schemeInfoId);
        /// <summary>
        /// 获取流程模板列表
        /// </summary>
        /// <returns></returns>
        DataTable GetList();
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程设计（保存,编辑）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="shcemeAuthorizeData"></param>
        /// <returns></returns>
        int SaveForm(string keyValue, WFSchemeInfoEntity entity,string[] shcemeAuthorizeData);
         /// <summary>
        /// 删除流程设计
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int DeleteEntity(string keyValue);
        /// <summary>
        /// 更新流程模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        void UpdateState(string keyValue, int state);
        #endregion
    }
}
