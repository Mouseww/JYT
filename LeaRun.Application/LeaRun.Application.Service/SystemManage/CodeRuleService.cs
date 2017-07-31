using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：刘百万 2016.11.24 14:51
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleService : RepositoryFactory<CodeRuleEntity>, ICodeRuleService
    {
        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CodeRuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CodeRuleEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //对象编号
                        expression = expression.And(t => t.F_EnCode.Contains(keyword));
                        break;
                    case "FullName":            //对象名称
                        expression = expression.And(t => t.F_FullName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CodeRuleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CodeRuleEntity codeRuleEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                codeRuleEntity.Modify(keyValue);
                this.BaseRepository().Update(codeRuleEntity);
            }
            else
            {
                codeRuleEntity.Create();
                this.BaseRepository().Insert(codeRuleEntity);
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<CodeRuleEntity>();
            expression = expression.And(t => t.F_EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_RuleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 规则名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<CodeRuleEntity>();
            expression = expression.And(t => t.F_FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_RuleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获得指定模块或者编号的单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        public string GetBillCode(string userId, string moduleId, string enCode)
        {
            IRepository db = new RepositoryFactory().BaseRepository();
            UserEntity userEntity = db.FindEntity<UserEntity>(userId);
            CodeRuleEntity coderuleentity = db.FindEntity<CodeRuleEntity>(t => t.F_ModuleId == moduleId || t.F_EnCode == enCode);
            //获得模板ID
            string billCode = "";//单据号
            string nextBillCode = "";//单据号
            bool isOutTime = false;//是否已过期
            if (coderuleentity != null)
            {
                try
                {
                    //判断种子是否已经产生,如果没有产生种子先插入一条初始种子
                    CodeRuleSeedEntity initSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.F_RuleId == coderuleentity.F_RuleId);
                    if (initSeed == null)
                    {
                        initSeed = new CodeRuleSeedEntity();
                        initSeed.Create();
                        initSeed.F_SeedValue = 1;
                        initSeed.F_RuleId = coderuleentity.F_RuleId;
                        db.Insert<CodeRuleSeedEntity>(initSeed);
                    }
                    else
                    {
                        db = new RepositoryFactory().BaseRepository().BeginTrans();
                    }
                    int nowSerious = 0;
                    //取得流水号种子
                    List<CodeRuleSeedEntity> codeRuleSeedlist = db.IQueryable<CodeRuleSeedEntity>(t => t.F_RuleId == coderuleentity.F_RuleId).ToList();
                    //取得最大种子
                    CodeRuleSeedEntity maxSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.F_UserId == null);
                    List<CodeRuleFormatEntity> codeRuleFormatList = coderuleentity.F_RuleFormatJson.ToList<CodeRuleFormatEntity>();
                    string dateFormatStr = "";
                    foreach (CodeRuleFormatEntity codeRuleFormatEntity in codeRuleFormatList)
                    {
                        switch (codeRuleFormatEntity.F_ItemType.ToString())
                        {
                            //自定义项
                            case "0":
                                billCode = billCode + codeRuleFormatEntity.F_FormatStr;
                                nextBillCode = nextBillCode + codeRuleFormatEntity.F_FormatStr;
                                break;
                            //日期
                            case "1":
                                //日期字符串类型
                                dateFormatStr=codeRuleFormatEntity.F_FormatStr;
                                billCode = billCode + DateTime.Now.ToString(codeRuleFormatEntity.F_FormatStr.Replace("m", "M"));
                                nextBillCode = nextBillCode + DateTime.Now.ToString(codeRuleFormatEntity.F_FormatStr.Replace("m", "M"));

                                break;
                            //流水号
                            case "2":
                                #region 处理流水号归0
                                //首先确定最大种子是否未归0的
                                if (dateFormatStr.Contains("dd"))
                                {
                                    if ((maxSeed.F_ModifyDate).ToDateString() != DateTime.Now.ToString("yyyy-MM-dd"))
                                    {
                                        isOutTime = true;
                                        maxSeed.F_SeedValue = 1;
                                        maxSeed.F_ModifyDate = DateTime.Now;
                                    }
                                }
                                else if (dateFormatStr.Contains("mm"))
                                {
                                    if (((DateTime)maxSeed.F_ModifyDate).ToString("yyyy-MM") != DateTime.Now.ToString("yyyy-MM"))
                                    {
                                        isOutTime = true;
                                        maxSeed.F_SeedValue = 1;
                                        maxSeed.F_ModifyDate = DateTime.Now;
                                    }
                                }
                                else if (dateFormatStr.Contains("yy"))
                                {
                                    if (((DateTime)maxSeed.F_ModifyDate).ToString("yyyy") != DateTime.Now.ToString("yyyy"))
                                    {
                                        isOutTime = true;
                                        maxSeed.F_SeedValue = 1;
                                        maxSeed.F_ModifyDate = DateTime.Now;
                                    }
                                }
                                #endregion
                                //查找当前用户是否已有之前未用掉的种子
                                CodeRuleSeedEntity codeRuleSeedEntity = codeRuleSeedlist.Find(t => t.F_UserId == userId && t.F_RuleId == coderuleentity.F_RuleId);
                                //删除已过期的用户未用掉的种子
                                if (codeRuleSeedEntity != null && isOutTime)
                                {
                                    db.Delete<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    codeRuleSeedEntity = null;
                                }
                                //如果没有就取当前最大的种子
                                if (codeRuleSeedEntity != null)
                                {
                                    nowSerious = (int)codeRuleSeedEntity.F_SeedValue;
                                }
                                else
                                {
                                    //取得系统最大的种子
                                    int maxSerious = (int)maxSeed.F_SeedValue;
                                    nowSerious = maxSerious;
                                    codeRuleSeedEntity = new CodeRuleSeedEntity();
                                    codeRuleSeedEntity.Create();
                                    codeRuleSeedEntity.F_SeedValue = maxSerious;
                                    codeRuleSeedEntity.F_UserId = userId;
                                    codeRuleSeedEntity.F_RuleId = coderuleentity.F_RuleId;
                                    db.Insert<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    //处理种子更新
                                    maxSeed.F_SeedValue += 1;
                                    maxSeed.Modify(maxSeed.F_RuleSeedId);
                                    db.Update<CodeRuleSeedEntity>(maxSeed);
                                }
                                string seriousStr = new string('0', (int)(codeRuleFormatEntity.F_FormatStr.Length - nowSerious.ToString().Length)) + nowSerious.ToString();
                                string NextSeriousStr = new string('0', (int)(codeRuleFormatEntity.F_FormatStr.Length - nowSerious.ToString().Length)) + maxSeed.F_SeedValue.ToString();
                                billCode = billCode + seriousStr;
                                nextBillCode = nextBillCode + NextSeriousStr;

                                break;
                            //部门
                            case "3":
                                DepartmentEntity departmentEntity = db.FindEntity<DepartmentEntity>(userEntity.F_DepartmentId);
                                if (codeRuleFormatEntity.F_FormatStr == "code")
                                {
                                    billCode = billCode + departmentEntity.F_EnCode;
                                    nextBillCode = nextBillCode + departmentEntity.F_EnCode;
                                }
                                else
                                {
                                    billCode = billCode + departmentEntity.F_FullName;
                                    nextBillCode = nextBillCode + departmentEntity.F_FullName;

                                }
                                break;
                            //公司
                            case "4":
                                OrganizeEntity organizeEntity = db.FindEntity<OrganizeEntity>(userEntity.F_OrganizeId);
                                if (codeRuleFormatEntity.F_FormatStr == "code")
                                {
                                    billCode = billCode + organizeEntity.F_EnCode;
                                    nextBillCode = nextBillCode + organizeEntity.F_EnCode;

                                }
                                else
                                {
                                    billCode = billCode + organizeEntity.F_FullName;
                                    nextBillCode = nextBillCode + organizeEntity.F_FullName;

                                }
                                break;
                            //用户
                            case "5":
                                if (codeRuleFormatEntity.F_FormatStr == "code")
                                {
                                    billCode = billCode + userEntity.F_EnCode;
                                    nextBillCode = nextBillCode + userEntity.F_EnCode;
                                }
                                else
                                {
                                    billCode = billCode + userEntity.F_Account;
                                    nextBillCode = nextBillCode + userEntity.F_Account;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    coderuleentity.F_CurrentNumber = nextBillCode;
                    db.Update<CodeRuleEntity>(coderuleentity);
                }
                catch (Exception)
                {
                    db.Rollback();
                    return billCode;
                }
                db.Commit();
            }
            return billCode;
        }
        /// <summary>
        /// 获得指定模块或者编号的单据号（直接使用）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        public string SetBillCode(string userId, string moduleId, string enCode, IRepository db = null)
        {
            IRepository dbc = null;
            if (db == null)
            {
                dbc = new RepositoryFactory().BaseRepository();
            }
            else
            {
                dbc = db;
            }
            UserEntity userEntity = db.FindEntity<UserEntity>(userId);
            CodeRuleEntity coderuleentity = db.FindEntity<CodeRuleEntity>(t => t.F_ModuleId == moduleId || t.F_EnCode == enCode);
            //判断种子是否已经产生,如果没有产生种子先插入一条初始种子
            CodeRuleSeedEntity initSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.F_RuleId == coderuleentity.F_RuleId);
            if (initSeed == null)
            {
                initSeed = new CodeRuleSeedEntity();
                initSeed.Create();
                initSeed.F_SeedValue = 1;
                initSeed.F_RuleId = coderuleentity.F_RuleId;
                initSeed.F_CreateDate = null;
                //db.Insert<CodeRuleSeedEntity>(initSeed);
            }
            //获得模板ID
            string billCode = "";//单据号
            string nextBillCode = "";//单据号
            bool isOutTime = false;//是否已过期
            if (coderuleentity != null)
            {
                try
                {
                    int nowSerious = 0;
                    //取得流水号种子
                    List<CodeRuleSeedEntity> codeRuleSeedlist = db.IQueryable<CodeRuleSeedEntity>(t => t.F_RuleId == coderuleentity.F_RuleId).ToList();
                    //取得最大种子
                    CodeRuleSeedEntity maxSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.F_UserId == null);

                    if (maxSeed == null)
                    {
                        maxSeed = initSeed;
                    }
                    List<CodeRuleFormatEntity> codeRuleFormatList = coderuleentity.F_RuleFormatJson.ToList<CodeRuleFormatEntity>();
                    string dateFormatStr = "";
                    foreach (CodeRuleFormatEntity codeRuleFormatEntity in codeRuleFormatList)
                    {
                        switch (codeRuleFormatEntity.F_ItemType.ToString())
                        {
                            //自定义项
                            case "0":
                                billCode = billCode + codeRuleFormatEntity.F_FormatStr;
                                nextBillCode = nextBillCode + codeRuleFormatEntity.F_FormatStr;
                                break;
                            //日期
                            case "1":
                                //日期字符串类型
                                dateFormatStr = codeRuleFormatEntity.F_FormatStr;
                                billCode = billCode + DateTime.Now.ToString(codeRuleFormatEntity.F_FormatStr.Replace("m", "M"));
                                nextBillCode = nextBillCode + DateTime.Now.ToString(codeRuleFormatEntity.F_FormatStr.Replace("m", "M"));

                                break;
                            //流水号
                            case "2":
                                #region 处理流水号归0
                                if (dateFormatStr.Contains("dd"))
                                {
                                    if ((maxSeed.F_ModifyDate).ToDateString() != DateTime.Now.ToString("yyyy-MM-dd"))
                                    {
                                        isOutTime = true;
                                        maxSeed.F_SeedValue = 1;
                                        maxSeed.F_ModifyDate = DateTime.Now;
                                    }
                                }
                                else if (dateFormatStr.Contains("mm"))
                                {
                                    if (((DateTime)maxSeed.F_ModifyDate).ToString("yyyy-MM") != DateTime.Now.ToString("yyyy-MM"))
                                    {
                                        isOutTime = true;
                                        maxSeed.F_SeedValue = 1;
                                        maxSeed.F_ModifyDate = DateTime.Now;
                                    }
                                }
                                else if (dateFormatStr.Contains("yy"))
                                {
                                    if (((DateTime)maxSeed.F_ModifyDate).ToString("yyyy") != DateTime.Now.ToString("yyyy"))
                                    {
                                        isOutTime = true;
                                        maxSeed.F_SeedValue = 1;
                                        maxSeed.F_ModifyDate = DateTime.Now;
                                    }
                                }
                                #endregion
                                //查找当前用户是否已有之前未用掉的种子
                                CodeRuleSeedEntity codeRuleSeedEntity = codeRuleSeedlist.Find(t => t.F_UserId == userId && t.F_RuleId == coderuleentity.F_RuleId);
                                //删除已过期的用户未用掉的种子
                                if (codeRuleSeedEntity != null && isOutTime)
                                {
                                    db.Delete<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    codeRuleSeedEntity = null;
                                }
                                //如果没有就取当前最大的种子
                                if (codeRuleSeedEntity == null)
                                {
                                    //取得系统最大的种子
                                    int maxSerious = (int)maxSeed.F_SeedValue;
                                    nowSerious = maxSerious;
                                    codeRuleSeedEntity = new CodeRuleSeedEntity();
                                    codeRuleSeedEntity.Create();
                                    codeRuleSeedEntity.F_SeedValue = maxSerious;
                                    codeRuleSeedEntity.F_UserId = userId;
                                    codeRuleSeedEntity.F_RuleId = coderuleentity.F_RuleId;
                                    //db.Insert<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    //处理种子更新
                                    maxSeed.F_SeedValue += 1;
                                    if (maxSeed.F_CreateDate != null)
                                    {
                                        db.Update<CodeRuleSeedEntity>(maxSeed);
                                    }
                                    else
                                    {
                                        maxSeed.F_CreateDate = DateTime.Now;
                                        db.Insert<CodeRuleSeedEntity>(maxSeed);
                                    }
                                }
                                else
                                {
                                    nowSerious = (int)codeRuleSeedEntity.F_SeedValue;
                                }
                                string seriousStr = new string('0', (int)(codeRuleFormatEntity.F_FormatStr.Length - nowSerious.ToString().Length)) + nowSerious.ToString();
                                string NextSeriousStr = new string('0', (int)(codeRuleFormatEntity.F_FormatStr.Length - nowSerious.ToString().Length)) + maxSeed.F_SeedValue.ToString();
                                billCode = billCode + seriousStr;
                                nextBillCode = nextBillCode + NextSeriousStr;

                                break;
                            //部门
                            case "3":
                                DepartmentEntity departmentEntity = db.FindEntity<DepartmentEntity>(userEntity.F_DepartmentId);
                                if (codeRuleFormatEntity.F_FormatStr == "code")
                                {
                                    billCode = billCode + departmentEntity.F_EnCode;
                                    nextBillCode = nextBillCode + departmentEntity.F_EnCode;
                                }
                                else
                                {
                                    billCode = billCode + departmentEntity.F_FullName;
                                    nextBillCode = nextBillCode + departmentEntity.F_FullName;

                                }
                                break;
                            //公司
                            case "4":
                                OrganizeEntity organizeEntity = db.FindEntity<OrganizeEntity>(userEntity.F_OrganizeId);
                                if (codeRuleFormatEntity.F_FormatStr == "code")
                                {
                                    billCode = billCode + organizeEntity.F_EnCode;
                                    nextBillCode = nextBillCode + organizeEntity.F_EnCode;

                                }
                                else
                                {
                                    billCode = billCode + organizeEntity.F_FullName;
                                    nextBillCode = nextBillCode + organizeEntity.F_FullName;

                                }
                                break;
                            //用户
                            case "5":
                                if (codeRuleFormatEntity.F_FormatStr == "code")
                                {
                                    billCode = billCode + userEntity.F_EnCode;
                                    nextBillCode = nextBillCode + userEntity.F_EnCode;
                                }
                                else
                                {
                                    billCode = billCode + userEntity.F_Account;
                                    nextBillCode = nextBillCode + userEntity.F_Account;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    coderuleentity.F_CurrentNumber = nextBillCode;
                    db.Update<CodeRuleEntity>(coderuleentity);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return billCode;
        }
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        ///<param name="db">事务，如果有使用事务请将事务传入方法</param>
        /// <returns>true/false</returns>
        public bool UseRuleSeed(string userId, string moduleId, string enCode, IRepository db = null)
        {
            IRepository dbc = null;
            if (db == null)
            {
                dbc = new RepositoryFactory().BaseRepository();
            }
            else
            {
                dbc = db;
            }
            UserEntity userEntity = dbc.FindEntity<UserEntity>(userId);
            CodeRuleEntity coderuleentity = dbc.FindEntity<CodeRuleEntity>(t => t.F_ModuleId == moduleId || t.F_EnCode == enCode);
            try
            {
                if (coderuleentity != null)
                {
                    //删除用户已经用掉的种子
                    dbc.Delete<CodeRuleSeedEntity>(t => t.F_UserId == userId && t.F_RuleId == coderuleentity.F_RuleId);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
