using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-06-17 10:34
    /// �� ������������
    /// </summary>
    public class Client_OrderMap : EntityTypeConfiguration<Client_OrderEntity>
    {
        public Client_OrderMap()
        {
            #region ������
            //��
            this.ToTable("Client_Order");
            //����
            this.HasKey(t => t.F_OrderId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
