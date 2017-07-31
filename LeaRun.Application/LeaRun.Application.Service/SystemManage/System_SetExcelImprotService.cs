using LeaRun.Application.Code;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
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
//using System.Data.Common;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 Learun ADMS6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：陈小二
    /// 日 期：2016-12-04 14:46
    /// 描 述：System_SetExcelImprot
    /// </summary>
    public class System_SetExcelImprotService : RepositoryFactory, System_SetExcelImprotIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<System_SetExcelImprotEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var expression = LinqExtensions.True<System_SetExcelImprotEntity>();
            if (!queryParam["Keyword"].IsEmpty())//关键字查询
            {
                string keyord = queryParam["Keyword"].ToString();
                expression = expression.And(t => t.F_Name.Contains(keyord));
            }
            if (!queryParam["F_ModuleId"].IsEmpty())//模块类型查询
            {
                string f_ModuleId = queryParam["F_ModuleId"].ToString();
                expression = expression.And(t => t.F_ModuleId == f_ModuleId);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<System_SetExcelImprotEntity> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                            t.F_Id,
	                            t.F_Name,
	                            t.F_ModuleId,
	                            t.F_ModuleBtnId,
	                            t.F_DbId,
	                            t.F_DbTable,
	                            t.F_ErrorType,
	                            t.F_EnabledMark,
                                t.F_Description,
	                            t.F_CreateDate,
	                            t.F_CreateUserId,
	                            t.F_CreateUserName,
	                            t.F_ModifyDate,
	                            t.F_ModifyUserId,
	                            t.F_ModifyUserName
                            FROM
	                            System_SetExcelImprot t
                            WHERE 1=1 
            ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["F_EnabledMark"].IsEmpty())
            {
                strSql.Append(" AND t.F_EnabledMark = @F_EnabledMark ");
                parameter.Add(DbParameters.CreateDbParameter("@F_EnabledMark", queryParam["F_EnabledMark"].ToString()));
            }
            return this.BaseRepository().FindList<System_SetExcelImprotEntity>(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public System_SetExcelImprotEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<System_SetExcelImprotEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {

            var db = this.BaseRepository().BeginTrans();
            try
            {
                System_SetExcelImprotEntity entity = db.FindEntity<System_SetExcelImprotEntity>(keyValue);
                var expression = LinqExtensions.True<System_SetExcelImportFiledEntity>();
                string f_ImportTemplateId = entity.F_Id;
                expression = expression.And(t => t.F_ImportTemplateId == f_ImportTemplateId);
                db.Delete<System_SetExcelImprotEntity>(keyValue);
                db.Delete(expression);
                db.Commit();
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, System_SetExcelImprotEntity entity, List<System_SetExcelImportFiledEntity> filedList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                }

                var expression = LinqExtensions.True<System_SetExcelImportFiledEntity>();
                string f_ImportTemplateId = entity.F_Id;
                expression = expression.And(t => t.F_ImportTemplateId == f_ImportTemplateId);
                db.Delete(expression);

                foreach (var item in filedList)
                {
                    item.F_ImportTemplateId = f_ImportTemplateId;
                    item.Create();
                    db.Insert(item);
                }

                db.Commit();
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public void UpdateForm(string keyValue, System_SetExcelImprotEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    throw new System.Exception("找不到需要更新的数据");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 执行excel模板数据导入
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable ExcelImport(string keyValue, DataTable dt)
        {
            System_SetExcelImportFiledService filedService = new System_SetExcelImportFiledService();
            DataItemDetailService dataItemService = new DataItemDetailService();
            DataBaseLinkService dbService = new DataBaseLinkService();
            try
            {
                System_SetExcelImprotEntity templateInfo = this.BaseRepository().FindEntity<System_SetExcelImprotEntity>(keyValue);
                IEnumerable<System_SetExcelImportFiledEntity> filedsInfo = filedService.GetList("{\"F_ImportTemplateId\":\"" + templateInfo.F_Id + "\"}");

                Dictionary<string, IEnumerable<DataItemDetailEntity>> dicDataItem = new Dictionary<string, IEnumerable<DataItemDetailEntity>>();

                string sql = " INSERT INTO " + templateInfo.F_DbTable + " (";
                string sqlValue = "(";
                bool isfirt = true;
                //组织sql语句
                //INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
                foreach (var col in filedsInfo)
                {
                    if (!isfirt)
                    {
                        sql += ",";
                        sqlValue += ",";
                    }
                    sql += col.F_FliedName;
                    sqlValue += "@" + col.F_FliedName;
                    isfirt = false;
                }
                sql += " ) VALUES " + sqlValue + ")";

                string sqlonly = " select * from " + templateInfo.F_DbTable + " where ";

                dt.Columns.Add("learunColOk", typeof(string));
                dt.Columns.Add("learunColError", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        var parameter = new List<DbParameter>();
                        foreach (var col in filedsInfo)
                        {
                            string paramName = "@" + col.F_FliedName;

                            switch (col.F_RelationType)
                            {
                                case 0://无关联
                                    if (col.F_FiledType == "日期")
                                    {
                                        parameter.Add(DbParameters.CreateDbParameter(paramName, DateTime.ParseExact(dr[col.F_ColName].ToString(), "m/d/yy", System.Globalization.CultureInfo.CurrentCulture)));
                                    }
                                    else
                                    {
                                        parameter.Add(DbParameters.CreateDbParameter(paramName, dr[col.F_ColName].ToString()));
                                    }
                                    IsOnlyOne(dbService, col, sqlonly, dr[col.F_ColName].ToString(), templateInfo.F_DbId);
                                    break;
                                case 1://GUID
                                    parameter.Add(DbParameters.CreateDbParameter(paramName, Guid.NewGuid().ToString()));
                                    break;
                                case 2://数据字典
                                    string dataItemName = "";
                                    if (!dicDataItem.ContainsKey(col.F_FliedName))
                                    {
                                        IEnumerable<DataItemDetailEntity> dataItemList = dataItemService.GetList(col.F_DataItemEncode);
                                        dicDataItem.Add(col.F_FliedName, dataItemList);
                                    }
                                    dataItemName = FindDataItemName(dicDataItem[col.F_FliedName], dr[col.F_ColName].ToString(), col.F_ColName);
                                    parameter.Add(DbParameters.CreateDbParameter(paramName, dataItemName));

                                    IsOnlyOne(dbService, col, sqlonly, dataItemName, templateInfo.F_DbId);
                                    break;
                                case 3://数据表
                                    string v = "";
                                    try
                                    {
                                        string strSql = "select " + col.F_DbSaveFlied + " from " + col.F_DbTable + " where " + col.F_DbRelationFlied + " = @" + col.F_DbRelationFlied;
                                        var parameter2 = new List<DbParameter>();
                                        parameter2.Add(DbParameters.CreateDbParameter("@" + col.F_DbRelationFlied, dr[col.F_ColName].ToString()));
                                        DataTable dt2 = dbService.FindTable(col.F_DbId, strSql, parameter2.ToArray());
                                        v = dt2.Rows[0][0].ToString();
                                        parameter.Add(DbParameters.CreateDbParameter(paramName, dt2.Rows[0][0].ToString()));


                                    }
                                    catch (Exception)
                                    {
                                        throw (new Exception("【" + col.F_ColName + "】 找不到对应的数据"));
                                    }
                                    IsOnlyOne(dbService, col, sqlonly, v, templateInfo.F_DbId);
                                    break;
                                case 4://固定值
                                    parameter.Add(DbParameters.CreateDbParameter(paramName, col.F_Value));
                                    break;
                                case 5://操作人ID
                                    parameter.Add(DbParameters.CreateDbParameter(paramName, OperatorProvider.Provider.Current().UserId));
                                    break;
                                case 6://操作人名字
                                    parameter.Add(DbParameters.CreateDbParameter(paramName, OperatorProvider.Provider.Current().UserName));
                                    break;
                                case 7://操作时间
                                    parameter.Add(DbParameters.CreateDbParameter(paramName, DateTime.Now));
                                    break;
                            }
                        }
                        dbService.ExecuteBySql(templateInfo.F_DbId, sql, parameter.ToArray());
                        dr["learunColOk"] = "1";
                    }
                    catch (Exception ex)
                    {
                        if (templateInfo.F_ErrorType == 0)
                        {
                            break;
                        }
                        dr["learunColOk"] = "0";
                        dr["learunColError"] = ex.Message;
                    }
                }

                return dt;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 数据字典查找name
        /// </summary>
        /// <param name="dataItemList"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        private string FindDataItemName(IEnumerable<DataItemDetailEntity> dataItemList, string itemName, string colName)
        {
            foreach (var one in dataItemList)
            {
                if (one.F_ItemName == itemName)
                {
                    return one.F_ItemValue;
                }
            }
            throw (new Exception("【" + colName + "】数据字典找不到对应值"));
        }
        /// <summary>
        /// 判断是否数据有重复
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="col"></param>
        /// <param name="sqlonly"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        private void IsOnlyOne(DataBaseLinkService dbService, System_SetExcelImportFiledEntity col, string sqlonly, string value, string dbId)
        {
            if (col.F_OnlyOne == 1)
            {
                var parameter = new List<DbParameter>();

                sqlonly += col.F_FliedName + " = @" + col.F_FliedName;
                parameter.Add(DbParameters.CreateDbParameter("@" + col.F_FliedName, value));

                var d = dbService.FindTable(dbId, sqlonly, parameter.ToArray());
                if (d.Rows.Count > 0)
                {
                    throw new Exception("【" + col.F_ColName + "】此项数据不能重复");
                }
            }
        }
        #endregion
    }
}
