using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using LeaRun.Util;
using System.Collections.Generic;
using System.Linq;
using System;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderService : RepositoryFactory<OrderEntity>, IOrderService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.F_OrderDate >= startTime && t.F_OrderDate <= endTime);
            }
            //单据编号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                expression = expression.And(t => t.F_OrderCode.Contains(OrderCode));
            }
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                expression = expression.And(t => t.F_CustomerName.Contains(CustomerName));
            }
            //销售人员
            if (!queryParam["SellerName"].IsEmpty())
            {
                string SellerName = queryParam["SellerName"].ToString();
                expression = expression.And(t => t.F_SellerName.Contains(SellerName));
            }
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                expression = expression.And(t => t.F_PaymentState == PaymentState);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            OrderEntity entity = this.GetEntity(keyValue);
            if (type == 1)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.F_CreateDate > entity.F_CreateDate).OrderBy(t => t.F_CreateDate).FirstOrDefault();
            }
            else if (type == 2)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.F_CreateDate < entity.F_CreateDate).OrderByDescending(t => t.F_CreateDate).FirstOrDefault();
            }
            return entity;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<OrderEntity>(keyValue);
                db.Delete<OrderEntryEntity>(t => t.F_OrderId.Equals(keyValue));
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
        /// <param name="orderEntity">实体对象</param>
        /// <param name="orderEntryList">明细实体对象</param>
        /// <returns>主键</returns>
        public string SaveForm(string keyValue, OrderEntity orderEntity, List<OrderEntryEntity> orderEntryList)
        {
            string pKey = "";
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    orderEntity.Modify(keyValue);
                    db.Update(orderEntity);
                    //明细
                    db.Delete<OrderEntryEntity>(t => t.F_OrderId.Equals(keyValue));
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.F_OrderId = orderEntity.F_OrderId;
                        db.Insert(orderEntryEntity);
                    }
                    pKey = keyValue;
                }
                else
                {
                    //主表
                    orderEntity.Create();
                    db.Insert(orderEntity);
                    coderuleService.UseRuleSeed(orderEntity.F_CreateUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString(), db);//占用单据号
                    //明细
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.Create();
                        orderEntryEntity.F_OrderId = orderEntity.F_OrderId;
                        db.Insert(orderEntryEntity);
                    }
                    pKey = orderEntity.F_OrderId;
                }
                db.Commit();
                return pKey;
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