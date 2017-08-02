using LeaRun.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-08-02 14:13
    /// 描 述：Base_PostTypes
    /// </summary>
    public class Base_PostTypesMap : EntityTypeConfiguration<Base_PostTypesEntity>
    {
        public Base_PostTypesMap()
        {
            #region 表、主键
            //表
            this.ToTable("BASE_POSTTYPES");
            //主键
            this.HasKey(t => t.F_TypeID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
