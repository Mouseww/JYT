using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.1 1:35
    /// 描 述：用户组权限
    /// </summary>
    public class PermissionUserGroupController : MvcControllerBase
    {
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        private DepartmentBLL departmentBLL = new DepartmentBLL();
        private DepartmentCache departmentCache = new DepartmentCache();
        private RoleBLL roleBLL = new RoleBLL();
        private UserBLL userBLL = new UserBLL();
        private ModuleBLL moduleBLL = new ModuleBLL();
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();
        private PermissionBLL permissionBLL = new PermissionBLL();
        private AuthorizeBLL authorizeBLL = new AuthorizeBLL();

        #region 视图功能
        /// <summary>
        /// 用户组权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllotAuthorize()
        {
            return View();
        }
        /// <summary>
        /// 用户组成员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllotMember()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 部门列表 
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetDepartmentTreeJson(string userGroupId)
        {
            var roleEntity = roleBLL.GetEntity(userGroupId);
            var organizeEntity = organizeBLL.GetEntity(roleEntity.F_OrganizeId);
            var data = departmentCache.GetList(roleEntity.F_OrganizeId);

            var treeList = new List<TreeEntity>();
            TreeEntity tree = new TreeEntity();
            tree.id = organizeEntity.F_OrganizeId;
            tree.text = organizeEntity.F_FullName;
            tree.value = organizeEntity.F_OrganizeId;
            tree.isexpand = true;
            tree.complete = true;
            tree.hasChildren = true;
            tree.parentId = "0";
            treeList.Add(tree);
            foreach (DepartmentEntity item in data)
            {
                tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_DepartmentId) == 0 ? false : true;
                tree.id = item.F_DepartmentId;
                tree.text = item.F_FullName;
                tree.value = item.F_DepartmentId;
                if (item.F_ParentId == "0")
                {
                    tree.parentId = roleEntity.F_OrganizeId;
                }
                else
                {
                    tree.parentId = item.F_ParentId;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserListJson(string userGroupId)
        {
            var existMember = permissionBLL.GetMemberList(userGroupId);
            var userdata = userBLL.GetTable();
            userdata.Columns.Add("f_ischeck", Type.GetType("System.Int32"));//自定义添加一列 判断是否勾选
            foreach (DataRow item in userdata.Rows)
            {
                string UserId = item["f_userid"].ToString();
                item["f_ischeck"] = existMember.Count(t => t.F_UserId == UserId);
            }
            userdata = DataHelper.DataFilter(userdata, "", "f_ischeck desc");
            return Content(userdata.ToJson());
        }
        /// <summary>
        /// 系统功能列表
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleTreeJson(string userGroupId)
        {
            var existModule = permissionBLL.GetModuleList(userGroupId);
            var data = moduleBLL.GetList();
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_ModuleId) == 0 ? false : true;
                tree.id = item.F_ModuleId;
                tree.text = item.F_FullName;
                tree.value = item.F_ModuleId;
                tree.title = "";
                tree.checkstate = existModule.Count(t => t.F_ItemId == item.F_ModuleId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.F_ParentId;
                tree.img = item.F_Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 系统按钮列表
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleButtonTreeJson(string userGroupId)
        {
            var existModuleButton = permissionBLL.GetModuleButtonList(userGroupId);
            var moduleButtonData = authorizeBLL.GetModuleButtonList(SystemInfo.CurrentUserId);
            var moduleData = authorizeBLL.GetModuleListByModuleButton(SystemInfo.CurrentUserId, moduleButtonData);
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in moduleData)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.F_ModuleId;
                tree.text = item.F_FullName;
                tree.value = item.F_ModuleId;
                tree.checkstate = existModuleButton.Count(t => t.F_ItemId == item.F_ModuleId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.parentId = item.F_ParentId;
                tree.img = item.F_Icon;
                treeList.Add(tree);
            }
            foreach (ModuleButtonEntity item in moduleButtonData)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = moduleButtonData.Count(t => t.F_ParentId == item.F_ModuleButtonId) == 0 ? false : true;
                tree.id = item.F_ModuleButtonId;
                tree.text = item.F_FullName;
                tree.value = item.F_ModuleButtonId;
                if (item.F_ParentId == "0")
                {
                    tree.parentId = item.F_ModuleId;
                }
                else
                {
                    tree.parentId = item.F_ParentId;
                }
                tree.checkstate = existModuleButton.Count(t => t.F_ItemId == item.F_ModuleButtonId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-wrench " + item.F_ModuleId;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 系统视图列表
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleColumnTreeJson(string userGroupId)
        {
            var existModuleColumn = permissionBLL.GetModuleColumnList(userGroupId);
            var moduleData = moduleBLL.GetList();
            var moduleColumnData = moduleColumnBLL.GetList();
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in moduleData)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.F_ModuleId;
                tree.text = item.F_FullName;
                tree.value = item.F_ModuleId;
                tree.checkstate = existModuleColumn.Count(t => t.F_ItemId == item.F_ModuleId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.parentId = item.F_ParentId;
                tree.img = item.F_Icon;
                treeList.Add(tree);
            }
            foreach (ModuleColumnEntity item in moduleColumnData)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = moduleColumnData.Count(t => t.F_ParentId == item.F_ModuleColumnId) == 0 ? false : true;
                tree.id = item.F_ModuleColumnId;
                tree.text = item.F_FullName;
                tree.value = item.F_ModuleColumnId;
                if (item.F_ParentId == "0")
                {
                    tree.parentId = item.F_ModuleId;
                }
                else
                {
                    tree.parentId = item.F_ParentId;
                }
                tree.checkstate = existModuleColumn.Count(t => t.F_ItemId == item.F_ModuleColumnId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-filter " + item.F_ModuleId;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 数据权限列表
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrganizeTreeJson(string userGroupId)
        {
            var existAuthorizeData = permissionBLL.GetAuthorizeDataList(userGroupId);
            var organizedata = organizeBLL.GetList();
            var departmentdata = departmentCache.GetList();
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = organizedata.Count(t => t.F_ParentId == item.F_OrganizeId) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.F_OrganizeId == item.F_OrganizeId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                tree.id = item.F_OrganizeId;
                tree.text = item.F_FullName;
                tree.value = item.F_OrganizeId;
                tree.parentId = item.F_ParentId;
                if (item.F_ParentId == "0")
                {
                    tree.img = "fa fa-sitemap";
                }
                else
                {
                    tree.img = "fa fa-home";
                }
                tree.checkstate = existAuthorizeData.Count(t => t.F_ResourceId == item.F_OrganizeId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = departmentdata.Count(t => t.F_ParentId == item.F_DepartmentId) == 0 ? false : true;
                tree.id = item.F_DepartmentId;
                tree.text = item.F_FullName;
                tree.value = item.F_DepartmentId;
                if (item.F_ParentId == "0")
                {
                    tree.parentId = item.F_OrganizeId;
                }
                else
                {
                    tree.parentId = item.F_ParentId;
                }
                tree.checkstate = existAuthorizeData.Count(t => t.F_ResourceId == item.F_DepartmentId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-umbrella";
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            int authorizeType = -1;
            if (existAuthorizeData.ToList().Count > 0)
            {
                authorizeType = existAuthorizeData.ToList()[0].F_AuthorizeType.ToInt();
            }
            var JsonData = new
            {
                authorizeType = authorizeType,
                authorizeData = existAuthorizeData,
                treeJson = treeList.TreeToJson(),
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存用户组成员
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveMember(string userGroupId, string userIds)
        {
            permissionBLL.SaveMember(AuthorizeTypeEnum.UserGroup, userGroupId, userIds);
            return Success("保存成功。");
        }
        /// <summary>
        /// 保存用户组授权
        /// </summary>
        /// <param name="userGroupId">用户组Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataJson">数据权限</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveAuthorize(string userGroupId, string moduleIds, string moduleButtonIds, string moduleColumnIds, string authorizeDataJson)
        {
            permissionBLL.SaveAuthorize(AuthorizeTypeEnum.UserGroup, userGroupId, moduleIds, moduleButtonIds, moduleColumnIds, authorizeDataJson);
            return Success("保存成功。");
        }
        #endregion
    }
}
