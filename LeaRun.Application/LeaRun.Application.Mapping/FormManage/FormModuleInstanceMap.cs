using LeaRun.Application.Entity.FormManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FormManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：陈彬彬
    /// 编辑人：陈小二 
    /// 编辑日期：2016.11.29 13:52
    /// 日 期：2016.03.18 09:58
    /// 描 述：表单模块实体表映射
    /// </summary>
    public class FormModuleInstanceMap : EntityTypeConfiguration<FormModuleInstanceEntity>
    {
       public FormModuleInstanceMap()
       {
            #region 表、主键
            //表
           this.ToTable("FORM_MODULEINSTANCE");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
