using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using LeaRun.WorkFlow;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.19 13:57
    /// 描 述:我的流程
    /// </summary>
    public class FlowMyProcessController : MvcControllerBase
    {
        private WFProcessBLL wfProcessBll = new WFProcessBLL();
        private FormModuleBLL formModuleBLL = new FormModuleBLL();

        #region 视图功能
        /// <summary>
        /// 我的流程实例页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 进度查看
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProcessLookForm()
        {
            return View();
        }
        /// <summary>
        /// 重新发起流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProcessEditForm()
        {
            return View();
        }
        #endregion

        #region 获取流程数据
        /// <summary>
        /// 获取我的流程实例数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            pagination.page++;
            var data = wfProcessBll.GetMyProcessPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 获取进程模板Json
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetProcessJson(string keyValue)
        {
            WFProcessInstanceEntity processEntity = wfProcessBll.GetProcessEntity(keyValue);
            WFProcessSchemeEntity processSchemeEntity = wfProcessBll.GetProcessSchemeEntity(processEntity.F_ProcessSchemeId);
            var nodeList = wfProcessBll.GetProcessNodeList(keyValue).ToList();
            nodeList = nodeList.OrderBy(t => t.F_NodeId).ThenBy(t => t.F_CreateDate).ToList();
            var dataJson = new
            {
                processEntity = processEntity,
                processSchemeEntity = processSchemeEntity,
                nodeList = nodeList
            };

            return Content(dataJson.ToJson());
        }
        /// <summary>
        /// 获取进程模板Json
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetProcessNodeJson(string keyValue)
        {
            WFProcessInstanceEntity processEntity = wfProcessBll.GetProcessEntity(keyValue);
            WFProcessSchemeEntity processSchemeEntity = wfProcessBll.GetProcessSchemeEntity(processEntity.F_ProcessSchemeId);
            var nodeList = wfProcessBll.GetProcessNodeList(keyValue);
            IWfRuntime wfRuntime = new WfRuntime(processSchemeEntity.F_SchemeContent);
            WFNodeModel node = wfRuntime.getStartNode();
            Dictionary<string, FormModuleEntity> formDataLsit = new Dictionary<string, FormModuleEntity>();

            FormModuleEntity formNewEntity = formModuleBLL.GetEntity(node.setInfo.nodeForm);
            formDataLsit.Add(node.setInfo.nodeForm, formNewEntity);
            List<FormModuleEntity> formEntityList = new List<FormModuleEntity>();
            Dictionary<string, FormModuleInstanceEntity> dFormData = new Dictionary<string, FormModuleInstanceEntity>();
            var formdata = formModuleBLL.GetInstanceList(keyValue);
            foreach (var item in formdata)
            {
                Form_ModuleContentEntity formEntity = formModuleBLL.GetFormContentEntity(item.F_FrmContentId);
                if (formDataLsit.ContainsKey(formEntity.F_FrmId) && !dFormData.ContainsKey(formEntity.F_FrmId))
                {
                    formDataLsit[formEntity.F_FrmId].F_FrmContent = formEntity.F_FrmContent;
                    formEntityList.Add(formDataLsit[formEntity.F_FrmId]);
                    dFormData.Add(formEntity.F_FrmId, item);
                }
            }
            if(formEntityList.Count == 0)
            {
                formEntityList.Add(formNewEntity);
            }
            var dataJson = new
            {
                processEntity = processEntity,
                processSchemeEntity = processSchemeEntity,
                nodeList = nodeList,
                formEntityList = formEntityList,
                dFormData = dFormData,
                currentNode = node
            };

            return Content(dataJson.ToJson());
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
        public ActionResult EditProcessInstance(WFCPParameterModel wfCPParameterModel)
        {
            wfProcessBll.EditProcessInstance(wfCPParameterModel);
            return Success("发起成功");
        }
        #endregion
    }
}
