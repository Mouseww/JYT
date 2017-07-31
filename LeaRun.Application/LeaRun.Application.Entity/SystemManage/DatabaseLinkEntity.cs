using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.18 11:02
    /// 描 述：数据库连接管理
    /// </summary>
    public class DataBaseLinkEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 数据库连接主键
        /// </summary>
        /// <returns></returns>
        [Column("F_DATABASELINKID")]
        public string F_DatabaseLinkId { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        /// <returns></returns>
        [Column("F_SERVERADDRESS")]
        public string F_ServerAddress { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        /// <returns></returns>
        [Column("F_DBNAME")]
        public string F_DBName { get; set; }
        /// <summary>
        /// 数据库别名
        /// </summary>
        /// <returns></returns>
        [Column("F_DBALIAS")]
        public string F_DBAlias { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <returns></returns>
        [Column("F_DBTYPE")]
        public string F_DbType { get; set; }
        /// <summary>
        /// 数据库版本
        /// </summary>
        /// <returns></returns>
        [Column("F_VERSION")]
        public string F_Version { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>
        /// <returns></returns>
        [Column("F_DBCONNECTION")]
        public string F_DbConnection { get; set; }
        /// <summary>
        /// 连接地址是否加密
        /// </summary>
        /// <returns></returns>
        [Column("F_DESENCRYPT")]
        public int? F_DESEncrypt { get; set; }
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
            this.F_DatabaseLinkId = Guid.NewGuid().ToString();
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
            this.F_DatabaseLinkId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}