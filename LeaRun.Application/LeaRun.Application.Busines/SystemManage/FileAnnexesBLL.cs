using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.11.14 17:50
    /// 描 述：文件信息（附件）
    /// </summary>
    public class FileAnnexesBLL
    {
        private IFileAnnexesService fileAnnexesService = new FileAnnexesService();

        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public List<FileAnnexesEntity> GetEntityList(string keyValues)
        {
            try
            {
                return fileAnnexesService.GetEntityList(keyValues);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FileAnnexesEntity GetEntity(string keyValue)
        {
            try
            {
                return fileAnnexesService.GetEntity(keyValue);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion


        #region 提交数据
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SaveEntity(string keyValue, FileAnnexesEntity entity)
        {
            try
            {
                fileAnnexesService.SaveEntity(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                fileAnnexesService.DeleteEntity(keyValue);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion
    }
}
