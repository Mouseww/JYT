using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.1.7
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2016.10.27 10:45
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例模板内容表操作
    /// </summary>
    public class WFProcessSchemeService:RepositoryFactory, IWFProcessSchemeService
    {
        #region 获取数据
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFProcessSchemeEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFProcessSchemeEntity>(keyValue);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取实体对象根据流程模板Id
        /// </summary>
        /// <param name="schemeId"></param>
        /// <returns></returns>
        public WFProcessSchemeEntity GetEntityBySchemeId(string schemeId)
        {
            try
            {
                var expression = LinqExtensions.True<WFProcessSchemeEntity>();
                expression = expression.And(t => t.F_SchemeInfoId == schemeId);
                return this.BaseRepository().FindEntity<WFProcessSchemeEntity>(expression);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取实体对象根据功能模块Id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public WFProcessSchemeEntity GetEntityByModuleId(string moduleId)
        {
            try
            {
                var expression = LinqExtensions.True<WFProcessSchemeEntity>();
                expression = expression.And(t => t.F_ModuleId == moduleId);
                return this.BaseRepository().FindEntity<WFProcessSchemeEntity>(expression);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string SaveEntity(string keyValue,WFProcessSchemeEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert<WFProcessSchemeEntity>(entity);
                    return entity.F_Id;
                }
                else {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update<WFProcessSchemeEntity>(entity);
                    return keyValue;
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
