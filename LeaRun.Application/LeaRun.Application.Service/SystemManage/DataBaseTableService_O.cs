using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.18 11:02
    /// 描 述：数据库管理（支持：SqlServer）
    /// </summary>
    public class DataBaseTableService_O : RepositoryFactory, IDataBaseTableService
    {
        private IDataBaseLinkService dataBaseLinkService = new DataBaseLinkService();

        #region 获取数据
        /// <summary>
        /// 数据表列表
        /// </summary>
        /// <param name="dataBaseLinkId">库连接Id</param>
        /// <param name="tableName">表明</param>
        /// <returns></returns>
        public DataTable GetTableList(string dataBaseLinkId, string tableName)
        {
            DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(dataBaseLinkId);
            if (dataBaseLinkEntity != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select distinct col.table_name f_name,
                                                0 f_reserved,
                                                0 f_fdata,
                                                0 f_index_size,
                                                nvl(t.num_rows, 0) f_sumrows,
                                                0 f_funused,
                                                tab.comments f_tdescription,
                                                column_name f_pk
                                  from user_cons_columns col
                                 inner join user_constraints con
                                    on con.constraint_name = col.constraint_name
                                 inner join user_tab_comments tab
                                    on tab.table_name = col.table_name
                                 inner join user_tables t
                                    on t.TABLE_NAME = col.table_name
                                 where con.constraint_type not in ('C', 'R') ");
                if (!string.IsNullOrEmpty(tableName))
                {
                    strSql.Append(" AND col.table_name='" + tableName + "'");
                }
                strSql.Append(" ORDER BY col.table_name");
                return this.BaseRepository(dataBaseLinkEntity.F_DbConnection, DatabaseType.Oracle).FindTable(strSql.ToString());
            }
            return null;
        }
        /// <summary>
        /// 数据表字段列表
        /// </summary>
        /// <param name="dataBaseLinkId">库连接Id</param>
        /// <param name="tableName">表明</param>
        /// <returns></returns>
        public IEnumerable<DataBaseTableFieldEntity> GetTableFiledList(string dataBaseLinkId, string tableName)
        {
            DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(dataBaseLinkId);
            if (dataBaseLinkEntity != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select col.column_id  f_number,
                                       col.column_name f_column,
                                       col.data_type f_datatype,
                                       col.data_length  f_length,
                                       null f_identity,
                                       case uc.constraint_type when 'P' then 1 else null end f_key,
                                       case col.nullable when 'N' then 0 else 1 end   f_isnullable,
                                       col.data_default f_defaults,
                                       comm.comments as f_remark
                                  from user_tab_columns col
                                   inner join user_col_comments comm
                                   on comm.TABLE_NAME = col.TABLE_NAME 
                                   and comm.COLUMN_NAME = col.COLUMN_NAME
                                  left join user_cons_columns ucc
                                    on ucc.table_name = col.table_name
                                   and ucc.column_name = col.column_name 
                                   and ucc.position=1
                                  left join user_constraints uc
                                    on uc.constraint_name = ucc.constraint_name
                                   and uc.constraint_type = 'P'
                                 where col.table_name = :tableName
                                order by col.column_id");
                DbParameter[] parameter = 
                {
                    DbParameters.CreateDbParameter(":tableName",tableName)
                };
                return this.BaseRepository(dataBaseLinkEntity.F_DbConnection, DatabaseType.Oracle).FindList<DataBaseTableFieldEntity>(strSql.ToString(), parameter);
            }
            return null;
        }
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="dataBaseLinkId">库连接</param>
        /// <param name="tableName">表明</param>
        /// <param name="switchWhere">条件</param>
        /// <param name="logic">逻辑</param>
        /// <param name="keyword">关键字</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public DataTable GetTableDataList(string dataBaseLinkId, string tableName, string switchWhere, string logic, string keyword, Pagination pagination)
        {
            DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(dataBaseLinkId);
            if (dataBaseLinkEntity != null)
            {
                StringBuilder strSql = new StringBuilder();
                List<DbParameter> parameter = new List<DbParameter>();
                strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1");
                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND " + switchWhere + "");
                    switch (logic)
                    {
                        case "Equal":           //等于
                            strSql.Append(" = ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "NotEqual":        //不等于
                            strSql.Append(" <> ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "Greater":         //大于
                            strSql.Append(" > ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "GreaterThan":     //大于等于
                            strSql.Append(" >= ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "Less":            //小于
                            strSql.Append(" < ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "LessThan":        //小于等于
                            strSql.Append(" >= ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "Null":            //为空
                            strSql.Append(" is null ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "NotNull":         //不为空
                            strSql.Append(" is not null ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, keyword));
                            break;
                        case "Like":            //包含
                            strSql.Append(" like ");
                            parameter.Add(DbParameters.CreateDbParameter(":" + switchWhere, '%' + keyword + '%'));
                            break;
                        default:
                            break;
                    }
                    strSql.Append(":" + switchWhere + "");
                }
                return this.BaseRepository(dataBaseLinkEntity.F_DbConnection, DatabaseType.Oracle).FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            return null;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据库表表单（新增、修改）
        /// </summary>
        /// <param name="dataBaseLinkId">库连接Id</param>
        /// <param name="tableName">表名称</param>
        /// <param name="tableDescription">表说明</param>
        /// <param name="fieldList">字段列表</param>
        public void SaveForm(string dataBaseLinkId, string tableName, string tableDescription, IEnumerable<DataBaseTableFieldEntity> fieldList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare num   number; ");
            strSql.Append(" begin ");
            strSql.Append(" select count(1) into num from user_tables where TABLE_NAME = 'EMP' ;");
            strSql.Append(" if   num=1   then ");
            strSql.Append("   drop table " + tableName + "");
            strSql.Append("end if;");
            strSql.Append("end;");
            strSql.Append("create table " + tableName + " (");


            strSql.Append("   LogId                varchar(50)        not null,");
            strSql.Append("   SourceObjectId       varchar(50)        null,");
            strSql.Append("   SourceContentJson    text               null,");
            strSql.Append("   IPAddress            varchar(50)        null,");
            strSql.Append("   IPAddressName        varchar(200)       null,");
            strSql.Append("   Category             int                null,");



            strSql.Append("   constraint PK_BASE_LOG primary key nonclustered (LogId)");
            strSql.Append(")");
            strSql.Append("go");
        }
        #endregion
    }
}
