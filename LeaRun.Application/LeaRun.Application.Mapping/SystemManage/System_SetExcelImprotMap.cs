using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：陈小二
    /// 日 期：2016-12-04 14:46
    /// 描 述：System_SetExcelImprot
    /// </summary>
    public class System_SetExcelImprotMap : EntityTypeConfiguration<System_SetExcelImprotEntity>
    {
        public System_SetExcelImprotMap()
        {
            #region 表、主键
            //表
            this.ToTable("System_SetExcelImprot");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
