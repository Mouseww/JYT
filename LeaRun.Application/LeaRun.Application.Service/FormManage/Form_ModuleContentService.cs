using LeaRun.Application.Entity.FormManage;
using LeaRun.Application.IService.FormManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.FormManage
{
    /// <summary>
    /// 版 本 Learun ADMS6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-12-14 05:56
    /// 描 述：Form_ModuleContent
    /// </summary>
    public class Form_ModuleContentService : RepositoryFactory<Form_ModuleContentEntity>, Form_ModuleContentIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Form_ModuleContentEntity> GetPageList(Pagination pagination, string queryJson)
        {
             return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Form_ModuleContentEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Form_ModuleContentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 根据表单id和版本获取表单内容数据
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public Form_ModuleContentEntity GetEntity(string formId, string version)
        {
            var expression = LinqExtensions.True<Form_ModuleContentEntity>();
            expression = expression.And(t => t.F_FrmId == formId && t.F_FrmVersion == version);
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string SaveEntity(string keyValue, Form_ModuleContentEntity entity)
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
            return entity.F_Id;
        }
        #endregion
    }
}
