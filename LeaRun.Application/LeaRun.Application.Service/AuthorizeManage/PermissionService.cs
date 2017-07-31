using LeaRun.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.5 22:35
    /// 描 述：权限配置管理（角色、岗位、职位、用户组、用户）
    /// </summary>
    public class PermissionService : RepositoryFactory, IPermissionService
    {
        #region 获取数据
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetMemberList(string objectId)
        {
            return this.BaseRepository().IQueryable<UserRelationEntity>(t => t.F_ObjectId == objectId).OrderByDescending(t => t.F_CreateDate).ToList();
        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetObjectList(string userId)
        {
            return this.BaseRepository().IQueryable<UserRelationEntity>(t => t.F_UserId == userId).OrderByDescending(t => t.F_CreateDate).ToList();
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeEntity>(t => t.F_ObjectId == objectId && t.F_ItemType == 1).ToList();
        }
        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleButtonList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeEntity>(t => t.F_ObjectId == objectId && t.F_ItemType == 2 ).ToList();
        }
        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleColumnList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeEntity>(t => t.F_ObjectId == objectId && t.F_ItemType == 3).ToList();
        }
        /// <summary>
        /// 获取数据权限列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeDataEntity> GetAuthorizeDataList(string objectId)
        {
            return this.BaseRepository().IQueryable<AuthorizeDataEntity>(t => t.F_ObjectId == objectId).OrderBy(t => t.F_SortCode).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="userIds">成员Id</param>
        public void SaveMember(AuthorizeTypeEnum authorizeType, string objectId, string[] userIds)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<UserRelationEntity>(t => t.F_ObjectId == objectId && t.F_IsDefault == 0);
                int SortCode = 1;
                foreach (string item in userIds)
                {
                    UserRelationEntity userRelationEntity = new UserRelationEntity();
                    userRelationEntity.Create();
                    userRelationEntity.F_Category = (int)authorizeType;
                    userRelationEntity.F_ObjectId = objectId;
                    userRelationEntity.F_UserId = item;
                    userRelationEntity.F_SortCode = SortCode++;
                    db.Insert(userRelationEntity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataList">数据权限</param>
        public void SaveAuthorize(AuthorizeTypeEnum authorizeType, string objectId, string[] moduleIds, string[] moduleButtonIds, string[] moduleColumnIds, IEnumerable<AuthorizeDataEntity> authorizeDataList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<AuthorizeEntity>(t => t.F_ObjectId == objectId);

                #region 功能
                int SortCode = 1;
                foreach (string item in moduleIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_Category = (int)authorizeType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 1;
                    authorizeEntity.F_ItemId = item;
                    authorizeEntity.F_SortCode = SortCode++;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 按钮
                SortCode = 1;
                foreach (string item in moduleButtonIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_Category = (int)authorizeType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 2;
                    authorizeEntity.F_ItemId = item;
                    authorizeEntity.F_SortCode = SortCode++;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 视图
                SortCode = 1;
                foreach (string item in moduleColumnIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_Category = (int)authorizeType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 3;
                    authorizeEntity.F_ItemId = item;
                    authorizeEntity.F_SortCode = SortCode++;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 数据权限
                SortCode = 1;
                db.Delete<AuthorizeDataEntity>(objectId, "F_ObjectId");
                int index = 0;
                foreach (AuthorizeDataEntity authorizeDataEntity in authorizeDataList)
                {
                    authorizeDataEntity.Create();
                    authorizeDataEntity.F_Category = (int)authorizeType;
                    authorizeDataEntity.F_ObjectId = objectId;
                    // authorizeDataEntity.Module = "Department";
                    authorizeDataEntity.F_SortCode = SortCode++;
                    db.Insert(authorizeDataEntity);
                    index++;
                }
                #endregion

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
