
namespace LeaRun.Application.Entity.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用自定义菜单
    /// </summary>
    public class WeChatAppMenuEntity
    {
        /// <summary>
        /// 菜单主键
        /// </summary>
        public string F_MenuId { get; set; }
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string F_MenuName { get; set; }
        /// <summary>
        /// 跳转URL
        /// </summary>
        public string F_MenuUrl { get; set; }
        /// <summary>
        /// 菜单的响应动作类型
        /// </summary>
        public string F_MenuType { get; set; }
        /// <summary>
        /// 菜单的响应动作类型
        /// </summary>
        public string F_MenuTypeName { get; set; }
        /// <summary>
        /// 菜单等级
        /// </summary>
        public int? F_Level { get; set; }
        /// <summary>
        /// 菜单节点
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? F_SortCode { get; set; }
    }
}
