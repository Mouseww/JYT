using LeaRun.Application.Entity.DemoManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.DemoManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2016-10-17 09:47
    /// �� ������������
    /// </summary>
    public class Client_OrderEntryMap : EntityTypeConfiguration<Client_OrderEntryEntity>
    {
        public Client_OrderEntryMap()
        {
            #region ��������
            //��
            this.ToTable("Client_OrderEntry");
            //����
            this.HasKey(t => t.F_OrderEntryId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}