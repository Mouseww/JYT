using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.28 09:58
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流实例流转历史记录
    /// </summary>
    public class WFProcessTransitionHistoryEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 实例进程Id
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSID")]
        public string F_ProcessId { get; set; }
        /// <summary>
        /// 开始节点Id
        /// </summary>
        /// <returns></returns>
        [Column("F_FROMNODEID")]
        public string F_FromNodeId { get; set; }
        /// <summary>
        /// F_FromNodeType
        /// </summary>
        /// <returns></returns>
        [Column("F_FROMNODETYPE")]
        public string F_FromNodeType { get; set; }
        /// <summary>
        /// 开始节点名称
        /// </summary>
        /// <returns></returns>
        [Column("F_FROMNODENAME")]
        public string F_FromNodeName { get; set; }
        /// <summary>
        /// 结束节点Id
        /// </summary>
        /// <returns></returns>
        [Column("F_TONODEID")]
        public string F_ToNodeId { get; set; }
        /// <summary>
        /// F_ToNodeType
        /// </summary>
        /// <returns></returns>
        [Column("F_TONODETYPE")]
        public string F_ToNodeType { get; set; }
        /// <summary>
        /// 结束节点名称
        /// </summary>
        /// <returns></returns>
        [Column("F_TONODENAME")]
        public string F_ToNodeName { get; set; }
        /// <summary>
        /// 转化状态
        /// </summary>
        /// <returns></returns>
        [Column("F_TRANSITIONSATE")]
        public int? F_TransitionSate { get; set; }
        /// <summary>
        /// 转化时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 执行人
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// F_CreateUserName
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
    }
}
