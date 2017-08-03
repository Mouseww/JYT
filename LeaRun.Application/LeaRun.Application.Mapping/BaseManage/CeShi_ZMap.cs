using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 16:44
    /// 描 述：主表
    /// </summary>
    public class CeShi_ZMap : EntityTypeConfiguration<CeShi_ZEntity>
    {
        public CeShi_ZMap()
        {
            #region 表、主键
            //表
            this.ToTable("CESHI_Z");
            //主键
            this.HasKey(t => t.Z_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
