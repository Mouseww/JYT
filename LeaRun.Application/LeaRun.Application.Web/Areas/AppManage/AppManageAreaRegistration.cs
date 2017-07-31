using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AppManage
{
    /// <summary>
    /// 区域注册
    /// </summary>
    public class AppManageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// 区域名字
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "AppManage";
            }
        }
        /// <summary>
        /// 注册区域
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AppManage_default",
                "AppManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
