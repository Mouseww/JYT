using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util;
using LeaRun.Util.Attributes;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : Controller
    {
        UserBLL user = new UserBLL();
        DepartmentBLL department = new DepartmentBLL();

        #region 视图功能
        /// <summary>
        /// 初始化页面
        /// </summary>
        public ActionResult Index()
        {
            var learn_UItheme = WebHelper.GetCookie("learn_UItheme");
            switch (learn_UItheme)
            {
                case "1":
                    return RedirectToAction("AdminDefault", "Home");
                case "2":
                    return RedirectToAction("AdminLTE", "Home");
                case "3":
                    return RedirectToAction("AdminWindos", "Home");
                case "4":
                    return RedirectToAction("AdminPretty", "Home");
                default:
                    return RedirectToAction("AdminDefault", "Home");
            }
        }
        /// <summary>
        /// 默认首页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {
            return View();
        }
        /// <summary>
        /// 默认首页桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefaultDesktop()
        {
            return View();
        }
        /// <summary>
        /// AdminLTE风格首页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminLTE()
        {
            return View();
        }
        /// <summary>
        /// AdminLTE风格首页桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminLTEDesktop()
        {
            return View();
        }
        /// <summary>
        /// Windos风格首页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminWindos()
        {
            return View();
        }
        /// <summary>
        /// Windos风格首页桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminWindosDesktop()
        {
            return View();
        }
        /// <summary>
        /// Pretty风格首页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminPretty() {
            return View();
        }
        /// <summary>
        /// Pretty风格首页桌面
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminPrettyDesktop()
        {
            return View();
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 2;
            logEntity.F_OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.F_OperateAccount = OperatorProvider.Provider.Current().Account;
            logEntity.F_OperateUserId = OperatorProvider.Provider.Current().UserId;
            logEntity.F_ModuleId = moduleId;
            logEntity.F_Module = moduleName;
            logEntity.F_ExecuteResult = 1;
            logEntity.F_ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Content(moduleId);
        }
        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }
        #endregion
    }
}
