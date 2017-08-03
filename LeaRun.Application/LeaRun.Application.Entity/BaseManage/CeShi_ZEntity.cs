using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 16:44
    /// 描 述：主表
    /// </summary>
    public class CeShi_ZEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主表ID
        /// </summary>
        /// <returns></returns>
        [Column("Z_ID")]
        public string Z_Id { get; set; }
        /// <summary>
        /// Z_Name
        /// </summary>
        /// <returns></returns>
        [Column("Z_NAME")]
        public string Z_Name { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Z_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Z_Id = keyValue;
        }
        #endregion
    }
}