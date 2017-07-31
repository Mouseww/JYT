using LeaRun.Util.Ioc;
using Microsoft.Practices.Unity;
using System.Configuration;

namespace LeaRun.Data.Repository
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.10.10
    /// 描 述：数据库建立工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, DatabaseType DbType)
        {
            DbHelper.DbType = DbType;
            return UnityIocHelper.DBInstance.GetService<IDatabase>(DbHelper.DbType.ToString(), new ParameterOverride(
              "connString", connString));
        }
        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base()
        {
            string providerName = ConfigurationManager.ConnectionStrings["BaseDb"].ProviderName;
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    DbHelper.DbType = DatabaseType.SqlServer;
                    break;
                case "MySql.Data.MySqlClient":
                    DbHelper.DbType = DatabaseType.MySql;
                    break;
                case "Oracle.ManagedDataAccess.Client":
                    DbHelper.DbType = DatabaseType.Oracle;
                    break;
                default:
                    DbHelper.DbType = DatabaseType.SqlServer;
                    break;
            }
            return UnityIocHelper.DBInstance.GetService<IDatabase>(DbHelper.DbType.ToString(), new ParameterOverride(
             "connString", "BaseDb"));
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Data()
        {
            string providerName = ConfigurationManager.ConnectionStrings["DataDb"].ProviderName;
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    DbHelper.DbType = DatabaseType.SqlServer;
                    break;
                case "MySql.Data.MySqlClient":
                    DbHelper.DbType = DatabaseType.MySql;
                    break;
                case "Oracle.ManagedDataAccess.Client":
                    DbHelper.DbType = DatabaseType.Oracle;
                    break;
                default:
                    DbHelper.DbType = DatabaseType.SqlServer;
                    break;
            }
            return UnityIocHelper.DBInstance.GetService<IDatabase>(DbHelper.DbType.ToString(), new ParameterOverride(
             "connString", "DataDb"));
        }
    }
}
