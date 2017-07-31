using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Application.IService.FormManage;
using LeaRun.Application.Service.FormManage;
using LeaRun.Util;
using LeaRun.Util.FormModule;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.Busines.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.11.30 13:57
    /// 描 述：表单设计操作类
    /// </summary>
    public class FormModuleBLL
    {
        private IFormModuleService server = new FormModuleService();
        private IFormModuleInstanceService serverInstance = new FormModuleInstanceService();
        private IFormModuleRelationService serverRelation = new FormModuleRelationService();
        private Form_ModuleContentIService serverContent = new Form_ModuleContentService();

        private FormOperation formOperation = new FormOperation();

        #region 表单设计管理

        #region 获取数据
        /// <summary>
        /// 获取表单列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<FormModuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return server.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取表单数据ALL(用于下拉框)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FormModuleEntity> GetEntityList(string queryJson) 
        {
            return server.GetEntityList(queryJson);
        }
        /// <summary>
        /// 获取表单数据根据关联信息
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleEntity> GetEntityListByRelation(string queryJson)
        {
            return server.GetEntityListByRelation(queryJson);
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
                return server.GetEntity(keyValue);
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
                return server.VirtualRemoveEntity(keyValue);
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
                return server.SaveEntity(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region 表单实例操作
        /// <summary>
        /// 根据表单Id获取表单实例列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="relationFormId"></param>
        /// <returns></returns>
        public DataTable GetInstancePageList(Pagination pagination, string relationFormId)
        {
            try
            {
                DataTable dt = null;
                IEnumerable<FormModuleInstanceEntity> datalists = serverInstance.GetInstancePageList(pagination, relationFormId);
                bool isFirst = true;
                foreach (var item in datalists)
                {
                    List<FormInstanceModel> fieldsData = item.F_FrmInstanceJson.ToObject<List<FormInstanceModel>>();
                    if (isFirst)
                    {
                        dt = formOperation.CreateTable(fieldsData);
                        isFirst = false;
                    }
                    DataRow newRow;
                    newRow = dt.NewRow();
                    newRow["leaCustmerFormId"] = item.F_Id;
                    foreach (var filedItem in fieldsData)
                    {
                        newRow[filedItem.field] = filedItem.value;
                    }
                    dt.Rows.Add(newRow);
                }
                return dt;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据processId获取表单实例列表数据
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleInstanceEntity> GetInstanceList(string processId)
        {
            try
            {
                return serverInstance.GetInstanceList(processId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 获取表单模块实例数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FormModuleInstanceEntity GetCustmerInstanceEntity(string keyValue)
        {
            try
            {
                return serverInstance.GetEntity(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单模块实例数据
        /// </summary>
        /// <param name="processId">对应功能模块Id（流程实例Id,系统功能模块Id）</param>
        /// <param name="frmContentId">表单模块Id</param>
        /// <returns></returns>
        public FormModuleInstanceEntity GetCustmerInstanceByProcessId(string processId, string frmContentId)
        {
            try
            {
                return serverInstance.GetEntityByProcessId(processId, frmContentId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单模块实例数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string SaveCustmerInstance(string keyValue, FormModuleInstanceEntity entity)
        {
            try
            {
                Form_ModuleContentEntity contentEntity = serverContent.GetEntity(entity.F_FrmContentId);
                return serverInstance.SaveEntity(keyValue, entity, contentEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 移除自定义表单实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="contentEntity"></param>
        /// <returns></returns>
        public int RemoveCustmerInstanceEntity(string keyValue, string frmContentId)
        {
            try
            {
                Form_ModuleContentEntity contentEntity = serverContent.GetEntity(frmContentId);
                serverInstance.RemoveEntity(keyValue,contentEntity);
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单内容数据
        /// </summary>
        /// <param name="formContentId"></param>
        /// <returns></returns>
        public Form_ModuleContentEntity GetFormContentEntity(string formContentId)
        {
            try
            {
               return serverContent.GetEntity(formContentId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region 表单与模块之间的数据关联
        /// <summary>
        /// 获取分页数据列表(表单与模块关联列表)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<FormModuleRelationEntity> GetRelationPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return serverRelation.GetPageList(pagination,queryJson);
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        /// <summary>
        /// 获取表单与模块关联的实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FormModuleRelationEntity GetRelationEntity(string keyValue) {
            try
            {
                return serverRelation.GetEntity(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单与模块关联的实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="moduleEntity"></param>
        public void SaveRelationEntity(string keyValue, FormModuleRelationEntity entity, ModuleEntity moduleEntity = null)
        {
            try
            {
                FormModuleEntity formModuleEntity = server.GetEntity(entity.F_FrmId);
                Form_ModuleContentEntity formModuleContentEntity = new Form_ModuleContentEntity()
                {
                    F_FrmContent = formModuleEntity.F_FrmContent,
                    F_FrmId = formModuleEntity.F_FrmId,
                    F_FrmVersion = formModuleEntity.F_Version
                };

                if (entity.F_FrmKind == 0)//自定义表单
                {
                    serverRelation.SaveEntity(keyValue, entity, moduleEntity, formModuleContentEntity);
                }
                else
                {
                    serverRelation.SaveEntity(keyValue, entity, formModuleContentEntity);    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除表单关联
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int RelationRemove(string keyValue)
        {
            try
            {
                return serverRelation.RemoveEntity(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 升级表单
        /// </summary>
        /// <param name="keyValue"></param>
        public void UpdateForm(string keyValue)
        {
            try
            {
                FormModuleRelationEntity relationEntity = serverRelation.GetEntity(keyValue);
                FormModuleEntity formModuleEntity = server.GetEntity(relationEntity.F_FrmId);
                Form_ModuleContentEntity formModuleContentEntity = new Form_ModuleContentEntity()
                {
                    F_FrmContent = formModuleEntity.F_FrmContent,
                    F_FrmId = formModuleEntity.F_FrmId,
                    F_FrmVersion = formModuleEntity.F_Version
                };
                relationEntity.F_FrmVersion = formModuleEntity.F_Version;
                serverRelation.SaveEntity(keyValue, relationEntity, formModuleContentEntity);    
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取表单内容
        /// </summary>
        /// <param name="keyValue"></param>
        public Form_ModuleContentEntity GetFormContentJson(string keyValue)
        {
            try
            {
                FormModuleRelationEntity relationEntity = serverRelation.GetEntity(keyValue);
                Form_ModuleContentEntity contentEntity = serverContent.GetEntity(relationEntity.F_ModuleContentId);
                return contentEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
