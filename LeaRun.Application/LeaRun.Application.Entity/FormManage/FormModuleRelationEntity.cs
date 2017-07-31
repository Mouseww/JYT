using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-12-14 06:22
    /// 描 述：Form_ModuleRelation
    /// </summary>
    public class FormModuleRelationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 表单名称，不在实体映射中
        /// </summary>
        [Column("F_FRMNAME")]
        [NotMapped]
        public string F_FrmName { get; set; }
        /// <summary>
        /// 最新表单版本号，不在实体映射中
        /// </summary>
        [Column("NEWVERSION")]
        [NotMapped]
        public string NewVersion { get; set; } 
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 表单内容表Id
        /// </summary>
        /// <returns></returns>
        [Column("F_MODULECONTENTID")]
        public string F_ModuleContentId { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMID")]
        public string F_FrmId { get; set; }
        /// <summary>
        /// 表单版本号
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMVERSION")]
        public string F_FrmVersion { get; set; }
        /// <summary>
        /// 表单关联类型
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMKIND")]
        public int? F_FrmKind { get; set; }
        /// <summary>
        /// 关联模块Id
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTID")]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 关联功能按钮Id
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTBUTTONID")]
        public string F_ObjectButtonId { get; set; }
        /// <summary>
        /// 关联功能模块名称
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTNAME")]
        public string F_ObjectName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 更新人id
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 更新人名称
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