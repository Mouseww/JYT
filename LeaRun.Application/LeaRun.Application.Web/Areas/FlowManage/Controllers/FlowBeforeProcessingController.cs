using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using LeaRun.WorkFlow;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101
    /// 日 期：2016.03.19 13:57
    /// 描 述:已办流程
    /// </summary>
    public class FlowBeforeProcessingController : MvcControllerBase
    {
        private WFProcessBLL wfProcessBll = new WFProcessBLL();
        private FormModuleBLL formModuleBLL = new FormModuleBLL();

        #region 视图功能
        //
        // GET: /FlowManage/FlowBeforeProcessing/
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 审核流程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult VerificationFrom()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 审核流程
        /// </summary>
        ///<param name="data">流程数据</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult VerificationProcess(WFVerificationParameterModel data)
        {
            wfProcessBll.NodeVerification(data);
            return Success("审核成功。");
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取代办的工作流程,运行中(分页)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            pagination.page++;
            var data = wfProcessBll.GetToMeBeforePageList(pagination, queryJson);
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
        /// <param name="nodeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetProcessJson(string keyValue,string nodeId)
        {
            WFProcessInstanceEntity processEntity = wfProcessBll.GetProcessEntity(keyValue);
            WFProcessSchemeEntity processSchemeEntity = wfProcessBll.GetProcessSchemeEntity(processEntity.F_ProcessSchemeId);
            var nodeList = wfProcessBll.GetProcessNodeList(keyValue);
            IWfRuntime wfRuntime = new WfRuntime(processSchemeEntity.F_SchemeContent);
            WFNodeModel node =  wfRuntime.getCurrentNode(nodeId);

            Dictionary<string, FormModuleEntity> formDataLsit = new Dictionary<string, FormModuleEntity>();
            foreach (var item in node.setInfo.formHaven)
            {
                FormModuleEntity formNewEntity =  formModuleBLL.GetEntity(item);
                formDataLsit.Add(item, formNewEntity);
            }
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
    }
}
