using LeaRun.Application.Busines;
using LeaRun.Application.Entity.ReportManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using LeaRun.Application.Busines.ReportManage;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表管理
    /// </summary>
    public class ReportController : MvcControllerBase
    {
        RptTempBLL rptTempBLL = new RptTempBLL();
        ModuleBLL modulebll = new ModuleBLL();

        #region 视图功能
        /// <summary>
        /// 报表管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 报表表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReportGuide()
        {
            return View();
        }
        /// <summary>
        /// 报表预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ReportPreview()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获得报表列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetListJson(string queryJson)
        {
            return Content(rptTempBLL.GetList(queryJson).ToJson());
        }
        /// <summary>
        /// 获得报表实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rptTempBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获得报表数据 
        /// </summary>
        /// <param name="reportId">报表主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetReportJson(string reportId)
        {
            var reportJson = rptTempBLL.GetReportData(reportId);
            return Content(reportJson);
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
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            rptTempBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tempJson">对象Json</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string tempJson)
        {
            dynamic RptTempJson = tempJson.ToJson();
            RptTempEntity rptTempEntity = new RptTempEntity();
            ModuleEntity moduleEntity = new ModuleEntity();
            rptTempEntity.F_EnCode = RptTempJson.F_EnCode;
            rptTempEntity.F_Description = RptTempJson.TempDescription;
            rptTempEntity.F_TempType = RptTempJson.F_TempType;
            rptTempEntity.F_FullName = RptTempJson.F_FullName;
            rptTempEntity.F_TempCategory = RptTempJson.F_TempCategory;
            StringBuilder rptJson = new StringBuilder();
            rptJson.Append("{");
            rptJson.AppendFormat("    \"title\":\"{0}\",", RptTempJson.title);//标题
            rptJson.AppendFormat("    \"sqlString\":\"{0}\",", RptTempJson.sqlString);
            rptJson.AppendFormat("    \"ParentId\":\"{0}\",", RptTempJson.ParentId);
            rptJson.AppendFormat("    \"Icon\":\"{0}\",", RptTempJson.Icon);
            rptJson.AppendFormat("    \"Description\":\"{0}\",", RptTempJson.Description);
            rptJson.AppendFormat("    \"listSqlString\":\"{0}\"", RptTempJson.listSqlString);
            rptJson.Append(" }"); rptJson.Replace("\n", "");
            rptTempEntity.F_ParamJson = rptJson.ToString();
            string parentId = RptTempJson.ParentId;
            if (!string.IsNullOrEmpty(parentId))
            {
                moduleEntity.Create();
                moduleEntity.F_ParentId = parentId;
                moduleEntity.F_Icon = RptTempJson.Icon;
                moduleEntity.F_Description = RptTempJson.Description;
                moduleEntity.F_IsMenu = 1;
                moduleEntity.F_FullName = rptTempEntity.F_FullName;
                moduleEntity.F_EnCode = rptTempEntity.F_EnCode;
                moduleEntity.F_EnabledMark = 1;
                moduleEntity.F_Target = "iframe";
                moduleEntity.F_SortCode = modulebll.GetSortCode();
            }
            rptTempBLL.SaveForm(keyValue, rptTempEntity, moduleEntity);
            return Success("操作成功。");
        }
        #endregion
    }
}
