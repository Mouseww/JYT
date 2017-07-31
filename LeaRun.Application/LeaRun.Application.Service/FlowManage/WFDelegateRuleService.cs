using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.BaseManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.01.14 11:02
    /// 描 述：工作流委托规则表操作类（支持：SqlServer）
    /// </summary>
    public class WFDelegateRuleService : RepositoryFactory, IWFDelegateRuleService
    {


        #region 获取数据
        /// <summary>
        /// 获取委托规则分页数据（不写委托人获取全部）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="userId">委托人</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson,string userId=null)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w1.F_Id,
	                                w1.F_ToUserId,
	                                w1.F_ToUserName,
	                                w1.F_BeginDate,
	                                w1.F_EndDate,
	                                w1.F_CreateUserId,
	                                w1.F_CreateUserName,
	                                w1.F_CreateDate,
	                                w1.F_Description,
                                    w1.F_EnabledMark,
	                                COUNT(w2.F_Id) as F_shcemeNum
                                FROM
	                                WF_DelegateRule w1
                                LEFT JOIN WF_DelegateRuleSchemeInfo w2 ON w2.F_DelegateRuleId = w1.F_Id
                                Where 1=1 
                               ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!string.IsNullOrEmpty(userId))
                {
                    strSql.Append(@" AND ( w1.F_CreateUserId = @CreateUserId )");
                    parameter.Add(DbParameters.CreateDbParameter("@CreateUserId",userId));
                }
                if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w1.F_ToUserName LIKE @keyword  )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                strSql.Append(@" GROUP BY
	                                w1.F_Id,
	                                w1.F_ToUserId,
	                                w1.F_ToUserName,
	                                w1.F_BeginDate,
	                                w1.F_EndDate,
	                                w1.F_CreateUserId,
	                                w1.F_CreateUserName,
	                                w1.F_CreateDate,
                                    w1.F_EnabledMark,
	                                w1.F_Description ");

                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程模板信息列表数据
        /// </summary>
        /// <param name="ruleId">委托规则Id</param>
        /// <returns></returns>
        public DataTable GetSchemeInfoList(string ruleId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.F_Id,
	                                w.F_SchemeCode,
	                                w.F_SchemeName,
	                                w.F_SchemeType,
	                                t2.F_ItemName AS F_SchemeTypeName,
	                                w.F_SortCode,
	                                w.F_DeleteMark,
	                                w.F_EnabledMark,
	                                w.F_Description,
	                                w.F_CreateDate,
	                                w.F_CreateUserId,
	                                w.F_CreateUserName,
	                                w.F_ModifyDate,
	                                w.F_ModifyUserId,
	                                w.F_ModifyUserName,
	                                CASE
                                WHEN t3.F_Id IS NOT NULL THEN
	                                '1'
                                ELSE
	                                '0'
                                END AS F_ischecked
                                FROM
	                                WF_SchemeInfo w
                                LEFT JOIN Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
                                LEFT JOIN WF_DelegateRuleSchemeInfo t3 ON t3.F_SchemeInfoId = w.F_Id and t3.F_DelegateRuleId = @ruleId
                                WHERE
	                                w.F_DeleteMark = 0
                                AND w.F_EnabledMark = 1
                                ORDER BY
	                                w.F_SchemeType,
	                                w.F_SchemeCode");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@ruleId", string.IsNullOrEmpty(ruleId) ? " " : ruleId));
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据模板信息Id获取委托规则实体
        /// </summary>
        /// <param name="shcemeInfoId"></param>
        /// <param name="objectIdList"></param>
        /// <returns></returns>
        public DataTable GetEntityBySchemeInfoId(string shcemeInfoId,string[] objectIdList)
        {
            try
            {
                IPermissionService service = new PermissionService();
                string userIdlist = "";
                foreach (string item in objectIdList)
                {
                    List<UserRelationEntity> list = service.GetMemberList(item).ToList();
                    foreach (var item1 in list)
                    {
                        if (userIdlist != "")
                        {
                            userIdlist += "','";
                        }
                        userIdlist += item1.F_UserId;
                    }
                    if (userIdlist != "")
                    {
                        userIdlist += "','";
                    }
                    userIdlist += item;
                }
                var strSql = new StringBuilder();
                strSql.Append(string.Format(@"SELECT
	                               	    w1.F_Id,
	                                    w1.F_ToUserId,
	                                    w1.F_ToUserName,
	                                    w1.F_BeginDate,
	                                    w1.F_EndDate,
	                                    w1.F_CreateDate,
	                                    w1.F_CreateUserId,
	                                    w1.F_CreateUserName,
	                                    w1.F_EnabledMark,
	                                    w1.F_Description
                                    FROM
	                                    WF_DelegateRule w1
                                    LEFT JOIN WF_DelegateRuleSchemeInfo w2 ON w2.F_DelegateRuleId = w1.F_Id
                                    WHERE
	                                    w1.F_EnabledMark = 1 AND w1.F_BeginDate <='{0}' AND w1.F_EndDate >='{0}' AND w1.F_CreateUserId in ('{1}')
                                   AND w2.F_SchemeInfoId = @SchemeInfoId
                                ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userIdlist));
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@SchemeInfoId", shcemeInfoId));
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取委托规则实体对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public WFDelegateRuleEntity GetEntity(string keyValue)
        {
            try
            {
               return this.BaseRepository().FindEntity<WFDelegateRuleEntity>(keyValue);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存委托规则
        /// </summary>
        /// <returns></returns>
        public int SaveDelegateRule(string keyValue,WFDelegateRuleEntity ruleEntity,string[] shcemeInfoIdlist)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    ruleEntity.Create();
                    db.Insert(ruleEntity);
                }
                else
                {
                    ruleEntity.Modify(keyValue);
                    db.Update(ruleEntity);
                }
                db.Delete<WFDelegateRuleSchemeInfoEntity>(ruleEntity.F_Id, "F_DelegateRuleId");
                foreach (string item in shcemeInfoIdlist)
                {
                    WFDelegateRuleSchemeInfoEntity entity = new WFDelegateRuleSchemeInfoEntity();
                    entity.Create();
                    entity.F_DelegateRuleId = ruleEntity.F_Id;
                    entity.F_SchemeInfoId = item;
                    db.Insert(entity);
                }
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 删除委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int DeleteRule(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<WFDelegateRuleEntity>(keyValue);
                db.Delete<WFDelegateRuleSchemeInfoEntity>(keyValue, "F_DelegateRuleId");
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
 
        }
        /// <summary>
        /// 使能委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="enableMark"></param>
        /// <returns></returns>
        public int UpdateRuleEnable(string keyValue, int enableMark)
        {
            try
            {
                WFDelegateRuleEntity entity = new WFDelegateRuleEntity();
                entity.Modify(keyValue);
                entity.F_EnabledMark = enableMark;
                this.BaseRepository().Update(entity);

                return 1;
            }
            catch {
                throw;
            }
        }
        #endregion

    }
}
