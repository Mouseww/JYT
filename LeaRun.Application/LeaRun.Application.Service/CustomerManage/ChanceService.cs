using LeaRun.Application.Code;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
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
    /// 日 期：2016-03-12 10:50
    /// 描 述：商机信息
    /// </summary>
    public class ChanceService : RepositoryFactory<ChanceEntity>, IChanceService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();
        private ITrailRecordService trailRecordService = new TrailRecordService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ChanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<ChanceEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //商机编号
                        expression = expression.And(t => t.F_EnCode.Contains(keyword));
                        break;
                    case "FullName":            //商机名称
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        break;
                    case "Contacts":            //联系人
                        expression = expression.And(t => t.F_Contacts.Contains(keyword));
                        break;
                    case "Mobile":              //手机
                        expression = expression.And(t => t.F_Mobile.Contains(keyword));
                        break;
                    case "Tel":                 //电话
                        expression = expression.And(t => t.F_Tel.Contains(keyword));
                        break;
                    case "QQ":                  //QQ
                        expression = expression.And(t => t.F_QQ.Contains(keyword));
                        break;
                    case "Wechat":              //微信
                        expression = expression.And(t => t.F_Wechat.Contains(keyword));
                        break;
                    case "All":
                        expression = expression.And(t => t.F_FullName.Contains(keyword) 
                            || t.F_Contacts.Contains(keyword)
                            || t.F_Mobile.Contains(keyword) 
                            || t.F_Tel.Contains(keyword)
                            || t.F_QQ.Contains(keyword)
                            || t.F_Wechat.Contains(keyword)
                            || t.F_CompanyName.Contains(keyword)
                            || t.F_Contacts.Contains(keyword)
                            || t.F_City.Contains(keyword)
                            );
                        break;
                    default:
                        break;
                }
            }
            //expression = expression.And(t => t.IsToCustom != 1);
            return this.BaseRepository().FindList(expression, pagination).ToAuthorizeData();
        } 
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ChanceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 商机名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<ChanceEntity>();
            expression = expression.And(t => t.F_FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_ChanceId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
                db.Delete<ChanceEntity>(keyValue);
                db.Delete<TrailRecordEntity>(t => t.F_ObjectId.Equals(keyValue));
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
        public void SaveForm(string keyValue, ChanceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    entity.Create();
                    db.Insert(entity);
                    //占用单据号
                    coderuleService.UseRuleSeed(entity.F_CreateUserId, SystemInfo.CurrentModuleId, "", db);
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 商机作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void Invalid(string keyValue)
        {
            ChanceEntity entity = new ChanceEntity();
            entity.Modify(keyValue);
            entity.F_ChanceState = 0;
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 商机转换客户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ToCustomer(string keyValue)
        {
            ChanceEntity chanceEntity = this.GetEntity(keyValue);
            IEnumerable<TrailRecordEntity> trailRecordList = trailRecordService.GetList(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                chanceEntity.Modify(keyValue);
                chanceEntity.F_IsToCustom = 1;
                db.Update<ChanceEntity>(chanceEntity);

                CustomerEntity customerEntity = new CustomerEntity();
                customerEntity.Create();
                customerEntity.F_EnCode = coderuleService.SetBillCode(customerEntity.F_CreateUserId, "", ((int)CodeRuleEnum.Customer_CustomerCode).ToString(), db);
                customerEntity.F_FullName = chanceEntity.F_CompanyName;
                customerEntity.F_TraceUserId = chanceEntity.F_TraceUserId;
                customerEntity.F_TraceUserName = chanceEntity.F_TraceUserName;
                customerEntity.F_CustIndustryId = chanceEntity.F_CompanyNatureId;
                customerEntity.F_CompanySite = chanceEntity.F_CompanySite;
                customerEntity.F_CompanyDesc = chanceEntity.F_CompanyDesc;
                customerEntity.F_CompanyAddress = chanceEntity.F_CompanyAddress;
                customerEntity.F_Province = chanceEntity.F_Province;
                customerEntity.F_City = chanceEntity.F_City;
                customerEntity.F_Contact = chanceEntity.F_Contacts;
                customerEntity.F_Mobile = chanceEntity.F_Mobile;
                customerEntity.F_Tel = chanceEntity.F_Tel;
                customerEntity.F_Fax = chanceEntity.F_Fax;
                customerEntity.F_QQ = chanceEntity.F_QQ;
                customerEntity.F_Email = chanceEntity.F_Email;
                customerEntity.F_Wechat = chanceEntity.F_Wechat;
                customerEntity.F_Hobby = chanceEntity.F_Hobby;
                customerEntity.F_Description = chanceEntity.F_Description;
                customerEntity.F_CustLevelId = "C";
                customerEntity.F_CustDegreeId = "往来客户";
                db.Insert<CustomerEntity>(customerEntity);

                foreach (TrailRecordEntity item in trailRecordList)
                {
                    item.F_TrailId = Guid.NewGuid().ToString();
                    item.F_ObjectId = customerEntity.F_CustomerId;
                    item.F_ObjectSort = 2;
                    db.Insert<TrailRecordEntity>(item);
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
