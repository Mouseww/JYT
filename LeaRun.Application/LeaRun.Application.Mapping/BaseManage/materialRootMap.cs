using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������ëë
    /// �� �ڣ�2017-08-02 09:41
    /// �� ����materialRoot
    /// </summary>
    public class materialRootMap : EntityTypeConfiguration<materialRootEntity>
    {
        public materialRootMap()
        {
            #region ������
            //��
            this.ToTable("materialRoot");
            //����
            this.HasKey(t => t.id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
