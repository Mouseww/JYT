using LeaRun.Application.Entity.FlowManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;
namespace LeaRun.Application.IService.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例表操作接口（支持：SqlServer）
    /// </summary>
    public interface IWFProcessInstanceService
    {
        #region 获取数据
        /// <summary>
        /// 获取流程监控数据（用于流程监控）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取我的流程实例数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        DataTable GetMyProcessPageList(Pagination pagination, string queryJson);
         /// <summary>
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetToMeBeforePageList(Pagination pagination, string queryJson);
         /// <summary>
        /// 获取登录者已经处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetToMeAfterPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实例进程信息实体
        /// </summary>
        /// <param name="keyVlaue"></param>
        /// <returns></returns>
        WFProcessInstanceEntity GetEntity(string keyVlaue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="wfCreateProcessModel"></param>
        void CreateProcess(WFCreateProcessModel wfCreateProcessModel);
         /// <summary>
        /// 重新发起流程实例
        /// </summary>
        /// <param name="wfCreateProcessModel"></param>
        void EditProcess(WFCreateProcessModel wfCreateProcessModel);
          /// <summary>
        /// 保存节点审核数据
        /// </summary>
        /// <param name="wfCreateProcessModel"></param>
        void SaveProcessVerification(WFVerificationProcessModel wfVerificationProcessModel);
        /// <summary>
        /// 删除工作流实例进程
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        int DeleteProcess(string keyValue);
        /// <summary>
        /// 虚拟操作实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="state">0暂停,1启用,2取消（召回）</param>
        /// <returns></returns>
        int OperateVirtualProcess(string keyValue, int state);
        /// <summary>
        /// 流程指派
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="makeLists"></param>
        void DesignateProcess(string processId, string makeLists);
        #endregion

        #region 流程流转节点操作
         /// <summary>
        /// 获取流程实例节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        WFProcessNodesEntity GetProcessNode(string nodeId);
         /// <summary>
        /// 获取流程实例节点信息
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        WFProcessNodesEntity GetProcessNode(string nodeId, string processId);
        /// <summary>
        /// 获取工作流实例里所有流转的节点
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        IEnumerable<WFProcessNodesEntity> GetProcessNodeList(string processId);
        /// <summary>
        /// 更新流程节点信息
        /// </summary>
        /// <param name="entity"></param>
        void UpdateNode(WFProcessNodesEntity entity);
        #endregion
    }
}
