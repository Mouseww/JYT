using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 编 辑：LR0101
    /// 日 期：2016-09-07 09:39
    /// 描 述：Base_DataSource
    /// </summary>
    public class DataSourceController : MvcControllerBase
    {
        private DataSourceBLL datasourcebll = new DataSourceBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 预览页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = datasourcebll.GetList(queryJson);
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
            var data = datasourcebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 源数据管理列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = datasourcebll.GetPageList(pagination, queryJson);
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
            datasourcebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, DataSourceEntity entity)
        {
            datasourcebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion

        #region 接口数据处理
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <param name="strEntity"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult TestData(string strEntity, string queryJson)
        {
            DataSourceEntity entyity = strEntity.ToObject<DataSourceEntity>();
            var data = datasourcebll.GetData(entyity, queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取数据表的数据
        /// </summary>
        /// <param name="dbLinkId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTableData(string dbLinkId, string tableName)
        {
            var data = datasourcebll.GetTableData(dbLinkId, tableName);
            return ToJsonResult(data);
        }

        #endregion
    }
}
