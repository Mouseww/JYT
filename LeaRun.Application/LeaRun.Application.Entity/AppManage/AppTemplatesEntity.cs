using System;
using LeaRun.Application.Code;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-07-18 11:52
    /// 描 述：App_PageTemplates
    /// </summary>
    public class AppTemplatesEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        /// <returns></returns>
        [Column("F_PROJECTID")]
        public string F_ProjectId { get; set; }
        /// <summary>
        /// 模板名字
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 模板值
        /// </summary>
        [Column("F_VALUE")]
        public string F_Value { get; set; }
        /// <summary>
        /// 模板类型
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary>
        /// 上一级Id
        /// </summary>
        [Column("F_PARENT")]
        public string F_Parent { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int? F_level { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Column("F_IMG")]
        public string F_img { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTENT")]
        public string F_Content { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人姓名
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
        }
        #endregion
    }
}