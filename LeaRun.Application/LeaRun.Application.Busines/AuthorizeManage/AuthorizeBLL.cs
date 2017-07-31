using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.AuthorizeManage;
using LeaRun.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeBLL
    {
        private IAuthorizeService service = new AuthorizeService();
        private ModuleBLL moduleBLL = new ModuleBLL();
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();
        /// <summary>
        /// 获取所有功能模块数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetALLModuleList()
        {
            return moduleBLL.GetList().FindAll(t => t.F_EnabledMark.Equals(1));
        }
        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleBLL.GetList().FindAll(t => t.F_EnabledMark.Equals(1));
            }
            else
            {
                return service.GetModuleList(userId);
            }
        }
        /// <summary>
        /// (获取授权功能)对没有按钮的模块做过滤
        /// </summary>
        /// <param name="moduleEntity"></param>
        /// <param name="moduleButtonEntity"></param>
        /// <returns></returns>
        public List<ModuleEntity> GetModuleListByModuleButton(string userId, IEnumerable<ModuleButtonEntity> moduleButtonEntity)
        {
            IEnumerable<ModuleEntity> moduleEntity = null;
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                moduleEntity = moduleBLL.GetList().FindAll(t => t.F_EnabledMark.Equals(1));
            }
            else
            {
                moduleEntity = service.GetModuleList(userId);
            }
            List<ModuleEntity> moduleData = new List<ModuleEntity>();
            List<ModuleEntity> moduleData2 = new List<ModuleEntity>();
            foreach (var item in moduleEntity)
            {
                if (item.F_IsMenu == 1)
                {
                    bool hasChildren = moduleButtonEntity.Count(t => t.F_ModuleId == item.F_ModuleId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                moduleData.Add(item);
            }
            foreach (var item in moduleData)
            {
                if (item.F_IsMenu != 1)
                {
                    bool hasChildren = moduleData.Count(t => t.F_ParentId == item.F_ModuleId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                moduleData2.Add(item);
            }
            moduleData = moduleData2;
            moduleData2 = new List<ModuleEntity>();
            foreach (var item in moduleData)
            {
                if (item.F_IsMenu != 1)
                {
                    bool hasChildren = moduleData.Count(t => t.F_ParentId == item.F_ModuleId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                moduleData2.Add(item);
            }
            moduleData = moduleData2;
            return moduleData;
        }
        /// <summary>
        /// 获取所有功能按钮
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetALLModuleButtonList()
        {
            return moduleButtonBLL.GetList();
        }
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleButtonBLL.GetList();
            }
            else
            {
                return service.GetModuleButtonList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleColumnBLL.GetList();
            }
            else
            {
                return service.GetModuleColumnList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            return service.GetUrlList(userId);
        }
        /// <summary>
        /// Action执行权限认证
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="action">请求地址</param>
        /// <returns></returns>
        public bool ActionAuthorize(string userId, string moduleId, string action)
        {
            List<AuthorizeUrlModel> authorizeUrlList = new List<AuthorizeUrlModel>();
            var cacheList = CacheFactory.Cache().GetCache<List<AuthorizeUrlModel>>("AuthorizeUrl_" + userId);
            if (cacheList == null)
            {
                authorizeUrlList = this.GetUrlList(userId).ToList();
                CacheFactory.Cache().WriteCache(authorizeUrlList, "AuthorizeUrl_" + userId, DateTime.Now.AddMinutes(1));
            }
            else
            {
                authorizeUrlList = cacheList;
            }
            authorizeUrlList = authorizeUrlList.FindAll(t => t.F_ModuleId.Equals(moduleId));
            foreach (AuthorizeUrlModel item in authorizeUrlList)
            {
                if (!string.IsNullOrEmpty(item.F_UrlAddress))
                {
                    string[] url = item.F_UrlAddress.Split('?');
                    if (item.F_ModuleId == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthorUserId(operators, isWrite);
        }
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthor(operators, isWrite);
        } 
    }
}
