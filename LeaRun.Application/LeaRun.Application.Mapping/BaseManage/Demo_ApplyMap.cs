using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-12-20 19:44
    /// 描 述：Demo_Apply
    /// </summary>
    public class Demo_ApplyMap : EntityTypeConfiguration<Demo_ApplyEntity>
    {
        public Demo_ApplyMap()
        {
            #region 表、主键
            //表
            this.ToTable("Demo_Apply");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
