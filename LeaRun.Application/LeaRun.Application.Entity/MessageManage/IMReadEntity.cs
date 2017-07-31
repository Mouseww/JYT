using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.MessageManage
{
	/// <summary>
    /// 未读消息表
	/// <author>
    ///		<name>she</name>
    ///		<date>2015.08.01 14:00</date>
    /// </author>
    /// </summary>
    public class IMReadEntity : BaseEntity
	{
        #region 实体成员
        /// <summary>
        /// 未读消息主键
        /// </summary>
        /// <returns></returns>
        [Column("F_READID")]
        public string F_ReadId { get; set; }
        /// <summary>
        /// 消息主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTENTID")]
        public string F_ContentId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDID")]
        public string F_SendId { get; set; }
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
        /// 发送状态（0-未送达，1-已送达，2-已阅读）
        /// </summary>
        /// <returns></returns>
        [Column("F_READSTATUS")]
        public int? F_ReadStatus { get; set; }
        #endregion
   		
   		#region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_ReadId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_ReadId = keyValue;
        }
        #endregion
	}
}