using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.27 10:44
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流实例模板表
    /// </summary>
    public class WFProcessSchemeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 流程模板ID
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMEINFOID")]
        public string F_SchemeInfoId { get; set; }
        /// <summary>
        /// 绑定的功能模块ID
        /// </summary>
        /// <returns></returns>
        [Column("F_MODULEID")]
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 流程内容版本
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMEVERSION")]
        public DateTime? F_SchemeVersion { get; set; }
        /// <summary>
        /// 流程模板内容
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMECONTENT")]
        public string F_SchemeContent { get; set; }
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
