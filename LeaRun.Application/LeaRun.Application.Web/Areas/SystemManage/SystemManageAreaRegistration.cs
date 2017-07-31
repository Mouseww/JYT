using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage
{
    /// <summary>
    /// 系统管理路由注册
    /// </summary>
    public class SystemManageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "SystemManage";
            }
        }
        /// <summary>
        /// 路由注册
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "LeaRun.Application.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
