using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.WeChatManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.WeChatManage;
using LeaRun.Util;
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
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号成员
    /// </summary>
    public class UserController : MvcControllerBase
    {
        private UserBLL userBLL = new UserBLL();
        private WeChatUserBLL weChatUserBLL = new WeChatUserBLL();

        #region 视图功能
        /// <summary>
        /// 成员管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 成员表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult MemberForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 成员列表 
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson()
        {
            var data = weChatUserBLL.GetList();
            var userlist = userBLL.GetList().ToList();
            var TreeList = new List<TreeGridEntity>();
            foreach (WeChatUserRelationEntity item in data)
            {
                UserEntity userEntity = userlist.Find(t => t.F_Account == item.F_UserId);
                if (userEntity != null)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    tree.id = item.F_UserId;
                    tree.hasChildren = false;
                    tree.parentId = "0";
                    tree.expanded = true;
                    if (userEntity.F_ModifyDate > item.F_CreateDate)
                    {
                        item.F_SyncState = "-1";
                    }
                    string entityJson = item.ToJson();
                    entityJson = entityJson.Insert(1, "\"F_Id\":\"" + userEntity.F_UserId + "\",");
                    entityJson = entityJson.Insert(1, "\"F_RealName\":\"" + userEntity.F_RealName + "\",");
                    entityJson = entityJson.Insert(1, "\"F_Gender\":\"" + userEntity.F_Gender + "\",");
                    entityJson = entityJson.Insert(1, "\"F_Mobile\":\"" + userEntity.F_Mobile + "\",");
                    entityJson = entityJson.Insert(1, "\"F_Email\":\"" + userEntity.F_Email + "\",");
                    entityJson = entityJson.Insert(1, "\"F_WeChat\":\"" + userEntity.F_WeChat + "\",");
                    entityJson = entityJson.Insert(1, "\"F_PostName\":\"" + userEntity.F_PostName + "\",");
                    tree.entityJson = entityJson;
                    TreeList.Add(tree);
                }
            }
            return Content(TreeList.TreeJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存成员
        /// </summary>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveMember(string userIds)
        {
            string msg = "";
            weChatUserBLL.SaveMember(userIds.Split(','), out msg);
            return Success(msg);
        }
        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="keyValue">成员Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult DeleteMember(string keyValue)
        {
            weChatUserBLL.DeleteMember(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 一键同步
        /// </summary>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Synchronization(string userIds)
        {
            weChatUserBLL.Synchronization(userIds.Split(','));
            return Success("同步成功。");
        }
        #endregion
    }
}
