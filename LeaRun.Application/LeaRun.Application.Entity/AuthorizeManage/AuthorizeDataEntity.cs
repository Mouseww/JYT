using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.27
    /// 描 述：授权数据范围
    /// </summary>
    public class AuthorizeDataEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 授权数据主键
        /// </summary>
        /// <returns></returns>
        [Column("F_AUTHORIZEDATAID")]
        public string F_AuthorizeDataId { get; set; }
        /// <summary>
        /// 1-仅限本人2-仅限本人及下属3-所在部门4-所在公司5-按明细设置
        /// </summary>
        /// <returns></returns>
        [Column("F_AUTHORIZETYPE")]
        public int? F_AuthorizeType { get; set; }
        /// <summary>
        /// 对象分类:1-部门2-角色3-岗位4-职位5-工作组
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
        /// 项目Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMID")]
        public string F_ItemId { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMNAME")]
        public string F_ItemName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <returns></returns>
        [Column("F_RESOURCEID")]
        public string F_ResourceId { get; set; }
        /// <summary>
        /// 只读
        /// </summary>
        /// <returns></returns>
        [Column("F_ISREAD")]
        public int? F_IsRead { get; set; }
        /// <summary>
        /// 约束表达式
        /// </summary>
        /// <returns></returns>
        [Column("F_AUTHORIZECONSTRAINT")]
        public string F_AuthorizeConstraint { get; set; }
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
            this.F_AuthorizeDataId = Guid.NewGuid().ToString();
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
            this.F_AuthorizeDataId = keyValue;
        }
        #endregion
    }
}