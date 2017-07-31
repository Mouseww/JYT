using LeaRun.Application.Entity.AppManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.AppManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-07-18 11:52
    /// 描 述：App_PageTemplates
    /// </summary>
    public class AppTemplatesMap : EntityTypeConfiguration<AppTemplatesEntity>
    {
        public AppTemplatesMap()
        {
            #region 表、主键
            //表
            this.ToTable("APP_TEMPLATES");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}