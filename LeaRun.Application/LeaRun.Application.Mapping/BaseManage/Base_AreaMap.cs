using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-07-31 11:07
    /// �� �������������
    /// </summary>
    public class Base_AreaMap : EntityTypeConfiguration<Base_AreaEntity>
    {
        public Base_AreaMap()
        {
            #region ������
            //��
            this.ToTable("Base_Area");
            //����
            this.HasKey(t => t.F_AreaId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
