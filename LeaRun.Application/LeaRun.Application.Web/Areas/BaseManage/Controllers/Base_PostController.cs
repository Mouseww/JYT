using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Linq;

namespace LeaRun.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 11:24
    /// 描 述：Base_Post
    /// </summary>
    public class Base_PostController : MvcControllerBase
    {
        private Base_PostBLL base_postbll = new Base_PostBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Base_PostIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Base_PostForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="target">目标</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword, string target)
        {
            var data = moduleBLL.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.F_FullName.Contains(keyword), "F_ModuleId");
            }
            if (!string.IsNullOrEmpty(target))
            {
                data = data.TreeWhere(t => t.F_Target.Contains(target), "F_ModuleId");
            }
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_ModuleId) == 0 ? false : true;
                tree.id = item.F_ModuleId;
                tree.text = item.F_FullName;
                tree.value = item.F_ModuleId;
                tree.isexpand = item.F_AllowExpand == 0 ? false : true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.F_ParentId;
                tree.img = item.F_Icon;
                tree.Attribute = "F_Target";
                tree.AttributeValue = item.F_Target;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = base_postbll.GetList(queryJson);
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
            var data = base_postbll.GetEntity(keyValue);
            return ToJsonResult(data);
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
            base_postbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Base_PostEntity entity)
        {
            base_postbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
