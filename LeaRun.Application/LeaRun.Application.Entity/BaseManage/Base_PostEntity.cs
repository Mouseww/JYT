using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-08-02 11:24
    /// �� ����Base_Post
    /// </summary>
    public class Base_PostEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���ӱ�����
        /// </summary>
        /// <returns></returns>
        public string F_PostID { get; set; }
        /// <summary>
        /// �����ID
        /// </summary>
        /// <returns></returns>
        public string Proposal_ID { get; set; }
        /// <summary>
        /// �ĵ���ID
        /// </summary>
        /// <returns></returns>
        public string Document_ID { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string NO { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string OrdinalTitle { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Contents { get; set; }
        /// <summary>
        /// �ĵ���Դ
        /// </summary>
        /// <returns></returns>
        public string DocumentName { get; set; }
        /// <summary>
        /// �ϴ�ʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public int? Traffic { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string Proposal { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.F_PostID = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_PostID = keyValue;
                                            }
        #endregion
    }
}