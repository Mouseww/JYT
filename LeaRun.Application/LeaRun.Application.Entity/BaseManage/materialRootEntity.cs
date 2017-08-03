using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �Ϻ�������Ϣ�������޹�˾
    /// �� ������ëë
    /// �� �ڣ�2017-08-02 09:41
    /// �� ����materialRoot
    /// </summary>
    public class materialRootEntity : BaseEntity
    {
        #region ʵ���Ա
        /// <summary>
        /// ���Id
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }
        /// <summary>
        /// ���һ����Id
        /// </summary>
        /// <returns></returns>
        public string parentId { get; set; }
        /// <summary>
        /// ������Id
        /// </summary>
        /// <returns></returns>
        public string parentIds { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public string parentNames { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }
        /// <summary>
        /// ȡ����׼
        /// </summary>
        /// <returns></returns>
        public string quYangBiaoZuen { get; set; }
        /// <summary>
        /// des
        /// </summary>
        /// <returns></returns>
        public string des { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int? sort { get; set; }
        /// <summary>
        /// ��Ч�ĺϷ���
        /// </summary>
        /// <returns></returns>
        public string valid { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string inputPersonId { get; set; }
        /// <summary>
        /// ��ԪId
        /// </summary>
        /// <returns></returns>
        public string unitId { get; set; }
        /// <summary>
        /// Ҷ��
        /// </summary>
        /// <returns></returns>
        public string isLeaf { get; set; }
        /// <summary>
        /// ���1
        /// </summary>
        /// <returns></returns>
        public string jianCha1 { get; set; }
        /// <summary>
        /// ���2
        /// </summary>
        /// <returns></returns>
        public string jianCha2 { get; set; }
        /// <summary>
        /// ���3
        /// </summary>
        /// <returns></returns>
        public string jianCha3 { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string other { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id =keyValue;
                                            }
        #endregion
    }
}