using LeaRun.Cache.Factory;
using LeaRun.Util;
using System;

namespace LeaRun.Application.Code
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 编辑人：LR0101
    /// 日 期：2015.10.10
    /// 描 述：当前操作者回话
    /// </summary>
    public class OperatorProvider : OperatorIProvider
    {
        #region 静态实例
        /// <summary>
        /// 当前提供者
        /// </summary>
        public static OperatorIProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        /// <summary>
        /// 给app调用
        /// </summary>
        public static string AppUserId
        {
            set;
            get;
        }
        #endregion

        /// <summary>
        /// 秘钥
        /// </summary>
        private string LoginUserKey = "Learun_LoginUserKey_2016_V6.1";
        /// <summary>
        /// 登陆提供者模式:Session、Cookie 
        /// </summary>
        private string LoginProvider = Config.GetValue("LoginProvider");
        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        /// <param name="dataAuthorize">成员数据权限</param>
        public virtual void AddCurrent(Operator user, AuthorizeDataModel dataAuthorize)
        {
            try
            {
                if (LoginProvider == "Cookie")
                {
                    WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                else
                {
                    WebHelper.WriteSession(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                dataAuthorize.Token = user.Token;
                CacheFactory.Cache().WriteCache(dataAuthorize, user.UserId, user.LogTime.AddHours(12));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 跟新登录信息缓存
        /// </summary>
        /// <param name="user"></param>
        public virtual void UpdateCurrent(Operator user)
        {
            try
            {
                if (LoginProvider == "Cookie")
                {
                    WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
                else
                {
                    WebHelper.WriteSession(LoginUserKey, DESEncrypt.Encrypt(user.ToJson()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public virtual Operator Current()
        {
            try
            {
                Operator user = new Operator();
                if (LoginProvider == "Cookie")
                {
                     user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();
                }
                else if (LoginProvider == "AppClient")
                {
                    user = CacheFactory.Cache().GetCache<Operator>("mb" + AppUserId);
                }
                else
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<Operator>();
                }
                
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 获取当前用户的数据权限
        /// </summary>
        /// <returns></returns>
        public virtual AuthorizeDataModel CurrentDataAuthorize()
        {
            try
            {
                return  CacheFactory.Cache().GetCache<AuthorizeDataModel>(Current().UserId);
            }
            catch (Exception ex) 
            {
                
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 设置当前的数据权限
        /// </summary>
        /// <param name="model"></param>
        public virtual void AddCurrentDataAuthorize(AuthorizeDataModel dataAuthorize,string userId)
        {
            try 
	        {
                CacheFactory.Cache().WriteCache(dataAuthorize, userId, DateTime.Now.AddHours(12));
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
        }

        /// <summary>
        /// 删除登录信息
        /// </summary>
        public virtual void EmptyCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOverdue()
        {
            try
            {
                object str = "";
                if (LoginProvider == "Cookie")
                {
                    str = WebHelper.GetCookie(LoginUserKey);
                }
                else
                {
                    str = WebHelper.GetSession(LoginUserKey);
                }
                if (str != null && str.ToString() != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public virtual int IsOnLine()
        {
            Operator user = new Operator();
            if (LoginProvider == "Cookie")
            {
                user = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<Operator>();
            }
            else
            {
                user = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<Operator>();
            }
            AuthorizeDataModel dataAuthorize = CacheFactory.Cache().GetCache<AuthorizeDataModel>(user.UserId);
            if (user == null)
            {
                return -1;//过期
            }
            if (dataAuthorize.Token == user.Token)
            {
                return 1;//正常
            }
            else
            {
                return 0;//已登录
            }
        }
    }
}
