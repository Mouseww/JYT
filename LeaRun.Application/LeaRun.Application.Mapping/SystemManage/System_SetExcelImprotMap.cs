using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� �� LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������С��
    /// �� �ڣ�2016-12-04 14:46
    /// �� ����System_SetExcelImprot
    /// </summary>
    public class System_SetExcelImprotMap : EntityTypeConfiguration<System_SetExcelImprotEntity>
    {
        public System_SetExcelImprotMap()
        {
            #region ������
            //��
            this.ToTable("System_SetExcelImprot");
            //����
            this.HasKey(t => t.F_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
