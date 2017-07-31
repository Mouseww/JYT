using LeaRun.Application.Entity.AppManage;
using LeaRun.Application.IService.AppManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-07-19 10:13
    /// 描 述：App_Project
    /// </summary>
    public class AppProjectService : RepositoryFactory, AppProjectIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<AppProjectEntity> GetList(string queryJson)
        {
            return this.BaseRepository().FindList<AppProjectEntity>("SELECT * FROM App_Project");
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AppProjectEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<AppProjectEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<AppProjectEntity>(keyValue);

                var expression = LinqExtensions.True<AppTemplatesEntity>();
                expression = expression.And(t => t.F_ProjectId == keyValue);
                db.Delete(expression);

                db.Commit();
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AppProjectEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 保存App项目
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="templatesEntitys"></param>
        public void SaveProject(string keyValue, AppProjectEntity entity,List<AppTemplatesEntity> templatesEntitys)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                }
                var expression = LinqExtensions.True<AppTemplatesEntity>();
                expression = expression.And(t => t.F_ProjectId == entity.F_Id);
                db.Delete(expression);

                foreach (var item in templatesEntitys)
                {
                    item.Create();
                    item.F_ProjectId = entity.F_Id;
                    db.Insert(item);
                }
                db.Commit();
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}