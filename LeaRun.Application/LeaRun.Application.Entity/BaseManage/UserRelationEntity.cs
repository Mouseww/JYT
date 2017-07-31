using System;
using LeaRun.Application.Code;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户关系
    /// </summary>
    public class UserRelationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户关系主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERRELATIONID")]
        public string F_UserRelationId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 分类:1-部门2-角色3-岗位4-职位5-工作组
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORY")]
        public int? F_Category { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTID")]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 默认对象
        /// </summary>
        /// <returns></returns>
        [Column("F_ISDEFAULT")]
        public int? F_IsDefault { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
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
            this.F_UserRelationId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.F_IsDefault = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            
        }
        #endregion
    }
}