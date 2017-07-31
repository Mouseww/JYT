using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 Learun ADMS6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：陈小二
    /// 日 期：2016-12-04 14:46
    /// 描 述：System_SetExcelImportFiled
    /// </summary>
    public class System_SetExcelImportFiledService : RepositoryFactory<System_SetExcelImportFiledEntity>, System_SetExcelImportFiledIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<System_SetExcelImportFiledEntity> GetPageList(Pagination pagination, string queryJson)
        {
             return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<System_SetExcelImportFiledEntity> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                        t.F_Id,
	                        t.F_ImportTemplateId,
	                        t.F_FliedName,
	                        t.F_FiledType,
	                        t.F_ColName,
                            t.F_OnlyOne,
	                        t.F_RelationType,
	                        t.F_DataItemEncode,
	                        t.F_Value,
	                        t.F_DbId,
	                        t.F_DbTable,
	                        t.F_DbSaveFlied,
	                        t.F_DbRelationFlied,
	                        t.F_SortCode
                        FROM
	                        System_SetExcelImportFiled t
                        WHERE 1=1
                        
            ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["F_ImportTemplateId"].IsEmpty())
            {
                strSql.Append(" AND t.F_ImportTemplateId = @F_ImportTemplateId ");
                parameter.Add(DbParameters.CreateDbParameter("@F_ImportTemplateId", queryParam["F_ImportTemplateId"].ToString()));
            }
            strSql.Append(" ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public System_SetExcelImportFiledEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, System_SetExcelImportFiledEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
