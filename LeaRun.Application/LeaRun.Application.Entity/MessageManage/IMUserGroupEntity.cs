using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.MessageManage
{
	/// <summary>
    /// 用户群组表
	/// <author>
    ///		<name>she</name>
    ///		<date>2015.08.01 14:00</date>
    /// </author>
    /// </summary>
    public class IMUserGroupEntity : BaseEntity
	{
        #region 实体成员
        /// <summary>
        /// 用户群组主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERGROUPID")]
        public string F_UserGroupId { get; set; }
        /// <summary>
        /// 群组主键
        /// </summary>
        /// <returns></returns>
        [Column("F_GROUPID")]
        public string F_GroupId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERID")]
        public string F_UserId { get; set; }
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
        #endregion
   		
   		#region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_UserGroupId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_GroupId = keyValue;
        }
        #endregion
	}
}