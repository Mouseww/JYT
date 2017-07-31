using LeaRun.Application.Code;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.BaseManage;
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
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.25 13:13
    /// 日 期：2016.02.23 10:02
    /// 描 述：工作流模板信息表操作（支持：SqlServer）
    /// </summary>
    public class WFSchemeInfoService : RepositoryFactory, IWFSchemeInfoService
    {
        private IUserService userservice = new UserService();
        #region 获取数据
        /// <summary>
        /// 获取流程列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
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
                                w.F_AuthorizeType
                            FROM
	                            WF_SchemeInfo w
                            LEFT JOIN 
	                            Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
                            WHERE w.F_DeleteMark = 0 ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.F_SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.F_SchemeCode LIKE @keyword 
                                        or w.F_SchemeName LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程列表数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetList(string queryJson)
        {
            try
            {
                string dd =OperatorProvider.Provider.Current().ObjectId;
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            distinct w.F_Id,
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
	                            w.F_ModifyUserName
                            FROM
	                            WF_SchemeInfo w
                            LEFT JOIN 
	                            Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
                            LEFT JOIN 
	                            WF_SchemeInfoAuthorize w2 ON w2.F_SchemeInfoId = w.F_Id
                            WHERE w.F_DeleteMark = 0 AND w.F_EnabledMark = 1  AND w.F_AuthorizeType != 2
                            AND ( w.F_AuthorizeType = 0  ");
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    if (OperatorProvider.Provider.Current().ObjectId != "")
                    {
                        strSql.Append(string.Format(" OR w2.F_ObjectId in ('{0}','{1}') )", OperatorProvider.Provider.Current().ObjectId.Replace(",", "','"), OperatorProvider.Provider.Current().UserId));
                    }
                    else
                    {
                        strSql.Append(" ) ");
                    }
                }
                else
                {
                    strSql.Append(" OR w.F_AuthorizeType = 1 ) ");
                }
               

                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();

                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.F_SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.F_SchemeCode LIKE @keyword 
                                        or w.F_SchemeName LIKE @keyword 
                                        or w.F_Description LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取一个流程模板实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFSchemeInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFSchemeInfoEntity>(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据一个功能模块Id获取一个流程模板
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public WFSchemeInfoEntity GetEntityByModuleId(string moduleId)
        {
            try
            {
                var expression = LinqExtensions.True<WFSchemeInfoEntity>();
                expression = expression.And(t => t.F_ModuleId == moduleId);
                return this.BaseRepository().FindEntity<WFSchemeInfoEntity>(expression);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取权限列表数据
        /// </summary>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        public IEnumerable<WFSchemeInfoAuthorizeEntity> GetAuthorizeEntityList(string schemeInfoId)
        {
            try
            {
                var expression = LinqExtensions.True<WFSchemeInfoAuthorizeEntity>();
                expression = expression.And(t => t.F_SchemeInfoId == schemeInfoId);
                return this.BaseRepository().FindList<WFSchemeInfoAuthorizeEntity>(expression);
            }
            catch
            {
                throw;
            }
        }




        /// <summary>
        /// 获取流程模板列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            w1.*,
	                            t2.F_ItemName,
                                t2.F_ItemDetailId as F_ItemId
                            FROM
                                WF_SchemeInfo w1
                            LEFT JOIN Base_DataItemDetail t2 ON t2.F_ItemDetailId = w1.F_SchemeType
                            ORDER BY t2.F_SortCode");
                return this.BaseRepository().FindTable(strSql.ToString());
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程设计（保存,编辑）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="shcemeAuthorizeData"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, WFSchemeInfoEntity entity,string[] shcemeAuthorizeData)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    db.Insert<WFSchemeInfoEntity>(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    db.Update<WFSchemeInfoEntity>(entity);
                }

                db.Delete<WFSchemeInfoAuthorizeEntity>(entity.F_Id, "F_SchemeInfoId");
                foreach (string item in shcemeAuthorizeData)
                {
                    if (item != "")
                    {
                        WFSchemeInfoAuthorizeEntity _authorizeEntity = new WFSchemeInfoAuthorizeEntity();
                        _authorizeEntity.Create();
                        _authorizeEntity.F_SchemeInfoId = entity.F_Id;
                        _authorizeEntity.F_ObjectId = item;
                        db.Insert(_authorizeEntity);
                    }
                }
                db.Commit();
                return 1;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 删除流程设计
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int DeleteEntity(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<WFSchemeInfoEntity>(keyValue);
                db.Delete<WFSchemeInfoAuthorizeEntity>(keyValue, "F_SchemeInfoId");
                db.Commit();
                return 1;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 更新流程模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                WFSchemeInfoEntity entity = new WFSchemeInfoEntity();
                entity.Modify(keyValue);
                entity.F_EnabledMark = state;
                this.BaseRepository().Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}
