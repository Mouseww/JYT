using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.FormManage.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS 6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.11.29 21:19
    /// 描 述：表单模块
    /// </summary>
    public class FormModuleController : MvcControllerBase
    {
        private FormModuleBLL fromModuleBLL = new FormModuleBLL();
        private ModuleBLL moduleBLL = new ModuleBLL();

        #region 表单设计
        /// <summary>
        /// 表单设计管理列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单设计器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FormBuider()
        {
            return View();
        }
        /// <summary>
        /// 表单预览页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FormPreview()
        {
            return View();
        }



        /// <summary>
        /// 获取表单列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = fromModuleBLL.GetPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 获取表单模块数据列表,不分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = fromModuleBLL.GetEntityList(queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取表单某块json数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEntityJson(string keyValue)
        {
            var data = fromModuleBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 表单树 
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string queryJson)
        {
            var data = fromModuleBLL.GetEntityList(queryJson);
            var treeList = new List<TreeEntity>();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var item in data)
            {
                TreeEntity tree = new TreeEntity();
                if (!dic.ContainsKey(item.F_FrmCategory))
                {
                    dic.Add(item.F_FrmCategory, item.F_FrmCategory);
                    TreeEntity tree1 = new TreeEntity();
                    tree1.id = item.F_FrmCategory;
                    tree1.text = item.F_FrmCategory;
                    tree1.value = item.F_FrmCategory;
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.parentId = "0";
                    tree1.img = "fa fa-list-alt";
                    tree1.Attribute = "Sort";
                    tree1.AttributeValue = "formCategory";
                    treeList.Add(tree1);
                }
                tree.id = item.F_FrmId;
                tree.text = item.F_FrmName;
                tree.value = item.F_FrmId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = item.F_FrmCategory;
                tree.Attribute = "Sort";
                tree.AttributeValue = "form";
                tree.AttributeA = "version";
                tree.AttributeValueA = item.F_Version;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, FormModuleEntity entity)
        {
            fromModuleBLL.SaveEntity(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 启用，停用设计表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="flag">1启用，0停用</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult EnableOrDisableForm(string keyValue,int flag)
        {
            FormModuleEntity entity = new FormModuleEntity()
            {
                F_EnabledMark = flag
            };
            fromModuleBLL.SaveEntity(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除表单模板(虚拟)
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult VirtualRemove(string keyValue)
        {
            fromModuleBLL.VirtualRemoveEntity(keyValue);
            return Success("删除成功。");
        }
        #endregion

        #region 表单管理
        /// <summary>
        /// 表单管理列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ManageIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单发布，用于扩展属性和自定义表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FormReleaseer()
        {
            return View();
        }
        /// <summary>
        /// 获取表单管理列表（分页）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetRelationPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = fromModuleBLL.GetRelationPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 获取表单某块json数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRelationEntityJson(string keyValue)
        {
            var baseInfo = fromModuleBLL.GetRelationEntity(keyValue);
            ModuleEntity moduleInfo = null;
            if(baseInfo.F_FrmKind == 0)
            {
                moduleInfo = moduleBLL.GetEntity(baseInfo.F_ObjectId);
            }
            var data = new
            {
                baseInfo = baseInfo,
                moduleInfo = moduleInfo
            };

            return Content(data.ToJson());
        }
        /// <summary>
        /// 保存表单关联数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="baseInfo"></param>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveRelation(string keyValue, string baseInfo, string moduleInfo)
        {
            FormModuleRelationEntity entity = baseInfo.ToObject<FormModuleRelationEntity>();
            ModuleEntity moduleEntity = moduleInfo.ToObject<ModuleEntity>();
            fromModuleBLL.SaveRelationEntity(keyValue, entity, moduleEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除表单关联
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RelationRemove(string keyValue)
        {
            fromModuleBLL.RelationRemove(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 删除表单关联
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateForm(string keyValue)
        {
            fromModuleBLL.UpdateForm(keyValue);
            return Success("删除成功。");
        }
        #endregion

        #region 自定义表单
        /// <summary>
        /// 自定义表单管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult CustmerFormIndex()
        {
            return View();
        }
        /// <summary>
        /// 自定义表单表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult CustmerFormForm()
        {
            return View();
        }

        /// <summary>
        /// 获取表单内容通过关联关系
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormContentJson(string keyValue)
        {
           var data = fromModuleBLL.GetFormContentJson(keyValue);
           return Content(data.ToJson());
        }
        #endregion

        #region 表单实例操作
        /// <summary>
        /// 保存表单实例数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveCustmerFormInstance(string keyValue, FormModuleInstanceEntity entity)
        {
            var data = fromModuleBLL.SaveCustmerInstance(keyValue, entity);
            return Content(new AjaxResult { type = ResultType.success, message = "保存成功", resultdata = data }.ToJson());
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetInstanceEntityJson(string keyValue)
        {
            var data = fromModuleBLL.GetCustmerInstanceEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 删除自定表单数据实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="frmContentId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveInstanceForm(string keyValue, string frmContentId)
        {
            fromModuleBLL.RemoveCustmerInstanceEntity(keyValue, frmContentId);
            return Success("操作成功。");
        }
        /// <summary>
        /// 根据表单Id获取表单实例列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="relationFormId"></param>
        /// <returns></returns>
         [HttpGet]
        public ActionResult GetInstancePageList(Pagination pagination, string relationFormId)
        {
            var watch = CommonHelper.TimerStart();
            var data = fromModuleBLL.GetInstancePageList(pagination, relationFormId);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }

        #endregion 
    }
}