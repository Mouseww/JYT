using LeaRun.Application.Entity.FormManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈小二
    /// 日 期：2016.11.29 14:35
    /// 描 述：表单模块表映射
    /// </summary>
    public class FormModuleMap : EntityTypeConfiguration<FormModuleEntity>
    {
        public FormModuleMap()
        {
            #region 表、主键
            //表
            this.ToTable("FORM_MODULE");
            //主键
            this.HasKey(t => t.F_FrmId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
