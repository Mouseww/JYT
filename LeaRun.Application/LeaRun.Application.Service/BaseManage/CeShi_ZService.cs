using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 16:44
    /// 描 述：主表
    /// </summary>
    public class CeShi_ZService : RepositoryFactory, CeShi_ZIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CeShi_ZEntity> GetPageList(Pagination pagination, string queryJson)
        {
             return this.BaseRepository("","").FindList<CeShi_ZEntity>(pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CeShi_ZEntity GetEntity(string keyValue)
        {
             return this.BaseRepository().FindEntity<CeShi_ZEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<CeShi_FEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<CeShi_FEntity>("select * from CeShi_F where Z_Id='"+keyValue +"'");        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
             IRepository db = this.BaseRepository().BeginTrans();
             try
             {
                 db.Delete<CeShi_ZEntity>(keyValue);
                 db.Delete<CeShi_FEntity>(t => t.Z_Id.Equals(keyValue));
                 db.Commit();
             }
             catch (Exception)
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
        public void SaveForm(string keyValue, CeShi_ZEntity entity,List<CeShi_FEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //主表
                entity.Modify(keyValue);
                db.Update(entity);
                //明细
                db.Delete<CeShi_FEntity>(t => t.Z_Id.Equals(keyValue));
                foreach (CeShi_FEntity item in entryList)
                {
                    item.Create();
                    item.Z_Id = entity.Z_Id;
                    db.Insert(item);
                }
            }
            else
            {
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
                foreach (CeShi_FEntity item in entryList)
                {
                    item.Create();
                    item.Z_Id = entity.Z_Id;
                    db.Insert(item);
                }
            }
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
