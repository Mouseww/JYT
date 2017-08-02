using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// �� �� LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 14:13
    /// �� ����Base_PostTypes
    /// </summary>
    public class Base_PostTypesEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���ͱ�����
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPEID")]
        public string F_TypeID { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [Column("TYPENAME")]
        public string TypeName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.F_TypeID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_TypeID = keyValue;
        }
        #endregion
    }
}