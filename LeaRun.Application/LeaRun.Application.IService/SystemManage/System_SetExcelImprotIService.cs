using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：陈小二
    /// 日 期：2016-12-04 14:46
    /// 描 述：System_SetExcelImprot
    /// </summary>
    public interface System_SetExcelImprotIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<System_SetExcelImprotEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<System_SetExcelImprotEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        System_SetExcelImprotEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, System_SetExcelImprotEntity entity, List<System_SetExcelImportFiledEntity> filedList);
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        void UpdateForm(string keyValue, System_SetExcelImprotEntity entity);
        #endregion

        #region 扩展操作
        /// <summary>
        /// 执行excel模板数据导入
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        DataTable ExcelImport(string keyValue, DataTable dt);
        #endregion
    }
}
