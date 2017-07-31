namespace LeaRun.CodeGenerator.Model
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：LR0101 2016.10.16 23.16
    /// 日 期：2016.1.15 9:54
    /// 描 述：表格字段信息
    /// </summary>
    public class GridColumnModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 字段Id
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 显示位置
        /// </summary>
        public string align { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool hidden { get; set; }
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool sortable { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string formatter { get; set; }
        /// <summary>
        /// 是否列表页中查询
        /// </summary>
        public bool query { get; set; }
    }
}
