using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.27 15.56
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流实例流转Id
    /// </summary>
    public class WFProcessNodesEntity : BaseEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 关联的实例Id
        /// </summary>
        [Column("F_PROCESSID")]
        public string F_ProcessId { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        [Column("F_NODEID")]
        public string F_NodeId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        [Column("F_NODENAME")]
        public string F_NodeName { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        [Column("F_NODETYPE")]
        public string F_NodeType { get; set; }
        /// <summary>
        /// 当前节点是否是活跃节点 1处于当前节点 0已流转到下一节点
        /// </summary>
        [Column("F_ISACTIVTYID")]
        public int? F_IsActivtyId { get; set; }
        /// <summary>
        /// 当前节点的状态 0 未处理 1 通过 2不通过 3驳回
        /// </summary>
        [Column("F_NODESATE")]
        public int? F_NodeSate { get; set; }
        /// <summary>
        /// 会签节点成功通过节点
        /// </summary>
        [Column("F_CONFLUENCEOKNUM")]
        public int? F_ConfluenceOkNum { get; set; }
        /// <summary>
        /// 会签节点不成功通过节点（后期预留）
        /// </summary>
        [Column("F_CONFLUENCENONUM")]
        public int? F_ConfluenceNoNum { get; set; }
        /// <summary>
        /// 上一个节点
        /// </summary>
        [Column("F_PREVIOUSID")]
        public string F_PreviousId { get; set; }
        /// <summary>
        /// 上一个节点名称
        /// </summary>
        [Column("F_PREVIOUSNAME")]
        public string F_PreviousName { get; set; }
        /// <summary>
        /// 当前节点审核人员
        /// </summary>
        [Column("F_USERLIST")]
        public string F_UserList { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }

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
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
