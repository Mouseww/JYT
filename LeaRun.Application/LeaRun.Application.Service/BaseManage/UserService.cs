using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.AuthorizeManage;
using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class UserService : RepositoryFactory<UserEntity>, IUserService
    {
        private IAuthorizeService<UserEntity> iauthorizeservice = new AuthorizeService<UserEntity>();

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.*,
                                    d.F_FullName AS F_DepartmentName 
                            FROM    Base_User u
                                    LEFT JOIN Base_Department d ON d.F_DepartmentId = u.F_DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND u.F_UserId <> 'System' AND u.F_EnabledMark = 1 AND u.F_DeleteMark=0");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList()
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.F_EnabledMark == 1).And(t => t.F_DeleteMark == 0);//.And(t => t.F_UserId != "System")
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.F_CreateDate).ToList();
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<UserEntity>();
            var queryParam = queryJson.ToJObject();
            //公司主键
            if (!queryParam["organizeId"].IsEmpty())
            {
                string organizeId = queryParam["organizeId"].ToString();
                expression = expression.And(t => t.F_OrganizeId.Equals(organizeId));
            }
            //部门主键
            if (!queryParam["departmentId"].IsEmpty())
            {
                string departmentId = queryParam["departmentId"].ToString();
                expression = expression.And(t => t.F_DepartmentId.Equals(departmentId));
            }
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Account":            //账户
                        expression = expression.And(t => t.F_Account.Contains(keyord));
                        break;
                    case "RealName":          //姓名
                        expression = expression.And(t => t.F_RealName.Contains(keyord));
                        break;
                    case "Mobile":          //手机
                        expression = expression.And(t => t.F_Mobile.Contains(keyord));
                        break;
                    default:
                        break;
                }
            }
            //expression = expression.And(t => t.UserId != "System");
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.F_UserId ,
                                    u.F_EnCode ,
                                    u.F_Account ,
                                    u.F_RealName ,
                                    u.F_Gender ,
                                    u.F_Birthday ,
                                    u.F_Mobile ,
                                    u.F_Manager ,
                                    u.F_OrganizeId,
                                    u.F_DepartmentId,
                                    o.F_FullName AS F_OrganizeName ,
                                    d.F_FullName AS F_DepartmentName ,
                                    u.F_RoleId ,
                                    u.F_DutyName ,
                                    u.F_PostName ,
                                    u.F_EnabledMark ,
                                    u.F_CreateDate,
                                    u.F_Description
                            FROM    Base_User u
                                    LEFT JOIN Base_Organize o ON o.F_OrganizeId = u.F_OrganizeId
                                    LEFT JOIN Base_Department d ON d.F_DepartmentId = u.F_DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND u.F_UserId <> 'System' AND u.F_EnabledMark = 1 AND u.F_DeleteMark=0 order by o.F_FullName,d.F_FullName,u.F_RealName");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public DataTable GetExportList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT F_Account
                                  ,F_RealName
                                  ,CASE WHEN F_Gender=1 THEN '男' ELSE '女' END AS F_Gender
                                  ,F_Birthday
                                  ,F_Mobile
                                  ,F_Telephone
                                  ,u.F_Email
                                  ,F_WeChat
                                  ,F_MSN
                                  ,u.F_Manager
                                  ,o.F_FullName AS F_Organize
                                  ,d.F_FullName AS F_Department
                                  ,u.F_Description
                                  ,u.F_CreateDate
                                  ,u.F_CreateUserName
                              FROM Base_User u
                              INNER JOIN Base_Department d ON u.F_DepartmentId=d.F_DepartmentId
                              INNER JOIN Base_Organize o ON u.F_OrganizeId=o.F_OrganizeId");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public UserEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public UserEntity CheckLogin(string username)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.F_Account == username);
            expression = expression.Or(t => t.F_Mobile == username);
            expression = expression.Or(t => t.F_Email == username);
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistAccount(string account, string keyValue)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.F_Account == account);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.F_UserId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, UserEntity userEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                #region 基本信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    userEntity.Modify(keyValue);
                    userEntity.F_Password = null;
                    db.Update(userEntity);
                }
                else
                {
                    userEntity.Create();
                    keyValue = userEntity.F_UserId;
                    userEntity.F_Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                    userEntity.F_Password = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(userEntity.F_Password, 32).ToLower(), userEntity.F_Secretkey).ToLower(), 32).ToLower();
                    db.Insert(userEntity);
                    
                }
                #endregion

                #region 默认添加 角色、岗位、职位
                db.Delete<UserRelationEntity>(t => t.F_IsDefault == 1 && t.F_UserId == userEntity.F_UserId);
                List<UserRelationEntity> userRelationEntitys = new List<UserRelationEntity>();
                //角色
                if (!string.IsNullOrEmpty(userEntity.F_RoleId))
                {
                    userRelationEntitys.Add(new UserRelationEntity
                    {
                        F_Category = 2,
                        F_UserRelationId = Guid.NewGuid().ToString(),
                        F_UserId = userEntity.F_UserId,
                        F_ObjectId = userEntity.F_RoleId,
                        F_CreateDate = DateTime.Now,
                        F_CreateUserId = OperatorProvider.Provider.Current().UserId,
                        F_CreateUserName = OperatorProvider.Provider.Current().UserName,
                        F_IsDefault = 1,
                    });
                }
                //岗位
                if (!string.IsNullOrEmpty(userEntity.F_DutyId))
                {
                    userRelationEntitys.Add(new UserRelationEntity
                    {
                        F_Category = 3,
                        F_UserRelationId = Guid.NewGuid().ToString(),
                        F_UserId = userEntity.F_UserId,
                        F_ObjectId = userEntity.F_DutyId,
                        F_CreateDate = DateTime.Now,
                        F_CreateUserId = OperatorProvider.Provider.Current().UserId,
                        F_CreateUserName = OperatorProvider.Provider.Current().UserName,
                        F_IsDefault = 1,
                    });
                }
                //职位
                if (!string.IsNullOrEmpty(userEntity.F_PostId))
                {
                    userRelationEntitys.Add(new UserRelationEntity
                    {
                        F_Category = 4,
                        F_UserRelationId = Guid.NewGuid().ToString(),
                        F_UserId = userEntity.F_UserId,
                        F_ObjectId = userEntity.F_PostId,
                        F_CreateDate = DateTime.Now,
                        F_CreateUserId = OperatorProvider.Provider.Current().UserId,
                        F_CreateUserName = OperatorProvider.Provider.Current().UserName,
                        F_IsDefault = 1,
                    });
                }
                db.Insert(userRelationEntitys);
                #endregion

                db.Commit();

                return keyValue;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码（MD5 小写）</param>
        public void RevisePassword(string keyValue, string Password)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_UserId = keyValue;
            userEntity.F_Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
            userEntity.F_Password = Md5Helper.MD5(DESEncrypt.Encrypt(Password, userEntity.F_Secretkey).ToLower(), 32).ToLower();
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public void UpdateState(string keyValue, int State)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.Modify(keyValue);
            userEntity.F_EnabledMark = State;
            this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public void UpdateEntity(UserEntity userEntity)
        {
            this.BaseRepository().Update(userEntity);
        }
        #endregion
    }
}
