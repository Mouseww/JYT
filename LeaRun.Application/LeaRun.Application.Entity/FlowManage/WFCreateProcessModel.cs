using System.Collections.Generic;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR 0101
    /// 日 期：2016.10.28 09:58
    /// 描 述：创建流程实例传递的实体类
    /// </summary>
    public class WFCreateProcessModel
    {
        /// <summary>
        /// 实例信息
        /// </summary>
        public WFProcessInstanceEntity processInstanceEntity { get; set; }
        /// <summary>
        /// 实例节点信息
        /// </summary>
        public List<WFProcessNodesEntity> processNodeList { get; set; }
        /// <summary>
        /// 实例流转记录
        /// </summary>
        public List<WFProcessTransitionHistoryEntity> processTransitionHistoryList { get; set; }
        /// <summary>
        /// 实例委托记录
        /// </summary>
        public List<WFDelegateRecordEntity> delegateRecordEntitylist { get; set; }
        /// <summary>
        /// 流程操作记录
        /// </summary>
        public WFProcessOperationHistoryEntity processOperationHistoryEntity { get; set; }
    }
}
