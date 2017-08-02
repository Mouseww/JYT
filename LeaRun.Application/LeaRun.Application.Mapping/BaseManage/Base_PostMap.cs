using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 11:24
    /// 描 述：Base_Post
    /// </summary>
    public class Base_PostMap : EntityTypeConfiguration<Base_PostEntity>
    {
        public Base_PostMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Post");
            //主键
            this.HasKey(t => t.F_PostID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
