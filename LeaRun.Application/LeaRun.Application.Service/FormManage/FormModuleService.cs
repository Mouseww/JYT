using LeaRun.Application.Entity.FormManage;
using LeaRun.Application.IService.FormManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.01.14 11:02
    /// 描 述：表单模块表
    /// </summary>
    public class FormModuleService : RepositoryFactory, IFormModuleService
    {
        #region 获取数据
        /// <summary>
        /// 获取表单列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<FormModuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var expression = LinqExtensions.True<FormModuleEntity>();
                expression = expression.And(t => t.F_DeleteMark == 0);
                if (!queryParam["F_FrmCategory"].IsEmpty())//分类查询
                {
                    string f_FrmCategory = queryParam["F_FrmCategory"].ToString();
                    expression = expression.And(t => t.F_FrmCategory == f_FrmCategory);
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyword = queryParam["Keyword"].ToString();
                    expression = expression.And(t => t.F_FrmCode.Contains(keyword) || t.F_FrmName.Contains(keyword));
                }

                if (!queryParam["F_FrmType"].IsEmpty())//类型查询（系统表单，自定义表单，自定义表单建表）
                {
                    int f_FrmType = Convert.ToInt32(queryParam["F_FrmType"].ToString());
                    expression = expression.And(t => t.F_FrmType == f_FrmType);
                }
                else if(!queryParam["F_FrmType2"].IsEmpty())//不等于某种类型
                {

                    int f_FrmType2 = Convert.ToInt32(queryParam["F_FrmType2"].ToString());
                    expression = expression.And(t => t.F_FrmType != f_FrmType2);
                }


                return this.BaseRepository().FindList<FormModuleEntity>(expression, pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单列表,不分页
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleEntity> GetEntityList(string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var expression = LinqExtensions.True<FormModuleEntity>();
                expression = expression.And(t => t.F_DeleteMark == 0);
                if (!queryParam["F_FrmCategory"].IsEmpty())//分类查询
                {
                    string f_FrmCategory = queryParam["F_FrmCategory"].ToString();
                    expression = expression.And(t => t.F_FrmCategory == f_FrmCategory);
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyword = queryParam["F_FrmCategory"].ToString();
                    expression = expression.And(t => t.F_FrmCode.Contains(keyword) || t.F_FrmName.Contains(keyword));
                }

                if (!queryParam["F_FrmType"].IsEmpty())//类型查询（系统表单，自定义表单，自定义表单建表）
                {
                    int f_FrmType = Convert.ToInt32(queryParam["F_FrmType"].ToString());
                    expression = expression.And(t => t.F_FrmType == f_FrmType);
                }
                else if (!queryParam["F_FrmType2"].IsEmpty())//不等于某种类型
                {
                    int f_FrmType2 = Convert.ToInt32(queryParam["F_FrmType2"].ToString());
                    expression = expression.And(t => t.F_FrmType != f_FrmType2);
                }

                return this.BaseRepository().FindList<FormModuleEntity>(expression);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单数据根据关联信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleEntity> GetEntityListByRelation(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
                                r.F_ObjectId,
	                            m.F_FrmId,
	                            m.F_FrmCode,
	                            m.F_FrmName,
	                            m.F_FrmCategory,
	                            m.F_FrmType,
	                            m.F_FrmDbId,
	                            m.F_FrmDbTable,
	                            m.F_FrmDbPkey,
	                            m.F_FrmContent,
	                            m.F_SortCode,
	                            m.F_DeleteMark,
	                            m.F_EnabledMark,
	                            m.F_Description,
                                m.F_Version,
	                            m.F_CreateDate,
	                            m.F_CreateUserId,
	                            m.F_CreateUserName,
	                            m.F_ModifyDate,
	                            m.F_ModifyUserId,
	                            m.F_ModifyUserName
                            FROM
	                            Form_ModuleRelation r
                            LEFT JOIN Form_Module m ON r.F_FrmId = m.F_FrmId
                            WHERE 1=1 AND m.F_DeleteMark = '0' ");
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                if (!queryParam["F_ObjectId"].IsEmpty())
                {
                    strSql.Append(" AND r.F_ObjectId = @F_ObjectId ");
                    parameter.Add(DbParameters.CreateDbParameter("@F_ObjectId", queryParam["F_ObjectId"].ToString()));
                }
                if (!queryParam["F_FrmKind"].IsEmpty())
                {
                    strSql.Append(" AND r.F_FrmKind = @F_FrmKind ");
                    parameter.Add(DbParameters.CreateDbParameter("@F_FrmKind", queryParam["F_FrmKind"].ToString()));
                }
                return this.BaseRepository().FindList<FormModuleEntity>(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取表单模块实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FormModuleEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FormModuleEntity>(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除表单模块数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public int VirtualRemoveEntity(string keyValue)
        {
            try
            {
                FormModuleEntity entity = new FormModuleEntity();
                entity.Modify(keyValue);
                entity.F_DeleteMark = 1;
                return this.BaseRepository().Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="entity">表单模板实体类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, FormModuleEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.F_Version = "V" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    entity.Create();
                    return this.BaseRepository().Insert(entity);
                }
                else
                {

                    if (entity.F_EnabledMark == 1 && !string.IsNullOrEmpty(entity.F_FrmContent))
                    {
                        FormModuleEntity one = this.BaseRepository().FindEntity<FormModuleEntity>(keyValue);
                        if (one.F_FrmContent != entity.F_FrmContent)//如果和原来的表单数据不一样产生的版本号
                        {
                            entity.F_Version = "V" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        }
                    }
                    entity.Modify(keyValue);
                    return this.BaseRepository().Update(entity);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
