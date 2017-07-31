using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AppManage.Controllers
{
    /// <summary>
    /// app设计控制器
    /// </summary>
    public class AppDesignController : Controller
    {
        /// <summary>
        /// 设计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 手机设计页（水果风格）
        /// </summary>
        /// <returns></returns>
        public ActionResult PhoneDIndex()
        {
            return View();
        }

    }
}
