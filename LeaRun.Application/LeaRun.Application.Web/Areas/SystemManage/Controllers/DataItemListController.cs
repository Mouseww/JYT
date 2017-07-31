using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Entity.SystemManage.ViewModel;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.23 16:40
    /// 描 述：辅助资料（主要是数据字典提供独立功能管理）
    /// </summary>
    public class DataItemListController : MvcControllerBase
    {
        private DataItemBLL dataItemBLL = new DataItemBLL();
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        private DataItemCache dataItemCache = new DataItemCache();

        #region 视图功能
        /// <summary>
        /// 辅助资料管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            string ItemCode = Request["ItemCode"];
            var data = dataItemBLL.GetEntityByCode(ItemCode);
            ViewBag.itemId = data.F_ItemId;
            ViewBag.isTree = data.F_IsTree;
            return View();
        }
        /// <summary>
        /// 辅助资料表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 辅助列表
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <param name="condition">条件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string EnCode, string condition, string keyword)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            if (!string.IsNullOrEmpty(keyword))
            {
                #region 多条件查询
                switch (condition)
                {
                    case "ItemName":        //项目名
                        data = data.Where(t => t.F_ItemName.Contains(keyword));
                        break;
                    case "ItemValue":      //项目值
                        data = data.Where(t => t.F_ItemValue.Contains(keyword));
                        break;
                    case "SimpleSpelling": //拼音
                        data = data.Where(t => t.F_SimpleSpelling.Contains(keyword));
                        break;
                    default:
                        break;
                }
                #endregion
            }

            var TreeList = new List<TreeGridEntity>();
            foreach (DataItemModel item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_ItemDetailId) == 0 ? false : true;
                tree.id = item.F_ItemDetailId;
                tree.parentId = item.F_ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }
        /// <summary>
        /// 辅助实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dataItemDetailBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除辅助
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            dataItemDetailBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存辅助表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">辅助实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity)
        {
            dataItemDetailBLL.SaveForm(keyValue, dataItemDetailEntity);
            return Success("操作成功。");
        }
        #endregion
    }
}
