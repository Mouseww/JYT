using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 16:44
    /// �� ��������
    /// </summary>
    public class CeShi_ZMap : EntityTypeConfiguration<CeShi_ZEntity>
    {
        public CeShi_ZMap()
        {
            #region ������
            //��
            this.ToTable("CESHI_Z");
            //����
            this.HasKey(t => t.Z_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
