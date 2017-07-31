using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-12-14 05:56
    /// 描 述：Form_ModuleContent
    /// </summary>
    public class Form_ModuleContentEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 表单Id
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
        /// 表单内容
        /// </summary>
        /// <returns></returns>
        [Column("F_FRMCONTENT")]
        public string F_FrmContent { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
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