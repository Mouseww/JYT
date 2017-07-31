using LeaRun.Application.Code;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
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
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例表操作
    /// </summary>
    public class WFProcessInstanceService : RepositoryFactory, IWFProcessInstanceService
    {
        private IDataBaseLinkService dataBaseLinkService = new DataBaseLinkService();

        #region 获取数据
        /// <summary>
        /// 获取流程监控数据（用于流程监控）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try{
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            w.F_Id,
	                            w.F_Code,
	                            w.F_Name,
	                            w.F_WfLevel,
	                            w.F_ProcessSchemeId,
	                            w.F_SchemeType,
	                            t2.F_ItemName AS F_SchemeTypeName,
	                            w.F_EnabledMark,
	                            w.F_CreateDate,
	                            w.F_CreateUserId,
	                            w.F_CreateUserName,
	                            w.F_Description
                            FROM
	                            WF_ProcessInstance w
                            LEFT JOIN WF_ProcessScheme w1 ON w1.F_Id = w.F_ProcessSchemeId
                            LEFT JOIN Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType where 1=1 ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["WFSchemeInfoId"].IsEmpty())
                {
                    strSql.Append(" AND w1.F_SchemeInfoId = @WFSchemeInfoId ");
                    parameter.Add(DbParameters.CreateDbParameter("@WFSchemeInfoId", queryParam["WFSchemeInfoId"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.F_Code LIKE @keyword 
                                        or w.F_CustomName LIKE @keyword 
                                        or w.F_CreateUserName LIKE @keyword 
                    )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch {
                throw;
            }
        }
        /// <summary>
        /// 获取我的流程实例数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetMyProcessPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"  SELECT
	                            w.F_Id,
	                            w.F_Code,
	                            w.F_Name,
	                            w.F_ProcessSchemeId,
	                            w.F_SchemeType,
	                            t2.F_ItemName AS F_SchemeTypeName,
	                            w.F_EnabledMark,
	                            w.F_CreateDate,
	                            w.F_CreateUserId,
	                            w.F_CreateUserName,
	                            w.F_Description,
	                            w.F_WfLevel,
                                w.F_IsFinished,
                                CASE WHEN n.F_IsActivtyId=1 THEN 1 ELSE 0 END AS F_Finished
                            FROM
	                            WF_ProcessInstance w
                            LEFT JOIN 
	                            Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
	                        LEFT JOIN WF_ProcessNodes n ON w.F_Id=n.F_ProcessId AND n.F_NodeName='结束' AND n.F_IsActivtyId=1
                            WHERE 1 = 1 ");//3表示草稿
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    strSql.Append(string.Format(" AND w.F_CreateUserId = '{0}' ", OperatorProvider.Provider.Current().UserId));
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
                    strSql.Append(@" AND ( w.F_Code LIKE @keyword 
                                        or w.F_Name LIKE @keyword 
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
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeBeforePageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                //strSql.Append(@"SELECT  w.F_Id ,
                //                        w.F_Code ,
                //                        w.F_Name ,
                //                        w.F_ProcessSchemeId ,
                //                        w.F_SchemeType ,
                //                        t2.F_ItemName AS F_SchemeTypeName ,
                //                        w.F_EnabledMark ,
                //                        w.F_CreateDate ,
                //                        w.F_CreateUserId ,
                //                        w.F_CreateUserName ,
                //                        w.F_Description ,
                //                        w.F_WfLevel ,
                //                        n.F_Id AS NodeId ,
                //                        n.F_NodeId ,
                //                        n.F_NodeName ,
                //                        n.F_NodeSate ,
                //                        n.F_NodeType ,
                //                        n.F_UserList
                //                FROM    WF_ProcessInstance w
                //                        LEFT JOIN WF_ProcessNodes n ON n.F_ProcessId = w.F_Id
                //                        LEFT JOIN Base_User u ON u.F_UserId = w.F_CreateUserId
                //                        LEFT JOIN Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
                //                WHERE   w.F_EnabledMark = 1
                //                        AND n.F_NodeSate = 0
                //                        AND n.F_NodeType != 'endround'
                //                        AND n.F_NodeType != 'startround'
                //                        AND u.F_DepartmentId = @F_DepartmentId");
                strSql.Append(@"SELECT  w.F_Id ,
                                        w.F_Code ,
                                        w.F_Name ,
                                        w.F_ProcessSchemeId ,
                                        w.F_SchemeType ,
                                        t2.F_ItemName AS F_SchemeTypeName ,
                                        w.F_EnabledMark ,
                                        w.F_CreateDate ,
                                        w.F_CreateUserId ,
                                        w.F_CreateUserName ,
                                        w.F_Description ,
                                        w.F_WfLevel ,
                                        n.F_Id AS NodeId ,
                                        n.F_NodeId ,
                                        n.F_NodeName ,
                                        n.F_NodeSate ,
                                        n.F_NodeType ,
                                        n.F_UserList
                                FROM    WF_ProcessInstance w
                                        LEFT JOIN WF_ProcessNodes n ON n.F_ProcessId = w.F_Id
                                        LEFT JOIN Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
                                WHERE   w.F_EnabledMark = 1
                                        AND n.F_NodeSate = 0
                                        AND n.F_NodeType != 'endround'
                                        AND n.F_NodeType != 'startround'");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                //parameter.Add(DbParameters.CreateDbParameter("@F_DepartmentId", OperatorProvider.Provider.Current().DepartmentId));
                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.F_SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.F_Code LIKE @keyword 
                                        or w.F_Name LIKE @keyword 
                                        or w.F_CreateUserName LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                strSql.Append(string.Format(@" AND ( n.F_UserList LIKE '%{0}%' or  n.F_UserList = '1' ", OperatorProvider.Provider.Current().UserId));
                foreach (var objectid in OperatorProvider.Provider.Current().ObjectId.Split(','))
                {
                    if (!string.IsNullOrEmpty(objectid))
                    {
                        strSql.Append(string.Format(@" or n.F_UserList LIKE '%{0}%' ", objectid));
                    }
                }
                strSql.Append(")");
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取登录者已经处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeAfterPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(string.Format(@"SELECT
	                                        w.F_Id,
	                                        w.F_Code,
	                                        w.F_Name,
	                                        w.F_ProcessSchemeId,
	                                        w.F_SchemeType,
	                                        t2.F_ItemName AS F_SchemeTypeName,
	                                        w.F_EnabledMark,
	                                        w.F_CreateDate,
	                                        w.F_CreateUserId,
	                                        w.F_CreateUserName,
	                                        w2.F_Content,
	                                        w.F_Description,
	                                        w.F_WfLevel
                                        FROM
	                                        WF_ProcessInstance w
                                        LEFT JOIN Base_DataItemDetail t2 ON t2.F_ItemDetailId = w.F_SchemeType
                                        LEFT JOIN WF_ProcessOperationHistory w2 ON w2.F_ProcessId = w.F_Id AND w2.F_Type != 0
                                        WHERE
	                                w.F_EnabledMark = 1 AND w2.F_CreateUserId = '{0}' ", OperatorProvider.Provider.Current().UserId));
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
                    strSql.Append(@" AND ( w.F_Code LIKE @keyword 
                                        or w.F_CustomName LIKE @keyword
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
        /// 获取实例进程信息实体
        /// </summary>
        /// <param name="keyVlaue"></param>
        /// <returns></returns>
        public WFProcessInstanceEntity GetEntity(string keyVlaue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyVlaue);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="wfCreateProcessModel"></param>
        public void CreateProcess(WFCreateProcessModel wfCreateProcessModel)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                wfCreateProcessModel.processInstanceEntity.Create();
                db.Insert(wfCreateProcessModel.processInstanceEntity);

                foreach (var item in wfCreateProcessModel.processNodeList)
                {
                    item.Create();
                    db.Insert(item);
                }
                foreach (var item in wfCreateProcessModel.processTransitionHistoryList)
                {
                    item.Create();
                    db.Insert(item);
                }
                foreach (var item in wfCreateProcessModel.delegateRecordEntitylist)
                {
                    item.Create();
                    db.Insert(item);
                }
                wfCreateProcessModel.processOperationHistoryEntity.Create();
                db.Insert(wfCreateProcessModel.processOperationHistoryEntity);

                //wfCreateProcessModel.processFormInstanceEntity.Create();
                //db.Insert(wfCreateProcessModel.processFormInstanceEntity);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 重新发起流程实例
        /// </summary>
        /// <param name="wfCreateProcessModel"></param>
        public void EditProcess(WFCreateProcessModel wfCreateProcessModel)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                db.Update(wfCreateProcessModel.processInstanceEntity);

                foreach (var item in wfCreateProcessModel.processNodeList)
                {
                    item.Create();
                    db.Insert(item);
                }
                foreach (var item in wfCreateProcessModel.processTransitionHistoryList)
                {
                    item.Create();
                    db.Insert(item);
                }
                foreach (var item in wfCreateProcessModel.delegateRecordEntitylist)
                {
                    item.Create();
                    db.Insert(item);
                }
                wfCreateProcessModel.processOperationHistoryEntity.Create();
                db.Insert(wfCreateProcessModel.processOperationHistoryEntity);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存节点审核数据
        /// </summary>
        /// <param name="wfVerificationProcessModel"></param>
        public void SaveProcessVerification(WFVerificationProcessModel wfVerificationProcessModel)
        {
           
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (wfVerificationProcessModel.wfProcessInstanceEntity != null)
                {
                    db.Update(wfVerificationProcessModel.wfProcessInstanceEntity);
                }

                if (wfVerificationProcessModel.processNodeList != null)
                {
                    foreach (var item in wfVerificationProcessModel.processNodeList)
                    {
                        if (!string.IsNullOrEmpty(item.F_Id))
                        {
                            item.Modify(item.F_Id);
                            db.Update(item);
                        }
                        else
                        {
                            item.Create();
                            var expression = LinqExtensions.True<WFProcessNodesEntity>();
                            expression = expression.And(t => t.F_ProcessId == item.F_ProcessId);
                            expression = expression.And(t => t.F_NodeId == item.F_NodeId);
                            WFProcessNodesEntity ee = this.BaseRepository().FindEntity<WFProcessNodesEntity>(expression);
                            if (ee != null)
                            {
                                db.Delete(ee);
                            }
                            db.Insert(item);
                        }
                        
                    }
                }
                if (wfVerificationProcessModel.processTransitionHistoryList != null)
                {
                    foreach (var item in wfVerificationProcessModel.processTransitionHistoryList)
                    {
                        item.Create();
                        db.Insert(item);
                    }
                }
                if (wfVerificationProcessModel.delegateRecordEntitylist != null)
                {
                    foreach (var item in wfVerificationProcessModel.delegateRecordEntitylist)
                    {
                        item.Create();
                        db.Insert(item);
                    }
                }
                //if (wfVerificationProcessModel.processFormInstanceEntityList != null)
                //{
                //    foreach (var item in wfVerificationProcessModel.processFormInstanceEntityList)
                //    {
                //        if (string.IsNullOrEmpty(item.F_Id))
                //        {
                //            item.Create();
                //            db.Insert(item);
                //        }
                //        else
                //        {
                //            item.Modify(item.F_Id);
                //            db.Update(item);
                //        }
                //    }
                //}
                wfVerificationProcessModel.processOperationHistoryEntity.Create();
                db.Insert(wfVerificationProcessModel.processOperationHistoryEntity);

                wfVerificationProcessModel.node.Modify(wfVerificationProcessModel.node.F_Id);
                db.Update(wfVerificationProcessModel.node);

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 删除工作流实例进程(删除草稿使用)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int DeleteProcess(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WFProcessInstanceEntity entity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyValue);
                db.Delete<WFProcessSchemeEntity>(entity.F_ProcessSchemeId);
                db.Delete<WFProcessInstanceEntity>(keyValue);
                db.Commit();
                return 1;
            }
            catch {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 虚拟操作实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="state">0暂停,1启用,2取消（召回）</param>
        /// <returns></returns>
        public int OperateVirtualProcess(string keyValue,int state)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WFProcessInstanceEntity entity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyValue);
                
                // 流程是否完成(0运行中,1运行结束,2被召回,3不同意,4表示被驳回)
                string content = "";
                switch (state)
                {
                    case 0:
                        if (entity.F_EnabledMark == 0)
                        {
                            return 1;
                        }
                        entity.F_EnabledMark = 0;
                        content = "【暂停】" + OperatorProvider.Provider.Current().UserName + "暂停了一个流程进程【" + entity.F_Code + "/" + entity.F_Name + "】";
                        break;
                    case 1:
                        if (entity.F_EnabledMark == 1)
                        {
                            return 1;
                        }
                        entity.F_EnabledMark = 1;
                        content = "【启用】" + OperatorProvider.Provider.Current().UserName + "启用了一个流程进程【" + entity.F_Code + "/" + entity.F_Name + "】";
                        break;
                    case 2:
                        entity.F_EnabledMark = 2;
                        content = "【召回】" + OperatorProvider.Provider.Current().UserName + "召回了一个流程进程【" + entity.F_Code + "/" + entity.F_Name + "】";
                        break;
                }
                db.Update<WFProcessInstanceEntity>(entity);
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.F_Type = 4;
                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.F_ProcessId = entity.F_Id;
                processOperationHistoryEntity.F_Content = content;
                db.Insert(processOperationHistoryEntity);
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
        /// 流程指派
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="makeLists"></param>
        public void DesignateProcess(string processId, string makeLists)
        {
            try
            {
                WFProcessInstanceEntity entity = new WFProcessInstanceEntity();
                entity.F_Id = processId;
                this.BaseRepository().Update(entity);
            }
            catch {
                throw;
            }
        }
        #endregion

        #region 流程流转节点
        /// <summary>
        /// 获取流程实例节点信息
        /// </summary>
        /// <param name="nodeId">主键</param>
        /// <returns></returns>
        public WFProcessNodesEntity GetProcessNode(string nodeId)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFProcessNodesEntity>(nodeId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程实例节点信息
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="processId">实例Id</param>
        /// <returns></returns>
        public WFProcessNodesEntity GetProcessNode(string nodeId, string processId)
        {
            try
            {
                var expression = LinqExtensions.True<WFProcessNodesEntity>();
                expression = expression.And(t => t.F_ProcessId == processId);
                expression = expression.And(t => t.F_NodeId == nodeId);
                return this.BaseRepository().FindEntity<WFProcessNodesEntity>(expression);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取工作流实例里所有流转的节点
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IEnumerable<WFProcessNodesEntity> GetProcessNodeList(string processId)
        {
            try
            {
                var expression = LinqExtensions.True<WFProcessNodesEntity>();
                expression = expression.And(t=>t.F_ProcessId == processId);
                return this.BaseRepository().FindList<WFProcessNodesEntity>(expression);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新流程节点信息
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateNode(WFProcessNodesEntity entity)
        {
            try 
	        {
                entity.Modify(entity.F_Id);
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
