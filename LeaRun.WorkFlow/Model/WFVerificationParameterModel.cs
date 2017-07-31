
using System.Collections.Generic;
namespace LeaRun.WorkFlow
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.10.26 10:18
    /// 描 述：创建流程实例参数模型
    /// </summary>
    public class WFVerificationParameterModel
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public string processId { get; set; }
        /// <summary>
        /// 流程节点Id
        /// </summary>
        public string nodeId { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool isOk { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 表单数据信息
        /// </summary>
        public List<WFVerificationFormModel> formDataList { get; set; }
    }

    /// <summary>
    /// 表单数据信息
    /// </summary>
    public class WFVerificationFormModel{
        public string formId { get; set; }

        public string formData { get; set; }
    }


}
