using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2016-12-20 19:44
    /// �� ����Demo_Apply
    /// </summary>
    public class Demo_ApplyMap : EntityTypeConfiguration<Demo_ApplyEntity>
    {
        public Demo_ApplyMap()
        {
            #region ������
            //��
            this.ToTable("Demo_Apply");
            //����
            this.HasKey(t => t.F_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
