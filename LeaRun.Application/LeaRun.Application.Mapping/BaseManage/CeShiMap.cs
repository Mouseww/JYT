using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-07-29 08:37
    /// �� ����CeShi
    /// </summary>
    public class CeShiMap : EntityTypeConfiguration<CeShiEntity>
    {
        public CeShiMap()
        {
            #region ������
            //��
            this.ToTable("CESHI");
            //����
            this.HasKey(t => t.CeShi_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
