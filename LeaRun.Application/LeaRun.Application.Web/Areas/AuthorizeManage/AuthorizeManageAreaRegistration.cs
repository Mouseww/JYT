using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AuthorizeManage
{
    /// <summary>
    /// 区域注册
    /// </summary>
    public class AuthorizeManageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// 区域名字
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "AuthorizeManage";
            }
        }
        /// <summary>
        /// 注册区域
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
