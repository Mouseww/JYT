using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.DemoManage.Controllers
{
    public class DeomJsController : Controller
    {
        #region 视图功能
        /// <summary>
        /// 展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

    }
}