using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.11.29 13:52
    /// 描 述：表单模块表
    /// </summary>
    public class FormModuleEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 关联的模块Id，不映射到实际表里
        /// </summary>
        [Column("F_OBJECTID")]
        [NotMapped]
        public string F_ObjectId { get; set; } 
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMID")]
        public string F_FrmId { get; set; }
        /// <summary>
        /// 表单编号
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMCODE")]
        public string F_FrmCode { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMNAME")]
        public string F_FrmName { get; set; }
        /// <summary>
        /// 表单分类
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMCATEGORY")]
        public string F_FrmCategory { get; set; }
        /// <summary>
        /// 表单类型（0自定义表单;1自定义表单建表;2系统表单）
        /// </summary>
        [Column("F_FRMTYPE")]
        public int? F_FrmType { get; set; }
        /// <summary>
        /// 数据库Id
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMDBID")]
        public string F_FrmDbId { get; set; }
        /// <summary>
        /// 数据表
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMDBTABLE")]
        public string F_FrmDbTable { get; set; }
        /// <summary>
        /// 关联表主键
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMDBPKEY")]
        public string F_FrmDbPkey { get; set; }
        /// <summary>
        /// 表单版本
        /// </summary>
        /// <returns></returns>
        [Column("F_VERSION")]
        public string F_Version { get; set; }
        /// <summary>
        /// 表单内容
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMCONTENT")]
        public string F_FrmContent { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_FrmId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.F_DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_FrmId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
