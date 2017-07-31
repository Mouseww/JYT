using LeaRun.Application.Entity.AppManage;
using LeaRun.Application.IService.AppManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-07-18 11:52
    /// 描 述：App_PageTemplates
    /// </summary>
    public class AppTemplatesService : RepositoryFactory<AppTemplatesEntity>, AppTemplatesIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<AppTemplatesEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AppTemplatesEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取实体列表根据项目Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IEnumerable<AppTemplatesEntity> GetEntityByProjectId(string projectId)
        {
            try
            {
                var expression = LinqExtensions.True<AppTemplatesEntity>();
                expression = expression.And(t => t.F_ProjectId == projectId);
                return this.BaseRepository().IQueryable(expression).OrderBy(t=> t.F_level).ToList();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AppTemplatesEntity entity)
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
        #endregion
    }
}