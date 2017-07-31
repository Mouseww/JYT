namespace LeaRun.CodeGenerator.Model
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR215
    /// 日 期：2016.09.06 14:47
    /// 描 述：代码生成模板（WebApp）
    /// </summary>
    public class AppAttrModel
    {
        public string value { get; set; }
    }
    public class AppWeightModel
    {
        public string size { get; set; }
        public string isItalic { get; set; }
    }
    /// <summary>
    /// 页面
    /// </summary>
    public class AppPageAttrModel : AppAttrModel
    {
        public string routing { get; set; }
        public string bgColor { get; set; }
        public string isTabed { get; set; }
        public string isPadding { get; set; }
        public string isHeadHide { get; set; }
        public string isFirst { get; set; }
    }
    /// <summary>
    /// 标题
    /// </summary>
    public class AppHeadAttrModel : AppAttrModel
    {
        public string size { get; set; }
        public string color { get; set; }
        public string align { get; set; }
        public string text { get; set; }
        public AppWeightModel weight { get; set; }
    }
    /// <summary>
    /// 段落
    /// </summary>
    public class AppParagraphAttrModel : AppAttrModel
    {
        public string size { get; set; }
        public string color { get; set; }
        public string align { get; set; }
        public string content { get; set; }
    }
    /// <summary>
    /// 按钮
    /// </summary>
    public class AppBtnAttrModel : AppAttrModel
    {
        public string link { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string align { get; set; }
        public string text { get; set; }
        public AppWeightModel weight { get; set; }
        public string btnType { get; set; }
        public string btnTheme { get; set; }
        public string btnSize { get; set; }

    }
    /// <summary>
    /// 输入框
    /// </summary>
    public class AppInputAttrModel : AppAttrModel
    {
        public string placeholder { get; set; }
        public string inputType { get; set; }
        public string name { get; set; }

    }
    /// <summary>
    /// 类型列表一
    /// </summary>
    public class ApplrList3AttrModel : AppAttrModel
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string color { get; set; }
        public string link { get; set; }

    }
    /// <summary>
    /// 类型列表二
    /// </summary>
    public class ApplrList4AttrModel : AppAttrModel
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string color { get; set; }
        public string link { get; set; }

    }
    /// <summary>
    /// tab面板项
    /// </summary>
    public class AppTabsAttrModel : AppAttrModel
    {
        public string bgColor { get; set; }
        public string iconColor { get; set; }
        public string iconType { get; set; }

    }
    /// <summary>
    /// tab按钮项
    /// </summary>
    public class AppTabAttrModel:AppAttrModel
    {
        public string iconOn { get; set; }
        public string iconOff { get; set; }
        public string innerTabPage { get; set; }
    }
}
