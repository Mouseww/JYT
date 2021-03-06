using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
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
    /// 日 期：2016-04-20 11:23
    /// 描 述：费用支出
    /// </summary>
    public class ExpensesService : RepositoryFactory<ExpensesEntity>, IExpensesService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ExpensesEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ExpensesEntity>();
            var queryParam = queryJson.ToJObject();
            //支出日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.F_ExpensesDate >= startTime && t.F_ExpensesDate <= endTime);
            }
            //支出种类
            if (!queryParam["ExpensesType"].IsEmpty())
            {
                string CustomerName = queryParam["ExpensesType"].ToString();
                expression = expression.And(t => t.F_ExpensesType.Equals(CustomerName));
            }
            //经手人
            if (!queryParam["Managers"].IsEmpty())
            {
                string SellerName = queryParam["Managers"].ToString();
                expression = expression.And(t => t.F_Managers.Contains(SellerName));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ExpensesEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ExpensesEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(ExpensesEntity entity)
        {
            ICashBalanceService icashbalanceservice = new CashBalanceService();

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //支出
                entity.Create();
                db.Insert(entity);


                //添加账户余额
                icashbalanceservice.AddBalance(db, new CashBalanceEntity
                {
                    F_ObjectId = entity.F_ExpensesId,
                    F_ExecutionDate = entity.F_ExpensesDate,
                    F_CashAccount = entity.F_ExpensesAccount,
                    F_Expenses = entity.F_ExpensesPrice,
                    F_Abstract = entity.F_ExpensesAbstract
                });

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
