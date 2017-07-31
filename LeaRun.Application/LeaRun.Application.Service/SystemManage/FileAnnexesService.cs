using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.11.14 17:50
    /// 描 述：文件信息（附件）
    /// </summary>
    public class FileAnnexesService : RepositoryFactory<FileAnnexesEntity>, IFileAnnexesService
    {

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
                var parameter = Expression.Parameter(typeof(FileAnnexesEntity), "t");
                var conditon = Expression.Constant(keyValues).Call("Contains", parameter.Property("F_Id"));
                var lambda = conditon.ToLambda<Func<FileAnnexesEntity, bool>>(parameter);
                return this.BaseRepository().IQueryable(lambda).ToList();
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
                return this.BaseRepository().FindEntity(keyValue);
            }
            catch (Exception)
            {
                throw;
            }         
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SaveEntity(string keyValue, FileAnnexesEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
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
                this.BaseRepository().Delete(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        
    }
}
