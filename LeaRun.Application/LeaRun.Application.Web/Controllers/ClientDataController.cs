using LeaRun.Application.Cache;
using LeaRun.Application.Entity.SystemManage.ViewModel;
using LeaRun.Util;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Busines.FormManage;
using LeaRun.Application.Entity.FormManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Entity.SystemManage;

namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.09.01 13:32
    /// 描 述：客户端数据
    /// </summary>
    public class ClientDataController : MvcControllerBase
    {
        private DataItemCache dataItemCache = new DataItemCache();
        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentCache departmentCache = new DepartmentCache();
        private PostCache postCache = new PostCache();
        private RoleCache roleCache = new RoleCache();
        private UserGroupCache userGroupCache = new UserGroupCache();
        private UserCache userCache = new UserCache();
        private AuthorizeBLL authorizeBLL = new AuthorizeBLL();
        private FormModuleBLL fromModuleBLL = new FormModuleBLL();
        private System_SetExcelImprotBLL excelBll = new System_SetExcelImprotBLL();
        private System_SetExcelExportBLL excelBll2 = new System_SetExcelExportBLL();

        #region 获取数据
        /// <summary>
        /// 批量加载数据给客户端（把常用数据全部加载到浏览器中 这样能够减少数据库交互）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetClientDataJson()
        {
            var jsonData = new
            {
                organize = this.GetOrganizeData(),                              //公司
                department = this.GetDepartmentData(),                          //部门
                post = this.GetPostData(),                                      //岗位
                role = this.GetRoleData(),                                      //角色
                userGroup = this.GetUserGroupData(),                            //用户组
                user = this.GetUserData(),                                      //用户
                authorizeMenu = this.GetModuleData(),                           //导航菜单
                authorizeButton = this.GetModuleButtonData(),                   //功能按钮
                authorizeColumn = this.GetModuleColumnData(),                   //功能视图
                menu = this.GetALLModuleData(),                                 //所有功能
                button = this.GetALLModuleButtonData(),                         //所有功能按钮
                dataItem = this.GetDataItem(),                                  //字典
                //formPropertyExpansion = this.GetEntityListByRelation(),         //表单扩展属性
                excelImportTemplate = this.GetExcelImportModuleList(),          //excel导入模板
                excelExportTemplate = this.GetExcelExportModuleList()           //excel导出模板
            };
            return ToJsonResult(jsonData);
        }
        #endregion

        #region 处理基础数据
        /// <summary>
        /// 获取公司数据
        /// </summary>
        /// <returns></returns>
        private object GetOrganizeData()
        {
            var data = organizeCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (OrganizeEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.F_EnCode,
                    FullName = item.F_FullName
                };
                dictionary.Add(item.F_OrganizeId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <returns></returns>
        private object GetDepartmentData()
        {
            var data = departmentCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (DepartmentEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.F_EnCode,
                    FullName = item.F_FullName,
                    OrganizeId = item.F_OrganizeId
                };
                dictionary.Add(item.F_DepartmentId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetUserGroupData()
        {
            var data = userGroupCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.F_EnCode,
                    FullName = item.F_FullName
                };
                dictionary.Add(item.F_RoleId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取岗位数据
        /// </summary>
        /// <returns></returns>
        private object GetPostData()
        {
            var data = postCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.F_EnCode,
                    FullName = item.F_FullName
                };
                dictionary.Add(item.F_RoleId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <returns></returns>
        private object GetRoleData()
        {
            var data = roleCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (RoleEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.F_EnCode,
                    FullName = item.F_FullName
                };
                dictionary.Add(item.F_RoleId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        private object GetUserData()
        {
            var data = userCache.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (UserEntity item in data)
            {
                var fieldItem = new
                {
                    EnCode = item.F_EnCode,
                    Account = item.F_Account,
                    RealName = item.F_RealName,
                    OrganizeId = item.F_OrganizeId,
                    DepartmentId = item.F_DepartmentId
                };
                dictionary.Add(item.F_UserId, fieldItem);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <returns></returns>
        private object GetDataItem()
        {
            var dataList = dataItemCache.GetDataItemList().Where(t => !string.IsNullOrEmpty(t.F_EnCode)); ;
            var dataSort = dataList.Distinct(new Comparint<DataItemModel>("F_EnCode"));
            Dictionary<string, object> dictionarySort = new Dictionary<string, object>();
            foreach (DataItemModel itemSort in dataSort)
            {
                var dataItemList = dataList.Where(t => t.F_EnCode.Equals(itemSort.F_EnCode));
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (DataItemModel itemList in dataItemList)
                {
                    if (!dictionaryItemList.ContainsKey(itemList.F_ItemValue))
                    {
                        dictionaryItemList.Add(itemList.F_ItemValue, itemList.F_ItemName);
                    }
                }
                foreach (DataItemModel itemList in dataItemList)
                {
                    if (!dictionaryItemList.ContainsKey(itemList.F_ItemDetailId))
                    {
                        dictionaryItemList.Add(itemList.F_ItemDetailId, itemList.F_ItemName);
                    }
                }
                dictionarySort.Add(itemSort.F_EnCode, dictionaryItemList);
            }
            return dictionarySort;
        }
        /// <summary>
        /// 获取所有功能数据
        /// </summary>
        /// <returns></returns>
        private object GetALLModuleData()
        {
            var data = authorizeBLL.GetALLModuleList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var item in data)
            {
                dictionary.Add(item.F_ModuleId, item);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取功能按钮数据
        /// </summary>
        /// <returns></returns>
        private object GetALLModuleButtonData()
        {
            var data = authorizeBLL.GetALLModuleButtonList();
            var dataModule = data.Distinct(new Comparint<ModuleButtonEntity>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModule)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.Add(item.F_ModuleId, buttonList);
            }
            return dictionary;
        }
        #endregion

        #region 处理授权数据
        /// <summary>
        /// 获取功能数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleData()
        {
            return authorizeBLL.GetModuleList(SystemInfo.CurrentUserId);
        }
        /// <summary>
        /// 获取功能按钮数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleButtonData()
        {
            var data = authorizeBLL.GetModuleButtonList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleButtonEntity>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleButtonEntity item in dataModule)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.Add(item.F_ModuleId, buttonList);
            }
            return dictionary;
        }
        /// <summary>
        /// 获取功能视图数据
        /// </summary>
        /// <returns></returns>
        private object GetModuleColumnData()
        {
            var data = authorizeBLL.GetModuleColumnList(SystemInfo.CurrentUserId);
            var dataModule = data.Distinct(new Comparint<ModuleColumnEntity>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (ModuleColumnEntity item in dataModule)
            {
                var columnList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.Add(item.F_ModuleId, columnList);
            }
            return dictionary;
        }
        #endregion

        #region 扩展属性数据
        /// <summary>
        /// 获取扩展属性表单的对应模块数据
        /// </summary>
        /// <returns></returns>
        private object GetEntityListByRelation()
        {
            var data = fromModuleBLL.GetEntityListByRelation("{\"F_FrmKind\":\"1\"}");
            Dictionary<string, List<FormModuleEntity>> dictionary = new Dictionary<string, List<FormModuleEntity>>();
            foreach (var item in data)
            {
                if (!dictionary.ContainsKey(item.F_ObjectId))
                {
                    List<FormModuleEntity> list = new List<FormModuleEntity>();
                    list.Add(item);
                    dictionary.Add(item.F_ObjectId, list);
                }
                else
                {
                    dictionary[item.F_ObjectId].Add(item);
                }
            }
            return dictionary;
        }
        #endregion

        #region Excel导入导出
        /// <summary>
        /// 获取模块绑定的导入模板
        /// </summary>
        /// <returns></returns>
        private object GetExcelImportModuleList()
        {
            var data = excelBll.GetList("{\"F_EnabledMark\":\"1\"}");
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (var item in data)
            {
                if (!dictionary.ContainsKey(item.F_ModuleId))
                {
                    List<string> list = new List<string>();
                    list.Add(item.F_Id);
                    dictionary.Add(item.F_ModuleId, list);
                }
                else
                {
                    dictionary[item.F_ModuleId].Add(item.F_Id);
                }
            }
            return dictionary;
        }
        /// <summary>
        /// 获取模块绑定的导出模板
        /// </summary>
        /// <returns></returns>
        private object GetExcelExportModuleList()
        {
            var data = excelBll2.GetList("{\"F_EnabledMark\":\"1\"}");
            Dictionary<string, List<object>> dictionary = new Dictionary<string, List<object>>();
            foreach (var item in data)
            {
                if (!dictionary.ContainsKey(item.F_ModuleId))
                {
                    List<object> list = new List<object>();
                    list.Add(item);
                    dictionary.Add(item.F_ModuleId, list);
                }
                else
                {
                    dictionary[item.F_ModuleId].Add(item);
                }
            }
            return dictionary;
        }
        #endregion
    }
}
