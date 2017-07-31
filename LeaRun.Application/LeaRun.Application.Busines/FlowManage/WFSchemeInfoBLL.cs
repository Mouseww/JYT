using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.Busines.FormManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：LR0101 2106.10.25 14:10
    /// 日 期：2016.03.19 13:57
    /// 描 述：工作流流程模板操作（支持：SqlServer）
    /// </summary>
    public class WFSchemeInfoBLL
    {
        private IWFSchemeInfoService infoserver = new WFSchemeInfoService();
        #region 获取数据
        /// <summary>
        /// 获取流程列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            return infoserver.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取流程列表数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetList(string queryJson)
        {
            return infoserver.GetList(queryJson);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFSchemeInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return infoserver.GetEntity(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取权限列表数据
        /// </summary>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        public IEnumerable<WFSchemeInfoAuthorizeEntity> GetAuthorizeEntityList(string schemeInfoId)
        {
            return infoserver.GetAuthorizeEntityList(schemeInfoId);
        }


        /// <summary>
        /// 获取所有表单数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            return infoserver.GetList();
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程设计（保存，编辑）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <param name="modelentity"></param>
        /// <param name="shcemeAuthorizeData"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, WFSchemeInfoEntity entity, string[] shcemeAuthorizeData)
        {
            try
            {
                return infoserver.SaveForm(keyValue,entity,shcemeAuthorizeData);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除流程设计
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {

            try
            {
                infoserver.DeleteEntity(keyValue);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// 更新流程模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                infoserver.UpdateState(keyValue, state);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
