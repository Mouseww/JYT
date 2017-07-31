using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// �� �� LearunADMS V6.1.2.0
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-07-29 08:37
    /// �� ����CeShi
    /// </summary>
    public class CeShiEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// CeShi_Id
        /// </summary>
        /// <returns></returns>
        [Column("CESHI_ID")]
        public string CeShi_Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        [Column("NAME")]
        public string Name { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CeShi_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CeShi_Id = keyValue;
        }
        #endregion
    }
}