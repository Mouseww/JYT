
namespace LeaRun.Util.FormModule{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR215
    /// 日 期：2016.09.15 16:58
    /// 描 述：自定义表单基础类
    /// </summary>
    public class FormBaseModel
    {
        /// <summary>
        /// 字段说明
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 所绑定的字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 是否是必填项
        /// </summary>
        public string required { get; set; }
    }
}
