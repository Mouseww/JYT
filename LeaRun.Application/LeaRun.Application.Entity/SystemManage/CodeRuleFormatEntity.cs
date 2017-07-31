using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则格式
    /// </summary>
    public class CodeRuleFormatEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号规则格式主键
        /// </summary>		
        public string F_RuleFormatId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>		
        public string F_RuleId { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>		
        public int? F_ItemType { get; set; }
        /// <summary>
        /// 项目类型名称
        /// </summary>		
        public string F_ItemTypeName { get; set; }
        /// <summary>
        /// 格式化字符串
        /// </summary>		
        public string F_FormatStr { get; set; }
        /// <summary>
        /// 步长
        /// </summary>		
        public int? F_StepValue { get; set; }
        /// <summary>
        /// 初始值
        /// </summary>		
        public int? F_InitValue { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string F_Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_RuleFormatId = Guid.NewGuid().ToString();
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {

        }
        #endregion
    }
}