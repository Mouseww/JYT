using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-29 08:37
    /// 描 述：CeShi
    /// </summary>
    public class CeShiMap : EntityTypeConfiguration<CeShiEntity>
    {
        public CeShiMap()
        {
            #region 表、主键
            //表
            this.ToTable("CESHI");
            //主键
            this.HasKey(t => t.CeShi_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
