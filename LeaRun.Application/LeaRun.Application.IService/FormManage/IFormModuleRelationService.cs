using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.11.29 14:50
    /// 描 述：表单模块关联表接口
    /// </summary>
    public interface IFormModuleRelationService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<FormModuleRelationEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FormModuleRelationEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">数据实体</param>
        /// <param name="formModuleContentEntity">表单内容数据</param>
        /// <returns></returns>
        int SaveEntity(string keyValue, FormModuleRelationEntity entity, Form_ModuleContentEntity formModuleContentEntity);
         /// <summary>
        /// 保存实体数据（关联功能模块菜单）
        /// </summary>
        /// <param name="keyValue">主键Id</param>
        /// <param name="entity">表单关联实体</param>
        /// <param name="moduleEntity">系统功能模板实体</param>
        /// <param name="formModuleContentEntity">表单内容数据</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, FormModuleRelationEntity entity, ModuleEntity moduleEntity, Form_ModuleContentEntity formModuleContentEntity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        int RemoveEntity(string keyValue);
        #endregion
    }
}
