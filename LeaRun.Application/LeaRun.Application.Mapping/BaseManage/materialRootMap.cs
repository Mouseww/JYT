using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：孔毛毛
    /// 日 期：2017-08-02 09:41
    /// 描 述：materialRoot
    /// </summary>
    public class materialRootMap : EntityTypeConfiguration<materialRootEntity>
    {
        public materialRootMap()
        {
            #region 表、主键
            //表
            this.ToTable("materialRoot");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
