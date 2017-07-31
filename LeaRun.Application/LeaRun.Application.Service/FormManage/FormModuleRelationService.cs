using LeaRun.Application.Entity.AuthorizeManage;
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
    /// 描 述：表单模块实例表
    /// </summary>
    public class FormModuleRelationService : RepositoryFactory, IFormModuleRelationService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleRelationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            r.F_Id,
	                            r.F_ModuleContentId,
	                            r.F_FrmId,
	                            r.F_FrmVersion,
	                            r.F_FrmKind,
	                            r.F_ObjectId,
	                            r.F_ObjectButtonId,
	                            r.F_ObjectName,
	                            r.F_CreateDate,
	                            r.F_CreateUserId,
	                            r.F_CreateUserName,
	                            r.F_ModifyDate,
	                            r.F_ModifyUserId,
	                            r.F_ModifyUserName,
	                            m.F_FrmName,
	                            m.F_Version AS NewVersion
                            FROM
	                            Form_ModuleRelation r
                            LEFT JOIN Form_Module m ON m.F_FrmId = r.F_FrmId
                            WHERE
	                            1 = 1 ");
                var queryParam = queryJson.ToJObject();
                var parameter = new List<DbParameter>();
                if (!queryParam["F_FrmKind"].IsEmpty())//关联表单模块类型
                {
                    strSql.Append(" AND r.F_FrmKind = @F_FrmKind ");
                    parameter.Add(DbParameters.CreateDbParameter("@F_FrmKind", queryParam["F_FrmKind"].ToString()));
                }
                if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    strSql.Append(" AND r.F_ObjectName = @Keyword ||  m.F_FrmName = @Keyword ");
                    parameter.Add(DbParameters.CreateDbParameter("@Keyword", queryParam["Keyword"].ToString()));
                }
                return this.BaseRepository().FindList<FormModuleRelationEntity>(strSql.ToString(), parameter.ToArray());                
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FormModuleRelationEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FormModuleRelationEntity>(keyValue);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据（关联功能模块菜单）
        /// </summary>
        /// <param name="keyValue">主键Id</param>
        /// <param name="entity">表单关联实体</param>
        /// <param name="moduleEntity">系统功能模板实体</param>
        /// <param name="formModuleContentEntity">表单内容数据</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, FormModuleRelationEntity entity, ModuleEntity moduleEntity, Form_ModuleContentEntity formModuleContentEntity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                var expression = LinqExtensions.True<Form_ModuleContentEntity>();
                expression = expression.And(t => t.F_FrmId == entity.F_FrmId && t.F_FrmVersion == entity.F_FrmVersion);
                Form_ModuleContentEntity form_ModuleContentEntity = db.FindEntity<Form_ModuleContentEntity>(expression);
                if (form_ModuleContentEntity == null)
                {
                    formModuleContentEntity.Create();
                    db.Insert(formModuleContentEntity);
                    entity.F_ModuleContentId = formModuleContentEntity.F_Id;
                }
                else
                {
                    entity.F_ModuleContentId = form_ModuleContentEntity.F_Id;
                }

                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    moduleEntity.Create();
                    db.Insert(moduleEntity);
                    entity.F_ObjectId = moduleEntity.F_ModuleId;
                    entity.F_ObjectName = moduleEntity.F_FullName;
                    moduleEntity.F_UrlAddress = moduleEntity.F_UrlAddress + "?Id=" + entity.F_Id;
                    db.Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    moduleEntity.Modify(entity.F_ObjectId);
                    db.Update(moduleEntity);
                    entity.F_ObjectName = moduleEntity.F_FullName;
                    moduleEntity.F_UrlAddress = moduleEntity.F_UrlAddress + "?Id=" + entity.F_Id;
                    db.Update(entity);
                }

                db.Delete<ModuleButtonEntity>(t => t.F_ModuleId.Equals(moduleEntity.F_ModuleId));
                ModuleButtonEntity moduleButtonrReplace = new ModuleButtonEntity();
                moduleButtonrReplace.F_ModuleId = moduleEntity.F_ModuleId;
                moduleButtonrReplace.F_EnCode = "lr-replace";
                moduleButtonrReplace.F_FullName = "刷新";
                moduleButtonrReplace.F_SortCode = 0;
                moduleButtonrReplace.F_ParentId = "0";
                moduleButtonrReplace.Create();
                db.Insert(moduleButtonrReplace);

                ModuleButtonEntity moduleButtonrAdd = new ModuleButtonEntity();
                moduleButtonrAdd.F_ModuleId = moduleEntity.F_ModuleId;
                moduleButtonrAdd.F_EnCode = "lr-add";
                moduleButtonrAdd.F_FullName = "新增";
                moduleButtonrAdd.F_SortCode = 1;
                moduleButtonrAdd.F_ActionAddress = "/FormManage/FormModule/CustmerFormForm";
                moduleButtonrAdd.F_ParentId = "0";
                moduleButtonrAdd.Create();
                db.Insert(moduleButtonrAdd);

                ModuleButtonEntity moduleButtonrEdit = new ModuleButtonEntity();
                moduleButtonrEdit.F_ModuleId = moduleEntity.F_ModuleId;
                moduleButtonrEdit.F_EnCode = "lr-edit";
                moduleButtonrEdit.F_FullName = "编辑";
                moduleButtonrEdit.F_SortCode = 2;
                moduleButtonrEdit.F_ActionAddress = "/FormManage/FormModule/CustmerFormForm";
                moduleButtonrEdit.F_ParentId = "0";
                moduleButtonrEdit.Create();
                db.Insert(moduleButtonrEdit);

                ModuleButtonEntity moduleButtonrDelete = new ModuleButtonEntity();
                moduleButtonrDelete.F_ModuleId = moduleEntity.F_ModuleId;
                moduleButtonrDelete.F_EnCode = "lr-delete";
                moduleButtonrDelete.F_FullName = "删除";
                moduleButtonrDelete.F_SortCode = 3;
                moduleButtonrDelete.F_ActionAddress = "/FormManage/FormModule/CustmerFormRemove";
                moduleButtonrDelete.F_ParentId = "0";
                moduleButtonrDelete.Create();
                db.Insert(moduleButtonrDelete);

                db.Commit();
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">数据实体</param>
        /// <param name="formModuleContentEntity">表单内容数据</param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, FormModuleRelationEntity entity,Form_ModuleContentEntity formModuleContentEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var expression = LinqExtensions.True<Form_ModuleContentEntity>();
                expression = expression.And(t => t.F_FrmId == entity.F_FrmId &&　t.F_FrmVersion == entity.F_FrmVersion);
                Form_ModuleContentEntity form_ModuleContentEntity = db.FindEntity<Form_ModuleContentEntity>(expression);
                if (form_ModuleContentEntity == null)
                {
                    formModuleContentEntity.Create();
                    db.Insert(formModuleContentEntity);
                    entity.F_ModuleContentId = formModuleContentEntity.F_Id;
                }
                else {
                    entity.F_ModuleContentId = form_ModuleContentEntity.F_Id;
                }
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    db.Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    db.Update(entity);
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
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int RemoveEntity(string keyValue)
        {
            FormModuleRelationEntity entity = this.BaseRepository().FindEntity<FormModuleRelationEntity>(keyValue);
            try
            {
                if (entity != null)
                {
                    if (entity.F_FrmKind == 2)//工作流关联不允许被删除
                    {
                        return 0;
                    }
                    else if (entity.F_FrmKind == 1)//扩展属性
                    {
                        return this.BaseRepository().Delete<FormModuleRelationEntity>(keyValue);
                    }
                    else//自定义表单
                    {
                        var db = this.BaseRepository().BeginTrans();
                        try
                        {
                            string moduleId = entity.F_ObjectId;
                            db.Delete<ModuleButtonEntity>(t => t.F_ModuleId.Equals(moduleId));
                            db.Delete<FormModuleRelationEntity>(keyValue);
                            db.Delete<ModuleEntity>(moduleId);
                            db.Commit();
                            return 1;
                        }
                        catch (Exception)
                        {
                            db.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}
