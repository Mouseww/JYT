using LeaRun.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// �� �� LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 14:13
    /// �� ����Base_PostTypes
    /// </summary>
    public class Base_PostTypesMap : EntityTypeConfiguration<Base_PostTypesEntity>
    {
        public Base_PostTypesMap()
        {
            #region ������
            //��
            this.ToTable("BASE_POSTTYPES");
            //����
            this.HasKey(t => t.F_TypeID);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
