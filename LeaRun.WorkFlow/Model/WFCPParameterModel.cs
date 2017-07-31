
namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：创建流程实例参数模型
    /// </summary>
    public class WFCPParameterModel
    {
        /// <summary>
        /// 创建实例Id
        /// </summary>
        public string processId { get; set; }
        /// <summary>
        /// 表单实例Id
        /// </summary>
        public string formInstanceId { get; set; }
        /// <summary>
        /// 创建实例名称
        /// </summary>
        public string processName { get; set; }
        /// <summary>
        /// 流程模板Id
        /// </summary>
        public string schemeId { get; set; }
        /// <summary>
        /// 流程模板绑定的Id
        /// </summary>
        public string moduleId { get; set; }
        /// <summary>
        /// 流程发起实例等级 1重要 2普通 3一般
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 流程实例发起备注
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 表单Id
        /// </summary>
        public string formId { get; set; }
        /// <summary>
        /// 创建流程的表单数据
        /// </summary>
        public string formData { get; set; }
    }
}
