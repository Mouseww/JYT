
using LeaRun.Application.Entity.SystemManage;
using System.Collections.Generic;
namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：LR0101
    /// 日 期：2016.11.14 17:50
    /// 描 述：文件信息（附件）
    /// </summary>
    public interface IFileAnnexesService
    {
        #region 获取数据
         /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        List<FileAnnexesEntity> GetEntityList(string keyValues);
         /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        FileAnnexesEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SaveEntity(string keyValue, FileAnnexesEntity entity);
         /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteEntity(string keyValue);
        #endregion
    }
}
