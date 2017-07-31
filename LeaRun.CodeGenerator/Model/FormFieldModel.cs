namespace LeaRun.CodeGenerator.Model
{
    /// <summary>
    /// 版 本 LearunADMS 6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：LR0101 2016:11:08 10:52
    /// 日 期：2016.1.15 9:54
    /// 描 述：表单字段信息
    /// </summary>
    public class FormFieldModel
    {
        /// <summary>
        /// 字段标识
        /// </summary>
        public string controlId { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string controlName { get; set; }
        /// <summary>
        /// 字段验证
        /// </summary>
        public string controlValidator { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string controlType { get; set; }
        /// <summary>
        /// 合并列
        /// </summary>
        public int? controlColspan { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string controlDefault { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public string controlDataSource { get; set; }
        /// <summary>
        /// 数据字典分类编码
        /// </summary>
        public string controlDataItemEncodeValue { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string controlDataDb { get; set; }
        /// <summary>
        /// 数据表
        /// </summary>
        public string controlDataTable { get; set; }
        /// <summary>
        /// 下拉框绑定字段Id
        /// </summary>
        public string controlDataFliedId { get; set; }
        /// <summary>
        /// 下拉框绑定字段text
        /// </summary>
        public string controlDataFliedText { get; set; }

        /// <summary>
        /// 是否影藏
        /// </summary>
        public bool controlHidden { get; set; }
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool controlSortable { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string controlAlign { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public string controlWidth { get; set; }
        /// <summary>
        /// 数据格式化
        /// </summary>
        public string controlFormatter { get; set; }
    }
}
