using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.25 10.36
    /// 日 期：2015.11.02 14:27
    /// 描 述：流程设计
    /// </summary>
    public class FlowDesignController : MvcControllerBase
    {
        private WFSchemeInfoBLL wfFlowInfoBLL = new WFSchemeInfoBLL();

        #region 视图功能
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
        /// 节点设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowNodeForm()
        {
            return View();
        }
        /// <summary>
        /// 连接线设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowLineForm()
        {
            return View();
        }
        /// <summary>
        /// 路程模板设计器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult FlowSchemeDesigner()
        {
            return View();
        }
        /// <summary>
        /// 流程模板预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult FlowPreviewIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 流程列表(分页)（管理页面使用）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = wfFlowInfoBLL.GetPageList(pagination, queryJson);
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
        /// 获取流程设计
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var schemeinfo = wfFlowInfoBLL.GetEntity(keyValue);
            var authorize = wfFlowInfoBLL.GetAuthorizeEntityList(schemeinfo.F_Id);
            var JsonData = new
            {
                schemeinfo = schemeinfo,
                authorize = authorize
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 流程模板树 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            var data = wfFlowInfoBLL.GetList();
            var treeList = new List<TreeEntity>();
            string schemeType = "";
            foreach (DataRow item in data.Rows)
            {
                TreeEntity tree = new TreeEntity();
                if (schemeType != item["F_ItemId"].ToString())
                {
                    TreeEntity tree1 = new TreeEntity();
                    schemeType = item["F_ItemId"].ToString();
                    tree1.id = schemeType;
                    tree1.text = item["F_ItemName"].ToString();
                    tree1.value = schemeType;
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.parentId = "0";
                    tree1.img = "fa fa-list-alt";
                    tree1.Attribute = "Sort";
                    tree1.AttributeValue = "SchemeType";
                    treeList.Add(tree1);
                }
                tree.id = item["F_Id"].ToString();
                tree.text = item["F_SchemeName"].ToString();
                tree.value = item["F_Id"].ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = schemeType;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Scheme";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程设计数据（保存，编辑）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="baseEntity"></param>
        /// <param name="contentEntity"></param>
        /// <param name="authorizeData"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string baseEntity, string authorizeData)
        {
            WFSchemeInfoEntity entyity = baseEntity.ToObject<WFSchemeInfoEntity>();
            wfFlowInfoBLL.SaveForm(keyValue, entyity, authorizeData.Split(','));
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除表单模板
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            wfFlowInfoBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// （启用、禁用）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitUpdateState(string keyValue, int State)
        {
            wfFlowInfoBLL.UpdateState(keyValue, State);
            return Success("操作成功。");
        }
        #endregion

        #region 获取权限数据
        private RoleBLL roleBLL = new RoleBLL();
        private PostBLL postBLL = new PostBLL();
        private UserGroupBLL userGroupBLL = new UserGroupBLL();
        private UserBLL userBLL = new UserBLL();
        /// <summary>
        /// 角色列表树 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetRoleCheckTreeJson(string keyword)
        {
            var data = roleBLL.GetAllList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            string companyname = "";
            int num = 0;
            foreach (RoleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                if (companyname != item.F_OrganizeId)
                {
                    num++;
                    TreeEntity tree1 = new TreeEntity();
                    companyname = item.F_OrganizeId;
                    tree1.id = num + "";
                    tree1.text = companyname;
                    tree1.value = companyname;
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.parentId = "0";
                    tree1.img = "fa fa-home";
                    treeList.Add(tree1);
                }
                tree.id = item.F_RoleId;
                tree.text = item.F_FullName;
                tree.value = item.F_RoleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = num + "";
                tree.showcheck = true;
                tree.img = "fa fa-user";
                tree.Attribute = "mytype";
                tree.AttributeValue = "Role";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 岗位列表树 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetPostCheckTreeJson(string keyword)
        {
            var data = postBLL.GetAllList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            string companyname = "";
            int num = 0;
            foreach (RoleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                if (companyname != item.F_OrganizeId)
                {
                    num++;
                    TreeEntity tree1 = new TreeEntity();
                    companyname = item.F_OrganizeId;
                    tree1.id = num + "";
                    tree1.text = companyname;
                    tree1.value = companyname;
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.parentId = "0";
                    tree1.img = "fa fa-home";
                    treeList.Add(tree1);

                }
                tree.id = item.F_RoleId;
                tree.text = item.F_FullName;
                tree.value = item.F_RoleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = num + "";
                tree.showcheck = true;
                tree.img = "fa fa-user";
                tree.Attribute = "mytype";
                tree.AttributeValue = "Post";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 用户列表树 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetUserCheckTreeJson()
        {
            var data = userBLL.GetAllTable();
            var treeList = new List<TreeEntity>();
            string companyid = "";
            string departmentid = "";
            foreach (DataRow item in data.Rows)
            {
                TreeEntity tree = new TreeEntity();
                if (companyid != item["F_OrganizeId"].ToString())
                {
                    TreeEntity tree1 = new TreeEntity();
                    companyid = item["F_OrganizeId"].ToString();
                    tree1.id = item["F_OrganizeId"].ToString();
                    tree1.text = item["F_OrganizeName"].ToString();
                    tree1.value = item["F_OrganizeId"].ToString();
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.Attribute = "Sort";
                    tree1.AttributeValue = "Organize";
                    tree1.parentId = "0";
                    tree1.img = "fa fa-home";
                    treeList.Add(tree1);
                }
                if (departmentid != item["F_DepartmentId"].ToString() && !string.IsNullOrEmpty(item["F_DepartmentId"].ToString()))
                {
                    TreeEntity tree1 = new TreeEntity();
                    departmentid = item["F_DepartmentId"].ToString();
                    tree1.id = item["F_DepartmentId"].ToString();
                    tree1.text = item["F_DepartmentName"].ToString();
                    tree1.value = item["F_DepartmentId"].ToString();
                    tree1.isexpand = false;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.Attribute = "Sort";
                    tree1.AttributeValue = "Department";
                    tree1.parentId = item["F_OrganizeId"].ToString();
                    tree1.img = "fa fa-umbrella";
                    treeList.Add(tree1);
                }
                tree.id = item["F_UserId"].ToString();
                tree.text = item["F_RealName"].ToString();
                tree.value = item["F_UserId"].ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = string.IsNullOrEmpty(item["F_DepartmentId"].ToString()) ? item["F_OrganizeId"].ToString() : item["F_DepartmentId"].ToString();
                tree.showcheck = true;
                tree.img = "fa fa-user";
                tree.Attribute = "mytype";
                tree.AttributeValue = "User";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 用户组列表树 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetUserGroupCheckTreeJson(string keyword)
        {
            var data = userGroupBLL.GetAllList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            string companyname = "";
            int num = 0;
            foreach (RoleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                if (companyname != item.F_OrganizeId)
                {
                    num++;
                    TreeEntity tree1 = new TreeEntity();
                    companyname = item.F_OrganizeId;
                    tree1.id = num + "";
                    tree1.text = companyname;
                    tree1.value = companyname;
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.parentId = "0";
                    tree1.img = "fa fa-home";
                    treeList.Add(tree1);
                }
                tree.id = item.F_RoleId;
                tree.text = item.F_FullName;
                tree.value = item.F_RoleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = num + "";
                tree.showcheck = true;
                tree.img = "fa fa-user";
                tree.Attribute = "mytype";
                tree.AttributeValue = "UserGroup";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        #endregion
    }
}
