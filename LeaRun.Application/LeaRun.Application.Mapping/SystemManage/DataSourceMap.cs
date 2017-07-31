using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-09-07 09:39
    /// 描 述：Base_DataSource
    /// </summary>
    public class DataSourceMap : EntityTypeConfiguration<DataSourceEntity>
    {
        public DataSourceMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_DataSource");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
