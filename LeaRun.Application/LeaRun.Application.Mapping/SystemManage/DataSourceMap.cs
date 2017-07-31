using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2016-09-07 09:39
    /// �� ����Base_DataSource
    /// </summary>
    public class DataSourceMap : EntityTypeConfiguration<DataSourceEntity>
    {
        public DataSourceMap()
        {
            #region ������
            //��
            this.ToTable("Base_DataSource");
            //����
            this.HasKey(t => t.F_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
