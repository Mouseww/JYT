
namespace LeaRun.Util.FormModule
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR215
    /// 日 期：2016.09.15 16:58
    /// 描 述：自定义表单实体类
    /// </summary>
    public class FormInstanceModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 数据字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 当前数据类型
        /// </summary>
        public string infoType { get; set; }
        /// <summary>
        /// 真实的值，数据字典或数据源翻译过的
        /// </summary>
        public string realValue { get; set; }
    }
}
