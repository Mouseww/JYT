using System.Collections.Generic;

namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：工作流线条类
    /// </summary>
    public class WFLineModel
    {
        /// <summary>
        /// 线条Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 线条名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 开始端节点ID
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 结束端节点ID
        /// </summary>
        public string to { get; set; }
        /// <summary>
        /// 线条设置信息
        /// </summary>
        public WFLineSettingModel setInfo { get; set; }
    }
    /// <summary>
    /// 工作流线条设置信息
    /// </summary>
    public class WFLineSettingModel
    {
        /// <summary>
        /// 线条名称
        /// </summary>
        public string lineMyName { get; set; }

        public List<WFLineConditionModel> conditionJson { get; set; }
    }
    /// <summary>
    /// 条件选择信息
    /// </summary>
    public class WFLineConditionModel
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 过滤方式
        /// </summary>
        public string FilterId { get; set; }
        /// <summary>
        /// 过滤名称
        /// </summary>
        public string FilterName { get; set; }
        /// <summary>
        /// 过滤数值
        /// </summary>
        public string FilterValue { get; set; }
        /// <summary>
        /// 条件关系
        /// </summary>
        public string Logic { get; set; }
    }
}
