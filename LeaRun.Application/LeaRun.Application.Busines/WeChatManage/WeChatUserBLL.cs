using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.WeChatManage;
using LeaRun.Application.IService.WeChatManage;
using LeaRun.Application.Service.WeChatManage;
using LeaRun.Util.Extension;
using LeaRun.Util.WeChat.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Busines.WeChatManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号成员
    /// </summary>
    public class WeChatUserBLL
    {
        private IWeChatUserService service = new WeChatUserService();
        private WeChatOrganizeBLL weChatOrganizeBLL = new WeChatOrganizeBLL();
        private UserBLL userBLL = new UserBLL();

        #region 获取数据
        /// <summary>
        /// 成员列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeChatUserRelationEntity> GetList()
        {
            return service.GetList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存成员（并自动添加企业号成员）
        /// </summary>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        public void SaveMember(string[] userIds, out string msg)
        {
            List<UserEntity> usreList = userBLL.GetList().ToList();
            List<WeChatDeptRelationEntity> departmentList = weChatOrganizeBLL.GetList().ToList();
            int succeed = 0;
            int error = 0;
            foreach (var userId in userIds)
            {
                try
                {
                    UserEntity userEntity = usreList.Find(t => t.F_UserId == userId);
                    WeChatDeptRelationEntity weChatDeptRelationEntity = departmentList.Find(t => t.F_DeptId == userEntity.F_DepartmentId);


                    UserCreate userCreate = new UserCreate();
                    userCreate.userid = userEntity.F_Account;
                    userCreate.name = userEntity.F_RealName;
                    userCreate.position = userEntity.F_PostName;
                    userCreate.mobile = userEntity.F_Mobile;
                    userCreate.gender = userEntity.F_Gender == 1 ? "1" : "2";
                    userCreate.email = userEntity.F_Email;
                    userCreate.weixinid = userEntity.F_WeChat;
                    string departmentId = weChatDeptRelationEntity.F_WeChatDeptId.ToString();
                    userCreate.department = new List<string>() { departmentId };
                    var result = userCreate.Send();
                    if (result.errcode == 0)
                    {
                        UserInvite userInvite = new UserInvite();
                        userInvite.userid = userCreate.userid;
                        result = userInvite.Send();
                    }

                    WeChatUserRelationEntity weChatUserRelationEntity = new WeChatUserRelationEntity();
                    weChatUserRelationEntity.F_UserRelationId = userCreate.userid;
                    weChatUserRelationEntity.F_UserId = userCreate.userid;
                    weChatUserRelationEntity.F_DeptId = weChatDeptRelationEntity.F_DeptId;
                    weChatUserRelationEntity.F_DeptName = weChatDeptRelationEntity.F_DeptName;
                    weChatUserRelationEntity.F_WeChatDeptId = departmentId.ToInt();
                    weChatUserRelationEntity.F_SyncState = result.errcode.ToString();
                    weChatUserRelationEntity.F_SyncLog = result.errmsg;
                    service.SaveForm("", weChatUserRelationEntity);

                    succeed++;
                }
                catch (System.Exception)
                {
                    error++;
                }
            }
            msg = "成功：" + succeed + " ;错误：" + error;
        }
        /// <summary>
        /// 删除成员（并自动删除企业号成员）
        /// </summary>
        /// <param name="userId">成员Id</param>
        /// <returns></returns>
        public void DeleteMember(string userId)
        {
            try
            {
                UserDelete userDelete = new UserDelete();
                userDelete.userid = userId;
                var result = userDelete.Send();
                if (result.errcode == 0)
                {
                    service.RemoveForm(userId);
                }
                else
                {
                    WeChatUserRelationEntity weChatUserRelationEntity = new WeChatUserRelationEntity();
                    weChatUserRelationEntity.F_SyncState = result.errcode.ToString();
                    weChatUserRelationEntity.F_SyncLog = result.errmsg;
                    service.SaveForm(userId, weChatUserRelationEntity);
                    throw new Exception(result.errmsg);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 同步成员
        /// </summary>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        public void Synchronization(string[] userIds)
        {
            List<UserEntity> usreList = userBLL.GetList().ToList();
            List<WeChatDeptRelationEntity> departmentList = weChatOrganizeBLL.GetList().ToList();
            foreach (var userId in userIds)
            {
                UserEntity userEntity = usreList.Find(t => t.F_UserId == userId);
                WeChatDeptRelationEntity weChatDeptRelationEntity = departmentList.Find(t => t.F_DeptId == userEntity.F_DepartmentId);

                #region 同步更新信息
                UserUpdate userUpdate = new UserUpdate();
                userUpdate.userid = userEntity.F_Account;
                userUpdate.name = userEntity.F_RealName;
                userUpdate.position = userEntity.F_PostName;
                userUpdate.mobile = userEntity.F_Mobile;
                userUpdate.gender = userEntity.F_Gender == 1 ? "1" : "2";
                userUpdate.email = userEntity.F_Email;
                userUpdate.weixinid = userEntity.F_WeChat;
                string departmentId = weChatDeptRelationEntity.F_WeChatDeptId.ToString();
                userUpdate.department = new List<string>() { departmentId };
                userUpdate.enable = userEntity.F_EnabledMark.ToInt();
                var result = userUpdate.Send();

                #endregion

                #region 同步邀请关注
                UserGet userGet = new UserGet();
                userGet.userid = userEntity.F_Account;
                var status = userGet.Send();
                #endregion

                WeChatUserRelationEntity weChatUserRelationEntity = new WeChatUserRelationEntity();
                weChatUserRelationEntity.F_UserId = userUpdate.userid;
                weChatUserRelationEntity.F_DeptId = weChatDeptRelationEntity.F_DeptId;
                weChatUserRelationEntity.F_DeptName = weChatDeptRelationEntity.F_DeptName;
                weChatUserRelationEntity.F_WeChatDeptId = departmentId.ToInt();
                weChatUserRelationEntity.F_SyncState = status.status.ToString();
                weChatUserRelationEntity.F_SyncLog = status.errmsg;
                service.SaveForm(userEntity.F_Account, weChatUserRelationEntity);
            }
        }
        #endregion
    }
}
