using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件内容
    /// </summary>
    public class EmailContentEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 邮件信息主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTENTID")]
        public string F_ContentId { get; set; }
        /// <summary>
        /// 邮件分类主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORYID")]
        public string F_CategoryId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        /// <returns></returns>
        [Column("F_THEME")]
        public string F_Theme { get; set; }
        /// <summary>
        /// 邮件主题色彩
        /// </summary>
        /// <returns></returns>
        [Column("F_THEMECOLOR")]
        public string F_ThemeColor { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        /// <returns></returns>
        [Column("F_EMAILCONTENT")]
        public string F_EmailContent { get; set; }
        /// <summary>
        /// 邮件附件
        /// </summary>
        [Column("F_FILES")]
        public string F_Files { get; set; }
        /// <summary>
        /// 邮件类型（1-内部2-外部）
        /// </summary>
        /// <returns></returns>
        [Column("F_EMAILTYPE")]
        public int? F_EmailType { get; set; }
        /// <summary>
        /// 发件人Id
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDERID")]
        public string F_SenderId { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDERNAME")]
        public string F_SenderName { get; set; }
        /// <summary>
        /// 发件日期
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDERTIME")]
        public DateTime? F_SenderTime { get; set; }
        /// <summary>
        /// 设置红旗
        /// </summary>
        /// <returns></returns>
        [Column("F_ISHIGHLIGHT")]
        public int? F_IsHighlight { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDPRIORITY")]
        public string F_SendPriority { get; set; }
        /// <summary>
        /// 短信提醒
        /// </summary>
        /// <returns></returns>
        [Column("F_ISSMSREMINDER")]
        public int? F_IsSmsReminder { get; set; }
        /// <summary>
        /// 已读回执
        /// </summary>
        /// <returns></returns>
        [Column("F_ISRECEIPT")]
        public int? F_IsReceipt { get; set; }
        /// <summary>
        /// 收件人html
        /// </summary>
        /// <returns></returns>
        [Column("F_ADDRESSSHTML")]
        public string F_AddresssHtml { get; set; }
        /// <summary>
        /// 抄送人html
        /// </summary>
        /// <returns></returns>
        [Column("F_COPYSENDHTML")]
        public string F_CopysendHtml { get; set; }
        /// <summary>
        /// 密送人html
        /// </summary>
        /// <returns></returns>
        [Column("F_BCCSENDHTML")]
        public string F_BccsendHtml { get; set; }
        /// <summary>
        /// 发送状态（1-已发送0-草稿）
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDSTATE")]
        public int? F_SendState { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
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
        /// 修改日期
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_ContentId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.F_SenderTime = DateTime.Now;
            this.F_SenderId = OperatorProvider.Provider.Current().UserId;
            this.F_SenderName = OperatorProvider.Provider.Current().UserName + "（" + OperatorProvider.Provider.Current().Account + "）";
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_ContentId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}