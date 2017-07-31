using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用
    /// </summary>
    public class WeChatAppEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 应用主键
        /// </summary>
        /// <returns></returns>
        [Column("F_APPID")]
        public string F_AppId { get; set; }
        /// <summary>
        /// 应用Logo
        /// </summary>
        /// <returns></returns>
        [Column("F_APPLOGO")]
        public string F_AppLogo { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        /// <returns></returns>
        [Column("F_APPNAME")]
        public string F_AppName { get; set; }
        /// <summary>
        /// 应用类型
        /// </summary>
        /// <returns></returns>
        [Column("F_APPTYPE")]
        public int? F_AppType { get; set; }
        /// <summary>
        /// 应用介绍
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 应用主页
        /// </summary>
        /// <returns></returns>
        [Column("F_APPURL")]
        public string F_AppUrl { get; set; }
        /// <summary>
        /// 可信域名
        /// </summary>
        /// <returns></returns>
        [Column("F_REDIRECTDOMAIN")]
        public string F_RedirectDomain { get; set; }
        /// <summary>
        /// 应用菜单
        /// </summary>
        /// <returns></returns>
        [Column("F_MENUJSON")]
        public string F_MenuJson { get; set; }
        /// <summary>
        /// 是否接收用户变更通知
        /// </summary>
        /// <returns></returns>
        [Column("F_ISREPORTUSER")]
        public int? F_IsReportUser { get; set; }
        /// <summary>
        /// 是否上报用户进入应用事件
        /// </summary>
        /// <returns></returns>
        [Column("F_ISREPORTENTER")]
        public int? F_IsReportenter { get; set; }
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
            this.F_AppId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}