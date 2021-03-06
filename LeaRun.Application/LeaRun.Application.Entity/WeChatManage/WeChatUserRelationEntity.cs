﻿using LeaRun.Application.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号成员
    /// </summary>
    public class WeChatUserRelationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户对应关系主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERRELATIONID")]
        public string F_UserRelationId { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        /// <returns></returns>
        [Column("F_DEPTID")]
        public string F_DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        [Column("F_DEPTNAME")]
        public string F_DeptName { get; set; }
        /// <summary>
        /// 微信部门ID
        /// </summary>
        /// <returns></returns>
        [Column("F_WECHATDEPTID")]
        public int? F_WeChatDeptId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        /// <returns></returns>
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 同步状态
        /// </summary>
        /// <returns></returns>
        [Column("F_SYNCSTATE")]
        public string F_SyncState { get; set; }
        /// <summary>
        /// 同步日志
        /// </summary>
        /// <returns></returns>
        [Column("F_SYNCLOG")]
        public string F_SyncLog { get; set; }
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
            this.F_UserRelationId = keyValue;
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}