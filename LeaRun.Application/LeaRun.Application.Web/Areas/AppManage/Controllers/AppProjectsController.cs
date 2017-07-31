using LeaRun.Application.Busines.AppManage;
using LeaRun.Application.Entity.AppManage;
using System;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AppManage.Controllers
{
    /// <summary>
    /// app项目管理控制器
    /// </summary>
    public class AppProjectsController : MvcControllerBase
    {
        private AppProjectBLL appprojectbll = new AppProjectBLL();
        private AppTemplatesBLL appTemplatesBLL = new AppTemplatesBLL();
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = appprojectbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = appprojectbll.GetEntity(keyValue);
            var templatesData = appTemplatesBLL.GetEntityByProjectId(data.F_Id);

            var JsonData = new
            {
                project = data,
                templates = templatesData
            };

            return ToJsonResult(JsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            appprojectbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveForm(string keyValue, AppProjectModel entity)
        {
            appprojectbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 下载设计页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DownForm(string keyValue, AppProjectModel entity)
        {
            string OutputDirectory = Server.MapPath("~/Web.config"); ;
            for (int i = 0; i < 2; i++)
                OutputDirectory = OutputDirectory.Substring(0, OutputDirectory.LastIndexOf('\\'));
            string fileName = entity.F_Name + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string res = appprojectbll.DownForm(keyValue, entity, OutputDirectory + "\\AppBuildPath\\" + fileName);
            var data = new {
                path = fileName
            };
            return ToJsonResult(data);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public void DownFile(string filename)
        {
            string OutputDirectory = Server.MapPath("~/Web.config"); ;
            for (int i = 0; i < 2; i++)
                OutputDirectory = OutputDirectory.Substring(0, OutputDirectory.LastIndexOf('\\'));
            appprojectbll.DownFile(OutputDirectory + "\\AppBuildPath\\" + filename + ".zip");
        }
        #endregion

    }
}
