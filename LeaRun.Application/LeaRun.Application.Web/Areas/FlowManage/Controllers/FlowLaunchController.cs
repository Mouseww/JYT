using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Util;
using LeaRun.WorkFlow;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary> 
    /// 版 本 LearunADMS 6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.25 23:54
    /// 日 期：2016.03.19 14:27
    /// 描 述：流程发起
    /// </summary>
    public class FlowLaunchController : MvcControllerBase
    {
        private WFProcessBLL wfProcessBll = new WFProcessBLL();
        private WFSchemeInfoBLL wfFlowInfoBLL = new WFSchemeInfoBLL();
        private FormModuleBLL fromModuleBLL = new FormModuleBLL();

        #region 视图功能
        //
        // GET: /FlowManage/FlowLaunch/
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowProcessNewForm()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="wfCPParameterModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult CreateProcessInstance(WFCPParameterModel wfCPParameterModel)
        {
            wfProcessBll.CreateProcessInstance(wfCPParameterModel);
            return Success("创建成功");
        } 
        #endregion

        #region 获取数据
        /// <summary>
        /// 流程列表(流程发起页面)
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = wfFlowInfoBLL.GetList(queryJson);
            return Content(data.ToJson());
        }
        /// <summary>上
        /// 获取流程实体(用于流程创建)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFlowJson(string keyValue)
        {
            var schemeInfo = wfFlowInfoBLL.GetEntity(keyValue);
            IWfRuntime wfRuntime = new WfRuntime(schemeInfo.F_SchemeContent);
            WFNodeModel wfNodeModel = wfRuntime.getStartNode();
            FormModuleEntity formEntity = fromModuleBLL.GetEntity(wfNodeModel.setInfo.nodeForm);

            var dataJson = new {
                schemeInfo = schemeInfo,
                formEntity = formEntity,
                 currentNode = wfNodeModel
            };
            return Content(dataJson.ToJson());
        }
        #endregion
    }
}
