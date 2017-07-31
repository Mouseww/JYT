using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流委托记录表
    /// </summary>
    public class WFDelegateRecordEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 委托规则Id
        /// </summary>
        /// <returns></returns>
        [Column("F_WFDELEGATERULEID")]
        public string F_WFDelegateRuleId { get; set; }
        /// <summary>
        /// 委托人Id
        /// </summary>
        /// <returns></returns>
        [Column("F_FROMUSERID")]
        public string F_FromUserId { get; set; }
        /// <summary>
        /// 委托人
        /// </summary>
        /// <returns></returns>
        [Column("F_FROMUSERNAME")]
        public string F_FromUserName { get; set; }
        /// <summary>
        /// 被委托人Id
        /// </summary>
        /// <returns></returns>
        [Column("F_TOUSERID")]
        public string F_ToUserId { get; set; }
        /// <summary>
        /// 被委托人名称
        /// </summary>
        /// <returns></returns>
        [Column("F_TOUSERNAME")]
        public string F_ToUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 流程实例Id
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSID")]
        public string F_ProcessId { get; set; }
        /// <summary>
        /// 实例编号
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSCODE")]
        public string F_ProcessCode { get; set; }
        /// <summary>
        /// 实例自定义名称
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSNAME")]
        public string F_ProcessName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;        }
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
