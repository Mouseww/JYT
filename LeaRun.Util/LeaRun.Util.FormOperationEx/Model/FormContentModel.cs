using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.FormModule
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.14.16 14:16
    /// 描 述：表单设计内容类型
    /// </summary>
    public class FormContentModel
    {
        /// <summary>
        /// 表单类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 绑定的数据库ID
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// 绑定的数据库表
        /// </summary>
        public string dbTable { get; set; }
        /// <summary>
        /// 绑定的主键
        /// </summary>
        public string dbPkey { get; set; }
        /// <summary>
        /// 具体表单的内容
        /// </summary>
        public dynamic data { get; set; }
    }
    public class FormDataModel
    {
        /// <summary>
        /// 区域信息
        /// </summary>
        public area area { get; set; }
        /// <summary>
        /// 字段信息
        /// </summary>
        public List<fieldModel> fields { get; set; }
    }
    public class area
    {
        public string id { get; set; }
        public string type { get; set; }
        public string margintop { get; set; }
        public string sortCode { get; set; }
    }
    /// <summary>
    /// 自定义表单设计字段
    /// </summary>
    public class fieldModel
    {
        /// <summary>
        /// 字段显示名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 字段控件id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 字段绑定id
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 字段控件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string defaultValue { get; set; }
        /// <summary>
        /// 验证方式
        /// </summary>
        public string verify { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        public string sortCode { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 数据源类型 
        /// </summary>
        public string dataSource { get; set; }
        /// <summary>
        /// 绑定的数据字典Code
        /// </summary>
        public string dataItemCode { get; set; }
        /// <summary>
        /// 绑定的库id
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// 绑定的表名
        /// </summary>
        public string dbTable { get; set; }
        /// <summary>
        /// 绑定的需要显示的字段
        /// </summary>
        public string dbFiledText { get; set; }
        /// <summary>
        /// 绑定的保存的字段
        /// </summary>
        public string dbFiledValue { get; set; }
        /// <summary>
        /// 显示的日期格式
        /// </summary>
        public string dateformat { get; set; }
        /// <summary>
        /// 上传的文件格式
        /// </summary>
        public string fileformat { get; set; }
        /// <summary>
        /// 组织单位类型
        /// </summary>
        public string baseType { get; set; }
        /// <summary>
        /// 关联的上一级
        /// </summary>
        public string relation { get; set; }
        /// <summary>
        /// 当前显示信息类型
        /// </summary>
        public string infoType { get; set; }
    }

    
}
