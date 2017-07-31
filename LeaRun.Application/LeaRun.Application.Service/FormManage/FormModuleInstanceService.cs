using LeaRun.Application.Entity.FormManage;
using LeaRun.Application.IService.FormManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.FormModule;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace LeaRun.Application.Service.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.01.14 11:02
    /// 描 述：表单模块实例表
    /// </summary>
    public class FormModuleInstanceService : RepositoryFactory, IFormModuleInstanceService
    {
        #region 获取数据
        /// <summary>
        /// 根据表单Id获取表单实例列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleInstanceEntity> GetInstancePageList(Pagination pagination, string Id)
        {
            try
            {
                var expression = LinqExtensions.True<FormModuleInstanceEntity>();
                expression = expression.And(t => t.F_ObjectId == Id);
                return this.BaseRepository().FindList(expression, pagination);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据表单Id获取表单实例列表数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleInstanceEntity> GetInstanceList(string processId)
        {
            try
            {
                var expression = LinqExtensions.True<FormModuleInstanceEntity>();
                expression = expression.And(t => t.F_ObjectId == processId);
                return this.BaseRepository().FindList(expression);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单模块实例数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FormModuleInstanceEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FormModuleInstanceEntity>(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单模块实例数据
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="frmContentId"></param>
        /// <returns></returns>
        public FormModuleInstanceEntity GetEntityByProcessId(string processId, string frmContentId)
        {
            try
            {
                var expression = LinqExtensions.True<FormModuleInstanceEntity>();
                expression = expression.And(t => t.F_ObjectId == processId && t.F_FrmContentId == frmContentId);
                return this.BaseRepository().FindEntity<FormModuleInstanceEntity>(expression);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单模块实例数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="contentEntity"></param>
        /// <returns>主键值</returns>
        public string SaveEntity(string keyValue, FormModuleInstanceEntity entity, Form_ModuleContentEntity contentEntity)
        {
            try
            {
                string resKeyValue = "";

                FormContentModel contentModel = contentEntity.F_FrmContent.ToObject<FormContentModel>();

                switch (contentModel.type)
                {
                    case "1":
                        DataBaseLinkService dbService = new DataBaseLinkService();
                        string strSql = "";
                        var parameter = new List<DbParameter>();
                        List<FormInstanceModel> filedsData = entity.F_FrmInstanceJson.ToObject<List<FormInstanceModel>>();

                        if (string.IsNullOrEmpty(keyValue))
                        {
                            string pkValue = Guid.NewGuid().ToString();
                            strSql = " INSERT INTO " + contentModel.dbTable + " ( " + contentModel.dbPkey;
                            string sqlValue = "( @" + contentModel.dbPkey;
                            parameter.Add(DbParameters.CreateDbParameter("@" + contentModel.dbPkey, pkValue));
                            foreach (var item in filedsData)
                            {
                                if (!string.IsNullOrEmpty(item.value))
                                {
                                    strSql += "," + item.field;
                                    sqlValue += ",@" + item.field;
                                    parameter.Add(DbParameters.CreateDbParameter("@" + item.field, item.value));
                                }
                            }
                            strSql += " ) VALUES " + sqlValue + ")";
                            entity.Create();
                            entity.F_Id = pkValue;
                            dbService.ExecuteBySql(contentModel.dbId, strSql, parameter.ToArray());
                            this.BaseRepository().Insert(entity);

                            resKeyValue = entity.F_Id;
                        }
                        else
                        {
                            strSql = " UPDATE   " + contentModel.dbTable + " SET  ";
                            bool isFirst = true;
                            foreach (var item in filedsData)
                            {
                                if (!string.IsNullOrEmpty(item.value))
                                {
                                    if (!isFirst) { strSql += ","; }
                                    strSql += item.field + "=@" + item.field;
                                    parameter.Add(DbParameters.CreateDbParameter("@" + item.field, item.value));
                                    isFirst = false;
                                }
                            }
                            strSql += " WHERE " + contentModel.dbPkey + "=@" + contentModel.dbPkey;
                            parameter.Add(DbParameters.CreateDbParameter("@" + contentModel.dbPkey, keyValue));

                            entity.Modify(keyValue);
                            dbService.ExecuteBySql(contentModel.dbId, strSql, parameter.ToArray());
                            this.BaseRepository().Update(entity);

                            resKeyValue = entity.F_Id;
                        }
                        break;
                    case "0":
                    case "2":
                        if (string.IsNullOrEmpty(keyValue))//新增
                        {
                            entity.Create();
                            this.BaseRepository().Insert(entity);
                        }
                        else//更新
                        {
                            entity.Modify(keyValue);
                            this.BaseRepository().Update(entity);
                        }
                        resKeyValue = entity.F_Id;
                    break;
                }
                return resKeyValue;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 保存表单模块实例数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="contentEntity"></param>
        /// <returns></returns>
        public string SaveEntity(FormModuleInstanceEntity entity, Form_ModuleContentEntity contentEntity)
        {
            FormModuleInstanceEntity formModuleInstanceEntity = GetEntityByProcessId(entity.F_ObjectId, entity.F_FrmContentId);
            if (formModuleInstanceEntity != null)
            {
                if (formModuleInstanceEntity.F_FrmInstanceJson != entity.F_FrmInstanceJson)
                {
                    return SaveEntity(formModuleInstanceEntity.F_Id, entity, contentEntity);
                }
                else
                {
                    return formModuleInstanceEntity.F_Id;
                }
            }
            else
            {
                return SaveEntity("", entity, contentEntity);
            }
        }

        /// <summary>
        /// 移除自定义表单实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="contentEntity"></param>
        /// <returns></returns>
        public int RemoveEntity(string keyValue, Form_ModuleContentEntity contentEntity)
        {
            try
            {
                FormContentModel contentModel = contentEntity.F_FrmContent.ToObject<FormContentModel>();

                if (contentModel.type == "1")
                {
                    DataBaseLinkService dbService = new DataBaseLinkService();
                    string strSql = "DELETE FROM " + contentModel.dbTable + " WHERE " + contentModel.dbPkey + " = @" + contentModel.dbPkey;
                    var parameter = new List<DbParameter>();
                    parameter.Add(DbParameters.CreateDbParameter("@" + contentModel.dbPkey, keyValue));
                    dbService.ExecuteBySql(contentModel.dbId, strSql, parameter.ToArray());
                }
                return this.BaseRepository().Delete<FormModuleInstanceEntity>(keyValue);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}
