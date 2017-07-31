using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Data.Common;
using LeaRun.Data;
using System.Text;
using System;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 Learun ADMS6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：陈小二
    /// 日 期：2016-12-07 15:32
    /// 描 述：System_SetExcelExport
    /// </summary>
    public class System_SetExcelExportService : RepositoryFactory<System_SetExcelExportEntity>, System_SetExcelExportIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<System_SetExcelExportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            var expression = LinqExtensions.True<System_SetExcelExportEntity>();
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
        public IEnumerable<System_SetExcelExportEntity> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                            t.F_Id,
	                            t.F_Name,
	                            t.F_GridId,
	                            t.F_ModuleId,
	                            t.F_ModuleBtnId,
	                            t.F_EnabledMark,
	                            t.F_CreateDate,
	                            t.F_CreateUserId,
	                            t.F_CreateUserName,
	                            t.F_ModifyDate,
	                            t.F_ModifyUserId,
	                            t.F_ModifyUserName
                            FROM
	                            System_SetExcelExport t
                            WHERE
	                            1 = 1
            ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["F_EnabledMark"].IsEmpty())
            {
                strSql.Append(" AND t.F_EnabledMark = @F_EnabledMark ");
                parameter.Add(DbParameters.CreateDbParameter("@F_EnabledMark", queryParam["F_EnabledMark"].ToString()));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public System_SetExcelExportEntity GetEntity(string keyValue)
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
        public void SaveEntity(string keyValue, System_SetExcelExportEntity entity)
        {
            var expression = LinqExtensions.True<System_SetExcelExportEntity>();


            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_ModuleBtnId == entity.F_ModuleBtnId && t.F_Id == keyValue);
                System_SetExcelExportEntity data = this.BaseRepository().FindEntity(expression);
                if (data != null)
                {
                    throw new Exception("一个按钮只能绑定一次");
                }
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                expression = expression.And(t => t.F_ModuleBtnId == entity.F_ModuleBtnId);
                System_SetExcelExportEntity data = this.BaseRepository().FindEntity(expression);
                if (data != null)
                {
                    throw new Exception("一个按钮只能绑定一次");
                }
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public void UpdateForm(string keyValue, System_SetExcelExportEntity entity)
        {
            try
            {
                var expression = LinqExtensions.True<System_SetExcelExportEntity>();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.F_ModuleBtnId == entity.F_ModuleBtnId && t.F_Id == keyValue);
                    System_SetExcelExportEntity data = this.BaseRepository().FindEntity(expression);
                    if (data != null)
                    {
                        throw new Exception("一个按钮只能绑定一次");
                    }
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
    }
}
