using LeaRun.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 11:24
    /// �� ����Base_Post
    /// </summary>
    public class Base_PostMap : EntityTypeConfiguration<Base_PostEntity>
    {
        public Base_PostMap()
        {
            #region ������
            //��
            this.ToTable("Base_Post");
            //����
            this.HasKey(t => t.F_PostID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
