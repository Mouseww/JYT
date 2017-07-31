using LeaRun.Application.Entity.DemoManage;
using LeaRun.Application.IService.DemoManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.DemoManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-10-17 09:47
    /// 描 述：订单管理
    /// </summary>
    public class Client_OrderService : RepositoryFactory, Client_OrderIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Client_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
             return this.BaseRepository().FindList<Client_OrderEntity>(pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Client_OrderEntity GetEntity(string keyValue)
        {
             return this.BaseRepository().FindEntity<Client_OrderEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Client_OrderEntryEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Client_OrderEntryEntity>("select * from Client_OrderEntry where F_OrderId='"+keyValue +"'");        }
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
                 db.Delete<Client_OrderEntity>(keyValue);
                 db.Delete<Client_OrderEntryEntity>(t => t.F_OrderId.Equals(keyValue));
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
        /// <param name="entryList">从表实体对象列表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Client_OrderEntity entity,List<Client_OrderEntryEntity> entryList)
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
                db.Delete<Client_OrderEntryEntity>(t => t.F_OrderId.Equals(keyValue));
                foreach (Client_OrderEntryEntity item in entryList)
                {
                    item.Create();
                    item.F_OrderId = entity.F_OrderId;
                    db.Insert(item);
                }
            }
            else
            {
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
                foreach (Client_OrderEntryEntity item in entryList)
                {
                    item.Create();
                    item.F_OrderId = entity.F_OrderId;
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
