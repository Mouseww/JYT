
using System.Collections.Generic;
namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：工作流引擎接口
    /// </summary>
    public interface IWfRuntime
    {
        #region
        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <returns></returns>
        WFNodeModel getStartNode();
        /// <summary>
        /// 获取节点实体
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        WFNodeModel getCurrentNode(string nodeId);
        /// <summary>
        /// 获取下一个节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        List<WFNodeModel> getNextNodes(string nodeId, string formData);
        /// <summary>
        /// 获取下一个节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="formDataList"></param>
        /// <returns></returns>
        List<WFNodeModel> getNextNodes(string nodeId, List<WFVerificationFormModel> formDataList);
        /// <summary>
        /// 获取此节点上一节点数
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        int getToNodeNum(string nodeId);

        /// <summary>
        /// 获取SQL语句
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="pkName"></param>
        /// <param name="frmData"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        string SqlBuider(dynamic tablename, string pkName, string frmData, string keyValue);
        #endregion
    }
}
