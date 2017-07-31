using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.CodeGenerator;
using LeaRun.CodeGenerator.Model;
using LeaRun.CodeGenerator.Template;
using LeaRun.Util;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.GeneratorManage.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：L0101
    /// 日 期：2016.2.13 14:29
    /// 描 述：生成器多表
    /// </summary>
    public class MultiTableController : MvcControllerBase
    {
        private ModuleBLL moduleBLL = new ModuleBLL();

        #region 视图功能
        /// <summary>
        /// 代码生成器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CodeBuilder()
        {
            string OutputDirectory = Server.MapPath("~/Web.config"); ;
            for (int i = 0; i < 2; i++)
                OutputDirectory = OutputDirectory.Substring(0, OutputDirectory.LastIndexOf('\\'));
            ViewBag.OutputDirectory = OutputDirectory;
            ViewBag.UserName = OperatorProvider.Provider.Current().UserName;
            return View();
        }
        /// <summary>
        /// 编辑控件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditControl()
        {
            return View();
        }
        /// <summary>
        /// 编辑详情页控件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditDetailsControl()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 查看代码
        /// </summary>
        /// <param name="baseInfoJson">基本信息配置</param>
        /// <param name="gridPrimaryFieldJson"></param>
        /// <param name="gridDetailsFieldJson"></param>
        /// <param name="formPrimaryFieldJson"></param>
        /// <param name="formDetailsFieldJson"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookCode(string baseInfoJson, string gridPrimaryFieldJson, string gridDetailsFieldJson, string formPrimaryFieldJson, string formDetailsFieldJson)
        {
            MultiTableConfigModel multiTableConfigModel = baseInfoJson.ToObject<MultiTableConfigModel>();
            var gridPrimary = gridPrimaryFieldJson.ToList<GridColumnModel>();
            var gridDetails = gridDetailsFieldJson.ToList<GridColumnModel>();
            var formPrimary = formPrimaryFieldJson.ToList<FormFieldModel>();
            var formDetails = formDetailsFieldJson.ToList<FormFieldModel>();

            var tablePrimaryFields = new DataBaseTableBLL(multiTableConfigModel.DataBaseLinkId).GetTableFiledList(multiTableConfigModel.DataBaseLinkId, multiTableConfigModel.DataBaseTableName);
            var tableDetailsFields = new DataBaseTableBLL(multiTableConfigModel.DataBaseLinkId).GetTableFiledList(multiTableConfigModel.DataBaseLinkId, multiTableConfigModel.ChildTableName);

            DataBaseLinkEntity dataBaseLinkEntity = new DataBaseLinkBLL().GetEntity(multiTableConfigModel.DataBaseLinkId);
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
            if (connectionString != dataBaseLinkEntity.F_DbConnection)
            {
                if (ConfigurationManager.ConnectionStrings[multiTableConfigModel.DataBaseName] == null)
                {
                    multiTableConfigModel.DataBaseName = dataBaseLinkEntity.F_DBName;
                    multiTableConfigModel.DataBaseType = dataBaseLinkEntity.F_DbType;
                    string providerName = "System.Data.SqlClient";
                    if (dataBaseLinkEntity.F_DbType == "MySql")
                    {
                        providerName = "MySql.Data.MySqlClient";
                    }
                    else if (dataBaseLinkEntity.F_DbType == "Oracle")
                    {
                        providerName = "Oracle.ManagedDataAccess.Client";
                    }
                    Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dataBaseLinkEntity.F_DBName, dataBaseLinkEntity.F_DbConnection, providerName);
                }
            }
            else
            {
                multiTableConfigModel.DataBaseName = "";
            }

            MultiTable multiTable = new MultiTable();

            string entitybuilder = multiTable.EntityBuilder(multiTableConfigModel, DataHelper.ListToDataTable<DataBaseTableFieldEntity>(tablePrimaryFields.ToList()), false);//主表实体
            string childEntitybuilder = multiTable.EntityBuilder(multiTableConfigModel, DataHelper.ListToDataTable<DataBaseTableFieldEntity>(tableDetailsFields.ToList()), true);//子表实体

            string entitymapbuilder = multiTable.EntityMapBuilder(multiTableConfigModel, false);//实体映射类
            string childEntitymapbuilder = multiTable.EntityMapBuilder(multiTableConfigModel, true);
            //服务类
            string servicebuilder = multiTable.ServiceBuilder(multiTableConfigModel,gridPrimary);
            //服务接口类
            string iservicebuilder = multiTable.IServiceBuilder(multiTableConfigModel);
            //业务类
            string businesbuilder = multiTable.BusinesBuilder(multiTableConfigModel);
            //控制器
            string controllerbuilder = multiTable.ControllerBuilder(multiTableConfigModel);
            //主页面
            string indexbuilder = multiTable.IndexBuilder(multiTableConfigModel, gridPrimary, gridDetails);
            //表单页面
            string formbuilder = multiTable.FormBuilder(multiTableConfigModel, formPrimary, formDetails);

            var jsonData = new
            {
                entityCode = entitybuilder,
                entityChildCode = childEntitybuilder,
                entitymapCode = entitymapbuilder,
                entitymapChildCode = childEntitymapbuilder,
                serviceCode = servicebuilder,
                iserviceCode = iservicebuilder,
                businesCode = businesbuilder,
                controllerCode = controllerbuilder,
                indexCode = indexbuilder,
                formCode = formbuilder
            };
            return ToJsonResult(jsonData);
        }

        /// <summary>
        /// 创建代码（自动写入VS里面目录）
        /// </summary>
        /// <param name="baseInfoJson">基本信息配置Json</param>
        /// <param name="strCode">生成代码内容</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateCode(string baseInfoJson, string strCode)
        {
            MultiTableConfigModel baseConfigModel = baseInfoJson.ToObject<MultiTableConfigModel>();
            CreateCodeFile.CreateExecution(baseConfigModel, Server.UrlDecode(strCode));
            return Success("恭喜您，创建成功！");
        }
        /// <summary>
        /// 发布功能（自动创建导航菜单）
        /// </summary>
        /// <param name="baseInfoJson">基本信息配置Json</param>
        /// <param name="moduleEntity">功能实体</param>
        /// <param name="moduleButtonListJson">按钮实体列表</param>
        /// <param name="moduleColumnListJson">视图实体列表</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult PublishModule(string baseInfoJson, string ModuleEnCode, string FullName, string ParentId, string Icon, string Description)
        {
            MultiTableConfigModel baseConfigModel = baseInfoJson.ToObject<MultiTableConfigModel>();
            var urlAddress = "/" + baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "/" + baseConfigModel.IndexPageName;
            var formUrlAddress = "/" + baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "/" + baseConfigModel.FormPageName;
            var deleteUrlAddress = "/" + baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "/RemoveForm";
            var detailUrlAddress = "/" + baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "/Detail";
            ModuleEntity moduleEntity = new ModuleEntity();

            moduleEntity.F_EnCode = ModuleEnCode;
            moduleEntity.F_FullName = FullName;
            moduleEntity.F_ParentId = ParentId;
            moduleEntity.F_Icon = Icon;
            moduleEntity.F_Description = Description;
            moduleEntity.F_SortCode = moduleBLL.GetSortCode();
            moduleEntity.F_IsMenu = 1;
            moduleEntity.F_EnabledMark = 1;
            moduleEntity.F_Target = "iframe";
            moduleEntity.F_UrlAddress = urlAddress;
            string buttonListJson = "[{\"F_ParentId\":\"0\",\"F_EnCode\":\"lr-replace\",\"F_FullName\":\"刷新\",\"F_SortCode\":0},";
            buttonListJson += "{\"F_ParentId\":\"0\",\"F_EnCode\":\"lr-add\",\"F_FullName\":\"新增\",\"F_ActionAddress\":\"" + formUrlAddress + "\",\"F_SortCode\":1},";
            buttonListJson += "{\"F_ParentId\":\"0\",\"F_EnCode\":\"lr-edit\",\"F_FullName\":\"编辑\",\"F_ActionAddress\":\"" + formUrlAddress + "\",\"F_SortCode\":2},";
            buttonListJson += "{\"F_ParentId\":\"0\",\"F_EnCode\":\"lr-delete\",\"F_FullName\":\"删除\",\"F_ActionAddress\":\"" + deleteUrlAddress + "\",\"F_SortCode\":3},";
            buttonListJson += "{\"F_ParentId\":\"0\",\"F_EnCode\":\"lr-detail\",\"F_FullName\":\"明细\",\"F_ActionAddress\":\"" + detailUrlAddress + "\",\"F_SortCode\":4}]";
            moduleBLL.SaveForm("", moduleEntity, buttonListJson, "");
            return Success("发布成功！");
        }
        #endregion

        #region 处理数据
        #endregion
    }
}
