using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� �� LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������С��
    /// �� �ڣ�2016-12-07 15:32
    /// �� ����System_SetExcelExport
    /// </summary>
    public class System_SetExcelExportMap : EntityTypeConfiguration<System_SetExcelExportEntity>
    {
        public System_SetExcelExportMap()
        {
            #region ������
            //��
            this.ToTable("System_SetExcelExport");
            //����
            this.HasKey(t => t.F_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
