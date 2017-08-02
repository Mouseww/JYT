using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Code;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Util;
using System.Linq;
using System.Text;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using System;



namespace LeaRun.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：孔毛毛
    /// 日 期：2017-08-02 09:41
    /// 描 述：materialRoot
    /// </summary>
    public class materialRootController : MvcControllerBase
    {
        private materialRootBLL materialrootbll = new materialRootBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult materialRootIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult materialRootForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        //获取目录级功能表
        /// <summary>
        /// 功能列表其他 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="target">目标</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson_other(string parentId)
        {
            var data = materialrootbll.GetList("").ToList();
            data = data.FindAll(a => a.parentId.ToString() == parentId);
            var temp = data;
            //data = data.FindAll(a => a.parentIds == "2");
            var treeList = new List<TreeEntity>();
            foreach (materialRootEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = temp.Count(t => t.parentId.ToString() == item.id.ToString()) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.name;
                tree.value = item.id.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.parentId.ToString();
                // tree.Attribute = "F_Target";
                //tree.AttributeValue = item.F_Target;

                treeList.Add(tree);

            }
            var tree1 = treeList.ToJson();
            return Content(tree1);
        }
        /// <summary>
        /// 区域列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string value)
        {
            var data = materialrootbll.GetList("").ToList();


            //data = data.FindAll(a => a.parentIds == "1" || a.parentIds == null);
            var temp = data;
           

            if (!string.IsNullOrEmpty(value))
            {
                data = data.TreeWhere(t => t.id.ToString().Contains(value), "F_ModuleId");
            }
            var treeList = new List<TreeEntity>();
            foreach (materialRootEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.parentId.ToString() == item.id.ToString()) == 0 ? false : true;
                tree.id = item.id.ToString();
                tree.text = item.name;
                tree.value = item.id.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.parentId.ToString();
                // tree.Attribute = "F_Target";
                //tree.AttributeValue = item.F_Target;
                treeList.Add(tree);

            }
            //表数据内循环异常
            for (int i =1; i < 20; i++)
            {
                treeList.Remove(treeList[i]);
            }
            var tree2 = treeList.TreeToJson();
            return Content(tree2);
        }

        //    string parentId = value == null ? "0" : value;
        //    var filterdata = materialrootbll.GetList(parentId).ToList();
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("[");
        //    if (filterdata.Count > 0)
        //    {
        //        foreach (materialRootEntity item in filterdata)
        //        {
        //            bool hasChildren = materialrootbll.GetList(item.id.ToString()).ToList().Count == 0 ? false : true;
        //            sb.Append("{");
        //            sb.Append("\"id\":\"" + item.parentIds + "\",");
        //            sb.Append("\"text\":\"" + item.name + "\",");
        //            sb.Append("\"value\":\"" + item.parentIds + "\",");
        //            sb.Append("\"isexpand\":false,");
        //            sb.Append("\"complete\":false,");
        //            sb.Append("\"hasChildren\":" + hasChildren.ToString().ToLower() + "");
        //            sb.Append("},");
        //        }
        //        sb = sb.Remove(sb.Length - 1, 1);
        //    }
        //    sb.Append("]");
        //    return Content(sb.ToString());
        //}
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = materialrootbll.GetPageList(pagination, queryJson).ToList();
           var data2 = materialrootbll.GetList("");
            try {
                var queryParam = queryJson.ToJObject();
               
                if (queryParam["parentId"]!=null)
            {
                    
                    var pname = queryParam["parentId"].ToString();
                   
                    data = data2.Where(a=>a.parentId.ToString()== pname).ToList();

            }
              
            }
            catch
            {

            }
            for (int i = 0; i < data.Count(); i++)
            {
                try
                {
                    data[i].parentNames = data2.First(a => a.id == data[i].parentId).name;
                }
                catch { } }
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = materialrootbll.GetList(queryJson);
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
            var data = materialrootbll.GetEntity(keyValue);
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
            materialrootbll.RemoveForm(keyValue);
          
            return Success("1");
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
        public ActionResult SaveForm(string keyValue, materialRootEntity entity)
        {
            materialrootbll.SaveForm(keyValue, entity);
         
            return Success("2");
        }
        #endregion
    }
}
