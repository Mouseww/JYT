using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.WeChatManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.23 10:14
    /// 描 述：企业号部门
    /// </summary>
    public class OrganizeController : MvcControllerBase
    {
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        private DepartmentBLL departmentBLL = new DepartmentBLL();
        private WeChatOrganizeBLL weChatOrganizeBLL = new WeChatOrganizeBLL();

        #region 视图功能
        /// <summary>
        /// 机构管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string condition, string keyword)
        {
            var organizedata = organizeBLL.GetList();
            var departmentdata = departmentBLL.GetList();
            var wechatdeptdata = weChatOrganizeBLL.GetList();
            var treeList = new List<TreeGridEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                bool hasChildren = organizedata.Count(t => t.F_ParentId == item.F_OrganizeId) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.F_OrganizeId == item.F_OrganizeId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                TreeGridEntity tree = new TreeGridEntity();
                tree.id = item.F_OrganizeId;
                tree.text = item.F_FullName;
                tree.hasChildren = hasChildren;
                tree.parentId = item.F_ParentId;
                tree.expanded = true;
                string entityJson = item.ToJson();
                var exists = wechatdeptdata.ToList().Find(t => t.F_DeptId == item.F_OrganizeId);
                if (exists != null)
                {
                    if (item.F_ModifyDate > exists.F_CreateDate)
                    {
                        entityJson = entityJson.Insert(1, "\"F_IsSync\":0,");
                    }
                    else
                    {
                        entityJson = entityJson.Insert(1, "\"F_IsSync\":1,");
                    }
                    entityJson = entityJson.Insert(1, "\"F_SyncTime\":\"" + exists.F_CreateDate.ToDate().ToString("yyyy-MM-dd HH:mm:ss") + "\",");
                }
                tree.entityJson = entityJson;
                treeList.Add(tree);
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                bool hasChildren = departmentdata.Count(t => t.F_ParentId == item.F_DepartmentId) == 0 ? false : true;
                TreeGridEntity tree = new TreeGridEntity();
                tree.id = item.F_DepartmentId;
                tree.text = item.F_FullName;
                tree.hasChildren = hasChildren;
                if (item.F_ParentId == "0")
                {
                    tree.parentId = item.F_OrganizeId;
                    item.F_ParentId = item.F_OrganizeId;
                }
                else
                {
                    tree.parentId = item.F_ParentId;
                }
                tree.expanded = true;
                item.F_OrganizeId = item.F_DepartmentId;
                string entityJson = item.ToJson();
                var exists = wechatdeptdata.ToList().Find(t => t.F_DeptId == item.F_OrganizeId);
                if (exists != null)
                {
                    if (item.F_ModifyDate > exists.F_CreateDate)
                    {
                        entityJson = entityJson.Insert(1, "\"IsSync\":0,");
                    }
                    else
                    {
                        entityJson = entityJson.Insert(1, "\"IsSync\":1,");
                    }
                    entityJson = entityJson.Insert(1, "\"SyncTime\":\"" + exists.F_CreateDate.ToDate().ToString("yyyy-MM-dd HH:mm:ss") + "\",");
                }
                tree.entityJson = entityJson;
                treeList.Add(tree);
            }
            if (!string.IsNullOrEmpty(condition) && !string.IsNullOrEmpty(keyword))
            {
                #region 多条件查询
                switch (condition)
                {
                    case "EnCode":      //部门编号
                        treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
                        break;
                    case "FullName":    //部门名称
                        treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            return Content(treeList.TreeJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 一键同步
        /// </summary>
        /// <param name="organizeListJson">机构列表Json</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Synchronization(string organizeListJson)
        {
            weChatOrganizeBLL.Synchronization(organizeListJson);
            return Success("同步成功。");
        }
        #endregion
    }
}
