using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-29 08:37
    /// 描 述：CeShi
    /// </summary>
    public class CeShiEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// CeShi_Id
        /// </summary>
        /// <returns></returns>
        [Column("CESHI_ID")]
        public string CeShi_Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        [Column("NAME")]
        public string Name { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CeShi_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CeShi_Id = keyValue;
        }
        #endregion
    }
}