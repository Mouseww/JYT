using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 14:13
    /// 描 述：Base_PostTypes
    /// </summary>
    public class Base_PostTypesEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 类型表主键
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPEID")]
        public string F_TypeID { get; set; }
        /// <summary>
        /// 类型名
        /// </summary>
        /// <returns></returns>
        [Column("TYPENAME")]
        public string TypeName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_TypeID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_TypeID = keyValue;
        }
        #endregion
    }
}