using LeaRun.Application.Entity.FormManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-12-14 05:56
    /// 描 述：Form_ModuleContent
    /// </summary>
    public class Form_ModuleContentMap : EntityTypeConfiguration<Form_ModuleContentEntity>
    {
        public Form_ModuleContentMap()
        {
            #region 表、主键
            //表
            this.ToTable("FORM_MODULECONTENT");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
