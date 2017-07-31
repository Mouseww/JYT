using LeaRun.Application.Entity.AppManage;
using LeaRun.Application.IService.AppManage;
using LeaRun.Application.Service.AppManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using LeaRun.CodeGenerator.Template;
using LeaRun.Util;

namespace LeaRun.Application.Busines.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：杨海勇
    /// 日 期：2016-07-19 10:13
    /// 描 述：App_Project
    /// </summary>
    public class AppProjectBLL
    {
        private AppProjectIService service = new AppProjectService();
        private WebAppTemplate webappTemplate = new WebAppTemplate();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<AppProjectEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AppProjectEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AppProjectModel entity)
        {
            try
            {
                AppProjectEntity appProjectEntity = new AppProjectEntity();
                appProjectEntity.F_Name = entity.F_Name;
                appProjectEntity.F_Icon = entity.F_Icon;
                appProjectEntity.F_IsTabed = entity.F_IsTabed;
                service.SaveProject(keyValue, appProjectEntity, entity.F_Templates);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 下载app设计
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public string DownForm(string keyValue, AppProjectModel entity, string outputDirectory)
        {
            try
            {
                AppProjectEntity appProjectEntity = new AppProjectEntity();
                appProjectEntity.F_Name = entity.F_Name;
                appProjectEntity.F_Icon = entity.F_Icon;
                appProjectEntity.F_IsTabed = entity.F_IsTabed;
                //service.SaveProject(keyValue, appProjectEntity, entity.F_Templates);

                return webappTemplate.AppBuilder(entity.F_Templates, appProjectEntity, outputDirectory);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="path"></param>
        public void DownFile(string path)
        {
            try
            {
                FileDownHelper.DownLoadnew(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}