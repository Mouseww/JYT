using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using LeaRun.Util.Extension;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-04-28 16:48
    /// 描 述：现金余额
    /// </summary>
    public class CashBalanceService : RepositoryFactory<CashBalanceEntity>, ICashBalanceService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CashBalanceEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<CashBalanceEntity>();
            var queryParam = queryJson.ToJObject();
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.F_ExecutionDate >= startTime && t.F_ExecutionDate <= endTime);
            }
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.F_CreateDate).ToList();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加收支余额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cashBalanceEntity"></param>
        public void AddBalance(IRepository db, CashBalanceEntity cashBalanceEntity)
        {
            decimal balance = 0;
            var data = db.IQueryable<CashBalanceEntity>(t => t.F_CashAccount == cashBalanceEntity.F_CashAccount).OrderByDescending(t => t.F_CreateDate);
            if (data.Count() > 0)
            {
                balance = data.First().F_Balance.ToDecimal();
            }
            if (cashBalanceEntity.F_Receivable != null)
            {
                cashBalanceEntity.F_Balance = cashBalanceEntity.F_Receivable + balance;
            }
            if (cashBalanceEntity.F_Expenses != null)
            {
                cashBalanceEntity.F_Balance = balance - cashBalanceEntity.F_Expenses;
            }
            cashBalanceEntity.Create();
            db.Insert(cashBalanceEntity);
        }
        #endregion
    }
}
